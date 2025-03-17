using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : Poolable
{
    [Header("건물 기본 설정")]
    [SerializeField] private ItemData buildingData;
    private float currentHealth;

    private BuildingAnimation buildingAnimation;
    private float buildingHeight;

    public string Name => buildingData.Name;  
    public float MaxHealth => buildingData.MaxHealth;
    public float CurrentHealth => currentHealth;
    public int ResourceCost => buildingData.ResourceAmount;
    public float BuildTime => buildingData.BuildTime;

    public virtual void Initialize()
    {
        if (buildingData != null)
        {
            currentHealth = buildingData.MaxHealth; 
        }
        else
        {
            currentHealth = 100; 
        }

        buildingAnimation = GetComponent<BuildingAnimation>();

        // 빌딩 높이만큼 지하로 내려가 있는 상태
        buildingHeight = GetComponent<Renderer>().bounds.size.y;
        transform.position = new Vector3(transform.position.x, -buildingHeight, transform.position.z);

        if (buildingAnimation != null)
        {
            buildingAnimation.PlayAnimation(1.5f, buildingHeight);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"남은 체력: {currentHealth}");

        if (currentHealth <= 0)
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

        Managers.Resource.Destroy(gameObject);
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
