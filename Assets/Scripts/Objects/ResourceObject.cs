using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceObject : InteractableObject
{
    //public ItemData itemToGive; //드랍되는아이템
    public GameObject resource;
    public int quantityperhit = 1; // 
    public int capacity; // 총 몇번때릴수있는지 
    public int resourceumber;

    public Transform position;


    Map map;

    private void Start()
    {
        map = FindAnyObjectByType<Map>();
        if (map != null)
        {
            Debug.Log("맵이할당되었습니다");
        }
        for (int i = 0; i < resourceumber; i++)
        {
            StartCoroutine(RespawnResource(0.1f));

        }
    }



    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityperhit; i++)
        {
            if (capacity <= 0)
            {
                DestroyResource();
                Debug.Log("Destroy");
                break;
            }
            capacity -= 1;
            //Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    void DestroyResource()
    {
        if(resource != null)
        {
            Destroy(resource);
        }
        StartCoroutine(RespawnResource(10f));
    }
    IEnumerator RespawnResource(float respwanTime)
    {

        yield return new WaitForSeconds(respwanTime);
        Vector3 randomPos = map.GetRandomPosition();

        GameObject newResource = Instantiate(resource, randomPos, Quaternion.identity, position);
        Debug.Log(newResource);
        Debug.Log(newResource.transform.position);
    }

}