using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : Poolable
{
    [Header("건물 기본 설정")]
    public float health = 100f;
    public float maxHealth = 100f;
    protected bool isConstructed = false;

    public virtual void Initialize()
    {
        health = maxHealth;
        isConstructed = false;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyBuilding();
        }
    }

    public virtual void DestroyBuilding()
    {
        Managers.Resource.Destroy(gameObject);
    }

    public virtual void StartConstruction(float buildTime)
    {
        StartCoroutine(Construct(buildTime));
    }

    private IEnumerator Construct(float buildTime)
    {
        float timer = 0f;
        while (timer < buildTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isConstructed = true;
    }
}
