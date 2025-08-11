using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public itemData item; // 아이템 데이터 스크립트

    public Button button; // 아이템 슬롯 버튼
    public Image icon; // 아이템 아이콘 이미지
    public TextMeshProUGUI quantityText; // 아이템 수량 텍스트
    private Outline outline; // 아이템 슬롯 아웃라인

    public UIInventory Inventory; // UI 인벤토리 스크립트

    public int index;
    public bool equipped; // 장착 여부
    public int quantity; // 아이템 수량

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void OnEnable()
    {
        outline.enabled = equipped; // 장착된 경우 아웃라인 활성화
    }

    public void Set()
    {
        icon.gameObject.SetActive(true); // 아이콘 활성화
        icon.sprite = item.icon; // 아이콘 이미지 설정
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty; // 수량이 1보다 크면 표시, 아니면 빈 문자열

        if (outline != null) 
        { 
           outline.enabled = equipped; // 장착된 경우 아웃라인 활성화
        }
    }

    public void Clear()
    {
        item = null; // 아이템 데이터 초기화
        icon.gameObject.SetActive(false); // 아이콘 비활성화
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        Inventory.SelectItem(index);
    }
}
