using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour , IDamageable
{
    public UICondition UICondition; // UICondition 스크립트의 인스턴스

    Condition health { get { return UICondition.Health; } }
    Condition hunger { get { return UICondition.hunger; } }
    Condition stamina { get { return UICondition.stamina; } }

    public float noHungerHealthDecay; // 배고픔이 없을 때 체력 감소 속도

    public event Action OnTakeDamage; // 피해를 받았을 때 호출되는 이벤트

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curvalue == 0f) 
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curvalue == 0f) 
        {
            Die();
        }

    }

    public void Heal(float amount) 
    {
       health.Add(amount);
    }

    public void Eat(float amount) 
    { 
       hunger.Add(amount);
    }

    public void Die()
    {
       Debug.Log("죽었다!");
    }

    public void TakePhysicalDamage(int damage) 
    { 
       health.Subtract(damage);
        OnTakeDamage?.Invoke(); // 이벤트 호출
    }
}
