using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Cobat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public override void OnAttackInput() //애니메이터를 동작시키는 함수
    {
        if (!attacking) 
        { 
            attacking = true;
            animator.SetTrigger("Attact");
            Invoke("OnCanAttac", attackRate);
        }
    }

    void OnCanAttack() 
    { 
      attacking = false;
    }

}
