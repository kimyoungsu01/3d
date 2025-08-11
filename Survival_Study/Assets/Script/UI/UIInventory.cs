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
    public ItemSlot[] Slots; // ������ ���� �迭

    public GameObject InventoryWindow; // �κ��丮 �г� ������Ʈ
    public Transform slotPanel; // ���� �г� Ʈ������
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selecteditemName; // ���õ� ������ �̸�
    public TextMeshProUGUI selecteditemDescription; // ���õ� ������ ����
    public TextMeshProUGUI selectedStatName; // ���õ� ������ ���� �̸�
    public TextMeshProUGUI selectedStatValue; // ���õ� ������ ���� ��
    public GameObject useButton; // ��� ��ư �ؽ�Ʈ
    public GameObject equipButton; // ��� ��ư �ؽ�Ʈ
    public GameObject unequipButton; // ��� ��ư �ؽ�Ʈ
    public GameObject dropButton; // ��� ��ư �ؽ�Ʈ

    private PlayerController controller; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    private PlayerCondition condition; // �÷��̾� ���� ��ũ��Ʈ

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle; // �κ��丮 �׼��� ��� �Լ��� ����
        //CharacterManager.Instance.Player.addItem += AddItem;

        InventoryWindow.SetActive(false); // �κ��丮 â�� ��Ȱ��ȭ
        Slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            Slots[i].index = i; // �� ���Կ� �ε��� �Ҵ�
            Slots[i].Inventory = this; // �� ���Կ� UIInventory ��ũ��Ʈ�� �Ҵ�
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearSelectedItemWindow()
    {
        selecteditemName.text = string.Empty; // ���õ� ������ �̸� �ʱ�ȭ
        selecteditemDescription.text = string.Empty; // ���õ� ������ ���� �ʱ�ȭ
        selectedStatName.text = string.Empty; // ���õ� ������ ���� �̸� �ʱ�ȭ
        selectedStatValue.text = string.Empty; // ���õ� ������ ���� �� �ʱ�ȭ

        useButton.SetActive(false); // ��� ��ư ��Ȱ��ȭ
        equipButton.SetActive(false); // ���� ��ư ��Ȱ��ȭ
        unequipButton.SetActive(false); // ���� ��ư ��Ȱ��ȭ
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

        // �������� �ߺ��� ��������? canStack
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++; // ������ ������ ���� ����
                CharacterManager.Instance.Player.itemData = null; // ������ ������ �ʱ�ȭ
                return; // ������ ������ �ִٸ� ����
            }
        }

       //����ִ� ���� �����´�
       ItemSlot emptySlot = GetEmptySlot();

        // �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1; // ������ ������ ������ 1�� ����
            UpdateUI(); // UI ������Ʈ
            CharacterManager.Instance.Player.itemData = null; // ������ ������ �ʱ�ȭ
            return; // �������� �߰��Ǿ����Ƿ� ����
        }

        // ���ٸ�
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

        return null; // TODO: ������ ������ �������� ���� ����
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
        return null; // TODO: �� ������ �������� ���� ����
    }

    void ThrowItem(itemData data)
    {
        Instantiate(data.DropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
}
