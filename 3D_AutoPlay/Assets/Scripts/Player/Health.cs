using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private ConditionSO conditionSO; // PlayerConSO 또는 EnemyConSO 할당

    private int health;
    public int CurrentHealth => health;
    public event Action OnDie;

    public bool IsDie = false;

    void Start()
    {
        if (conditionSO != null)
        {
            maxHealth = conditionSO.MaxHP;
        }
        else
        {
            Debug.LogWarning("ConditionSO가 할당되지 않았습니다.");
            maxHealth = 100; //기본값 할당
        }
            health = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            IsDie = true;
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
}
