using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStat : StatHandler
{
    public bool isDead = false;

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
            if (Health <= 0)
            {
                isDead = true;
            }
        }
    }

    public void IsDie()
    {
        // 죽는 애니메이션이 끝나면
        Destroy(gameObject);
    }
}
