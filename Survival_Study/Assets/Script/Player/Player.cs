using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition; // PlayerCondition 스크립트의 인스턴스
    public EquipMent equip;

    public itemData itemData; // 아이템 데이터 스크립터블 오브젝트
    public Action addItem; // 아이템을 추가할 때 호출되는 액션

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<EquipMent>();
    }
}
