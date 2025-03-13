using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Resource : MonoBehaviour
{
    //public ItemData itemToGive; //드랍되는아이템
    public GameObject resource;
    public int quantityperhit = 1; // 
    public int capacity; // 총 몇번때릴수있는지 

    public float respawnTime;
    private Vector3 respawnArea;

    private void Start()
    {
        Respawn();
        Debug.Log(resource);
        Debug.Log(resource.transform.position);
    }
    public void Gather(Vector3 hitPoint , Vector3 hitNormal)
    {
        for (int i = 0; i < quantityperhit; i++) 
        {
            if (capacity <= 0)
            {
                DestroyResource();
                break;
            }

            capacity -= 1;
            //Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    void DestroyResource()
    {
        Managers.Resource.Destroy(gameObject);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
          
        yield return new WaitForSeconds(respawnTime);
        Vector3 randomPosition;

        randomPosition.x = Random.Range(respawnArea.x - 14, respawnArea.x + 14);
        randomPosition.y = respawnArea.y;
        randomPosition.z = Random.Range(respawnArea.z -14, respawnArea.z + 14);


        GameObject crystal = Managers.Resource.Instantiate("Crystal_01", transform.position = respawnArea);

        if (crystal != null) 
        { 
            crystal.transform.position = randomPosition;
        }
    }
}
