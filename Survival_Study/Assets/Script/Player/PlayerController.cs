using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float MoveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMarsk; // 땅 레이어

    [Header("Look")]
    public Transform cameraContainer; // 카메라가 부착된 컨테이너
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLock = true;

    public Action inventory; // 인벤토리 액션
    private Rigidbody _rigidbody;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLock) 
        {
            CameraLook();
        }
    }
    void Move()
    {
        Debug.DrawRay(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down * 0.5f, Color.red, 0.1f);
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // 현재 방향과 입력값을 곱해서 이동 방향을 구함
        dir *= MoveSpeed;
        dir.y = _rigidbody.velocity.y; // y축 속도는 유지

        _rigidbody.velocity = dir; // Rigidbody의 속도를 설정하여 이동
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 카메라의 X축 회전을 제한
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) //키가 손에서 떨어졌을 때
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            Debug.Log("점프 실행됨");
        }
        Debug.Log(IsGrounded());
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.3f), Vector3.down),
            new Ray(transform.position + (- transform.right * 0.2f) + (transform.up * 0.3f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.3f), Vector3.down),
            new Ray(transform.position + (- transform.forward * 0.2f) + (transform.up * 0.3f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMarsk))
            {
                return true; // 땅에 닿아있으면 true 반환
            }
        }
        return false; // 땅에 닿아있지 않으면 false 반환
    }
    public void OnInventory(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started) 
        {
            inventory?.Invoke(); // 인벤토리 액션을 호출
            ToggleCursor();
        }
    }

    void ToggleCursor() 
    { 
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = toggle;
        canLock = !toggle;
    }
}
