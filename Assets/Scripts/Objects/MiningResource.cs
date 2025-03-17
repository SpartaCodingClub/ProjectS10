using UnityEngine;

public class MiningResource : InteractableObject
{
    [SerializeField] private ItemData itemToGive; //얻을수있는 아이템
    [SerializeField] private int capacity; // 총 몇번때릴수있는지 
    [SerializeField] private int curentCapacity;

    public override void OnInteraction()
    {
        base.OnInteraction();

        if (curentCapacity > 0)
        {
            Gather(transform.position, transform.up);
        }
    }

    private void Start()
    {
        curentCapacity = capacity;
    }


    private void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        int amount = Random.Range(1, 3);
        Item item = new Item(itemToGive, amount);

        curentCapacity--;
        Managers.Item.AddItem(item);
        if (curentCapacity <= 0)
        {
            DestroyResource();
            Managers.Game.ReSpawn();
        }
    }

    private void DestroyResource()
    {
        Managers.Resource.Destroy(gameObject);
        Managers.Game.SpawnCount--;
    }
}