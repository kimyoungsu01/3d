using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public itemData item; // ������ ������ ��ũ��Ʈ

    public Button button; // ������ ���� ��ư
    public Image icon; // ������ ������ �̹���
    public TextMeshProUGUI quantityText; // ������ ���� �ؽ�Ʈ
    private Outline outline; // ������ ���� �ƿ�����

    public UIInventory Inventory; // UI �κ��丮 ��ũ��Ʈ

    public int index;
    public bool equipped; // ���� ����
    public int quantity; // ������ ����

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void OnEnable()
    {
        outline.enabled = equipped; // ������ ��� �ƿ����� Ȱ��ȭ
    }

    public void Set()
    {
        icon.gameObject.SetActive(true); // ������ Ȱ��ȭ
        icon.sprite = item.icon; // ������ �̹��� ����
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty; // ������ 1���� ũ�� ǥ��, �ƴϸ� �� ���ڿ�

        if (outline != null) 
        { 
           outline.enabled = equipped; // ������ ��� �ƿ����� Ȱ��ȭ
        }
    }

    public void Clear()
    {
        item = null; // ������ ������ �ʱ�ȭ
        icon.gameObject.SetActive(false); // ������ ��Ȱ��ȭ
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        Inventory.SelectItem(index);
    }
}
