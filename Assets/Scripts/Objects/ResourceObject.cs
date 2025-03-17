using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] private GameObject[] Objects;
    public int spawnCount;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int maxSpawnCount;


    public static ResourceObject Instance;
    Map map;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        map = FindAnyObjectByType<Map>();
       
    }

    public void ReSpawn()
    {
        if (spawnCount < maxSpawnCount)
        {
            StartCoroutine(SpawnValue(3f));
            spawnCount++;
        }

    }

    public void Spawn()
    {
        spawnCount = 0;
        for (int i = 0; i < maxSpawnCount; i++)
        {
            StartCoroutine(SpawnValue(0));
            spawnCount++;
        }

    }

    IEnumerator SpawnValue(float respwanTime)
    {
        yield return new WaitForSeconds(respwanTime);
        int randomIndex = Random.Range(0, 100);
        Vector3 randomPos = map.GetRandomPosition();
        MiningResource miningResource = GetComponent<MiningResource>();

        if (randomIndex < 10)
        {

            if (randomIndex < 5)
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal Purple", randomPos);
                newResource.transform.SetParent(parentTransform);
            }
            else
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal Purple 2", randomPos);
                newResource.transform.SetParent(parentTransform);
            }

        }
        else if (randomIndex < 40)
        {
            if (randomIndex < 20)
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal Blue", randomPos);
                newResource.transform.SetParent(parentTransform);
            }
            else
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal Blue 2", randomPos);
                newResource.transform.SetParent(parentTransform);
            }
        }
        else
        {
            if (randomIndex < 70)
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal", randomPos);
                newResource.transform.SetParent(parentTransform);
            }
            else
            {
                GameObject newResource = Managers.Resource.Instantiate("Crystal 2", randomPos);
                newResource.transform.SetParent(parentTransform);
            }
        }

    }
}