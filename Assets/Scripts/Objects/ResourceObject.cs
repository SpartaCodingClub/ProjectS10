using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceObject : InteractableObject
{
    //public ItemData itemToGive; //드랍되는아이템
    public GameObject resource;
    public int quantityperhit = 1; // 
    public int capacity; // 총 몇번때릴수있는지 
    public int resourceumber;

    private Vector3 respawnArea;

    Map map;

    private void Start()
    {
        map = FindAnyObjectByType<Map>();
        if (map != null) Debug.Log("맵이할당되었습니다");
        StartCoroutine(Respawn(0.1f));
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
        Managers.Resource.Destroy(resource);
        StartCoroutine(Respawn(30f));
    }
    IEnumerator Respawn(float respwanTime)
    {
        yield return new WaitForSeconds(respwanTime);
        Vector3 randomPos = map.GetRandomPosition();
        Instantiate();
        //Vector3 randomPosition = new Vector3(randomPosition.x = Random.Range(respawnArea.x - 30, respawnArea.x + 30),
        //randomPosition.y = respawnArea.y,
        //randomPosition.z = Random.Range(respawnArea.z = 0, respawnArea.z + 30));

        resource.transform.position = randomPos;
        //Managers.Resource.Instantiate(resource);
        Debug.Log(resource);
        Debug.Log(resource.transform.position);
    }

    private void Instantiate()
    {
        GameObject.Instantiate(resource);
    }
    private void Spawn()
    {
        for (int i = 0; i < resourceumber; i++)
        {
            Vector3 randomPos = map.GetRandomPosition();

            randomPos = new Vector3( Random.Range(respawnArea.x - 30, respawnArea.x + 30),
            randomPos.y = respawnArea.y,
            randomPos.z = Random.Range(respawnArea.z = 0, respawnArea.z + 30));

            resource.transform.position = randomPos;

            Managers.Resource.Instantiate(resource);
            Debug.Log(resource);
            Debug.Log(resource.transform.position);
        }
    }
}