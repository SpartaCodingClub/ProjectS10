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
    public int resourceNumber;

    public Transform parentTransform;

    private List<GameObject> activeResources = new List<GameObject>();

    private GameObject currentInteractResource;

    Map map;

    public override void OnInteraction()
    {
        base.OnInteraction();

        if (capacity > 0)
        {
            Gather();
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
        parentTransform = GameObject.Find("Resource")?.transform; // 부모 오브젝트 찾기
        for (int i = 0; i < resourceNumber; i++)
        {
            StartCoroutine(RespawnResource(100f));
        }
    }

    public void Gather()
    {
        for (int i = 0; i < quantityperhit; i++)
        {
            capacity--;
            if (capacity <= 0)
            {
                DestroyResource(resource);
                Debug.Log($"Destroy" );
                break;
            }

            //Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    private void DestroyResource(GameObject resourceToDestroy)
    {
        activeResources.Remove(resourceToDestroy);  // 리스트에서 해당 리소스 제거
        Destroy(resourceToDestroy);
        Debug.Log("재생성 시작");
        StartCoroutine(RespawnResource(3f));
    }


    IEnumerator RespawnResource(float respwanTime)
    {
        Debug.Log("ResopwanREsource");
        yield return new WaitForSeconds(respwanTime);

        Debug.Log(" 생성");
        Vector3 randomPos = map.GetRandomPosition();
        GameObject newResource = Instantiate(resource, randomPos, Quaternion.identity, parentTransform);
        newResource.GetComponent<MiningResource>().parentTransform = parentTransform;
        activeResources.Add(newResource);

        currentInteractResource = newResource;

        //Spawn();
    }



    //public void Spawn()
    //{
    //    Vector3 randomPos = map.GetRandomPosition();
    //    GameObject newResource = Instantiate(resource, randomPos, Quaternion.identity,parentTransform);
    //    newResource.GetComponent<MiningResource>().parentTransform = parentTransform;
    //    activeResources.Add(newResource);

    //    currentInteractResource = newResource;
    //}
}

