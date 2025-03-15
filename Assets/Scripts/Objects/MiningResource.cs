using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiningResource : InteractableObject
{
    //public ItemData itemToGive; //드랍되는아이템
    public GameObject resource;
    public int quantityperhit = 1; // 
    public int capacity; // 총 몇번때릴수있는지 
    public int resourceumber;

    public Transform parentTransform;

    private List<GameObject> activeResources = new List<GameObject>();

    private GameObject currentInteractResource;

    Map map;

    public override void OnInteraction()
    {
        base.OnInteraction();

        if (capacity > 0)
        {
            Gather(transform.position, transform.up);
        }
    }
    private void Start()
    {
        map = FindAnyObjectByType<Map>();
        if (map == null)
        {
            Debug.Log("맵이 없습니다 ");
            return;
        }
        Transform parentObject = GameObject.Find("Resource")?.transform; // 부모 오브젝트 찾기
        parentTransform = parentObject; // 부모 Transform 할당
        for (int i = 0; i < resourceumber; i++)
        {
            SpawnResource();
        }
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityperhit; i++)
        {
            if (capacity <= 0)
            {
                DestroyResource(currentInteractResource);
                Debug.Log("Destroy");
                break;
            }
            capacity -= 1;
            //Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    void DestroyResource(GameObject resourceToDestroy)
    {
        if (resourceToDestroy != null)
        {
            Destroy(resourceToDestroy);
            activeResources.Remove(resourceToDestroy);  // 리스트에서 해당 리소스 제거
        }
        StartCoroutine(RespawnResource(10f, parentTransform));
    }
    IEnumerator RespawnResource(float respwanTime , Transform parnetRransform)
    {
        yield return new WaitForSeconds(respwanTime);
        SpawnResource();
    }

    void SpawnResource()
    {
        Vector3 randomPos = map.GetRandomPosition();
        GameObject newResource = Instantiate(resource, randomPos, Quaternion.identity, parentTransform);
        activeResources.Add(newResource);

        currentInteractResource = newResource;
    }
}
