using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] Slots; // 아이템 슬롯 배열

    public GameObject InventoryWindow; // 인벤토리 패널 오브젝트
    public Transform slotPanel; // 슬롯 패널 트랜스폼
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selecteditemName; // 선택된 아이템 이름
    public TextMeshProUGUI selecteditemDescription; // 선택된 아이템 설명
    public TextMeshProUGUI selectedStatName; // 선택된 아이템 스탯 이름
    public TextMeshProUGUI selectedStatValue; // 선택된 아이템 스탯 값
    public GameObject useButton; // 사용 버튼 텍스트
    public GameObject equipButton; // 사용 버튼 텍스트
    public GameObject unequipButton; // 사용 버튼 텍스트
    public GameObject dropButton; // 사용 버튼 텍스트

    private PlayerController controller; // 플레이어 컨트롤러 스크립트
    private PlayerCondition condition; // 플레이어 상태 스크립트

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle; // 인벤토리 액션을 토글 함수로 설정
        //CharacterManager.Instance.Player.addItem += AddItem;

        InventoryWindow.SetActive(false); // 인벤토리 창을 비활성화
        Slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            Slots[i].index = i; // 각 슬롯에 인덱스 할당
            Slots[i].Inventory = this; // 각 슬롯에 UIInventory 스크립트를 할당
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearSelectedItemWindow()
    {
        selecteditemName.text = string.Empty; // 선택된 아이템 이름 초기화
        selecteditemDescription.text = string.Empty; // 선택된 아이템 설명 초기화
        selectedStatName.text = string.Empty; // 선택된 아이템 스탯 이름 초기화
        selectedStatValue.text = string.Empty; // 선택된 아이템 스탯 값 초기화

        useButton.SetActive(false); // 사용 버튼 비활성화
        equipButton.SetActive(false); // 장착 버튼 비활성화
        unequipButton.SetActive(false); // 해제 버튼 비활성화
        dropButton.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            InventoryWindow.SetActive(false);
        }

        else 
        {
            InventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return InventoryWindow.activeInHierarchy;
    }

    public void AddItem()
    {
        itemData data = CharacterManager.Instance.Player.itemData;

        // 아이템이 중복이 가능한지? canStack
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++; // 아이템 슬롯의 수량 증가
                CharacterManager.Instance.Player.itemData = null; // 아이템 데이터 초기화
                return; // 아이템 스택이 있다면 종료
            }
        }

       //비어있는 슬롯 가져온다
       ItemSlot emptySlot = GetEmptySlot();

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1; // 아이템 슬롯의 수량을 1로 설정
            UpdateUI(); // UI 업데이트
            CharacterManager.Instance.Player.itemData = null; // 아이템 데이터 초기화
            return; // 아이템이 추가되었으므로 종료
        }

        // 없다면
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item != null)
            {
                Slots[i].Set();
            }

            else
            {
                Slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(itemData data)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item == data && Slots[i].quantity < data.maxStackAmount)
            {
                return Slots[i];
            }
        }

        return null; // TODO: 아이템 스택을 가져오는 로직 구현
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item == null)
            {
                return Slots[i];
            }
        }
        return null; // TODO: 빈 슬롯을 가져오는 로직 구현
    }

    void ThrowItem(itemData data)
    {
        Instantiate(data.DropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
}
