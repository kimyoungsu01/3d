using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
   Equipable,
   Consumable,
   Resource
}

public enum ConsumableType
{
  Health,
  Hunger,
}

public  class itemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class itemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject DropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackSize;

    [Header("Consumable")]
    public itemDataConsumable[] consumables;
}
