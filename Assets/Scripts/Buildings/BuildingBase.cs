using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] private ItemData buildingData;
    private BuildingAnimation buildingAnimation;
    private float currentHealth;
    private float buildingHeight;

    public string Name => buildingData.Name;  
    public float MaxHealth => buildingData.MaxHealth;
    public float CurrentHealth => currentHealth;
    public Vector3Int ResourceCost => buildingData.ResourceAmount;
    public float BuildTime => buildingData.BuildTime;

    public float BuildingHeight => buildingHeight;
    public BuildingAnimation BuildingAnim => buildingAnimation;

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
        buildingHeight = GetComponent<Renderer>().bounds.size.y;

        transform.position = new Vector3(transform.position.x, -buildingHeight, transform.position.z);

        if (buildingAnimation != null)
        {
            buildingAnimation.PlayAnimation(BuildTime, buildingHeight);
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
        Debug.Log($"DestroyBuilding() 실행됨");

        if (buildingAnimation != null)
        {
            Debug.Log($"DestroyBuilding() 애니메이션 실행");
            buildingAnimation.RemoveAnimation(1.5f, buildingHeight);
        }

        StartCoroutine(DestroyRoutine(1.5f));
    }

    private IEnumerator DestroyRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
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
        Destroy(gameObject);
    }
}
