using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum E_Class
{
    Melee,       // 근접
    Ranged,      // 원거리
    MiniBoss,    // 중간보스
    FinalBoss    // 최종보스
}

public class EnemyStat : StatHandler
{
    public bool isDead = false;
    public E_Class eclass;
    [SerializeField] private UI_HealthBar healthBar;

    private float maxHealth;

    void Start()
    {
        maxHealth = Health;

        switch (eclass)
        {
            case E_Class.Melee:
            case E_Class.Ranged:
                healthBar = gameObject.FindComponent<UI_HealthBar>("UI_HealthBar_Monster");
                break;
            case E_Class.MiniBoss:
                healthBar = gameObject.FindComponent<UI_HealthBar>("UI_HealthBar_Miniboss");
                break;
            case E_Class.FinalBoss:
                healthBar = gameObject.FindComponent<UI_HealthBar>("UI_HealthBar_Boss");
                break;

        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public override void Damage(float damage)
    {

        Health -= damage;
        healthBar.UpdateUI(Health, maxHealth);

        if (Health > 0)
            TakeDamage(damage);
        else
            isDead = true;
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

        Managers.Game.KillMonster();
    }
}
