using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStat : StatHandler
{
    public bool isDead = false;

    [SerializeField] private UI_HealthBar healthBar;

    private float maxHealth;

    void Start()
    {
        maxHealth = Health;
        healthBar = gameObject.FindComponent<UI_HealthBar> ("UI_HealthBar_Monster");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(float Damage)
    {
        if (Health > 0)
        {
            Health -= Damage;
            healthBar.UpdateUI(Health, maxHealth);
            if (Health <= 0)
            {
                isDead = true;
            }
        }
    }



    public void IsDie()
    {
        // 죽는 애니메이션이 끝나면 애니메이션 이벤트로 호출
        Destroy(gameObject);
    }
}
