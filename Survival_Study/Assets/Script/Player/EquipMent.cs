using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMent : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipNew(itemData data) 
    {
        UnEquip();
        curEquip = Instantiate(data.EquipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip() 
    {
        if (curEquip != null) 
        { 
          Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
