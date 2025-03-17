using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiningResource : InteractableObject
{
    [SerializeField]public ItemData itemToGive; //드랍되는아이템
    [SerializeField] public int quantityperhit; // 
    [SerializeField] public int capacity; // 총 몇번때릴수있는지 
    [SerializeField] public int resourceNumber;

    ResourceObject resourceObject;


    public override void OnInteraction()
    {
        base.OnInteraction();

        if (capacity > 0)
        {
            Gather(transform.position , transform.up);
        }
    }

    private void Start()
    {
        resourceObject = FindAnyObjectByType<ResourceObject>();
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityperhit; i++)
        {
            capacity--;
            if (capacity <= 0)
            {
                DestroyResource();
                Debug.Log($"Destroy" );
                break;
            }
            Instantiate(itemToGive, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    private void DestroyResource()
    {
        Destroy(gameObject);
        resourceObject.spawnCount--;
    }
}

