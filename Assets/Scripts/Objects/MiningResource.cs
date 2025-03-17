using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MiningResource : InteractableObject
{
    [SerializeField] private ItemData itemToGive; //얻을수있는 아이템
    [SerializeField] private int capacity; // 총 몇번때릴수있는지 

    private UI_Inventory inventoryUI;
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
        int amount = Random.Range(0, 10);
        Item item = new Item(itemToGive , amount);

            capacity--;
            Managers.Item.AddItem(item);
            if (capacity <= 0)
            {
                DestroyResource();
                Debug.Log($"Destroy" );
            }
    }

    private void DestroyResource()
    {
        Destroy(gameObject);
        resourceObject.spawnCount--;
    }
}

