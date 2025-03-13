using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Resource : MonoBehaviour
{
    //public ItemData itemToGive; //드랍되는아이템
    public int quantityperhit = 1; // 
    public int capacity; // 총 몇번때릴수있는지 

    public float respawnTime;
    public Vector3 respawnArea;

    private void Start()
    {
        Respawn();
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
        gameObject.SetActive(false);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        if (capacity <=1)// 다른오브젝트와 겹쳤을경우
        {
            yield break;
        }
        yield return new WaitForSeconds(respawnTime);

        
        gameObject.SetActive(true);
    }
}
