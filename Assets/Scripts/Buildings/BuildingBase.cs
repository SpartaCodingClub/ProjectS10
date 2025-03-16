using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : Poolable
{
    [Header("건물 기본 설정")]
    public float health = 100f;
    public float maxHealth = 100f;
    protected bool isConstructed = false;

    private BuildingAnimation buildingAnimation;
    private float buildingHeight;

    public virtual void Initialize()
    {
        health = maxHealth;
        isConstructed = false;
        buildingAnimation = GetComponent<BuildingAnimation>();

        // 빌딩 높이만큼 지하로 내려가 있는 상태
        buildingHeight = GetComponent<Renderer>().bounds.size.y;
        transform.position = new Vector3(transform.position.x, -buildingHeight, transform.position.z);

        if (buildingAnimation != null)
        {
            buildingAnimation.PlayAnimation(1.5f, buildingHeight);
        }
    }

    //protected virtual void OnConstructionComplete()
    //{
        
    //}

    //private IEnumerator Construct(float buildTime)
    //{
    //    yield return new WaitForSeconds(buildTime);
    //    isConstructed = true;
    //    OnConstructionComplete();
    //}

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
        BuildingDestruction destruction = GetComponent<BuildingDestruction>();

        if (destruction != null)
        {
            destruction.StartDestruction();
            return;
        }
        Destroy(gameObject);
    }

    public virtual void StartRemoving(float removeTime)
    {
        if (buildingAnimation != null)
        {
            buildingAnimation.RemoveAnimation(removeTime, buildingHeight);
        }
        StartCoroutine(RemoveRoutine(removeTime));
    }

    private IEnumerator RemoveRoutine(float removeTime)
    {
        yield return new WaitForSeconds(removeTime);
        Managers.Resource.Destroy(gameObject);
    }
}
