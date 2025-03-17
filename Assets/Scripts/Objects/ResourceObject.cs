using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceObject : MonoBehaviour
{
    [SerializeField]private GameObject[] Objects;
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
        for (int i = 0; i < spawnCount; i++)
        {
            StartCoroutine(Spawn(0.1f));
        }

    }
    private void Update()
    {
        if (spawnCount < maxSpawnCount)
        {
            StartCoroutine(ReSpawn(3f));
        }
    }


    //private void Spawn()
    //{
    //    int randomIndex = Random.Range(0, Objects.Length);
    //    Vector3 randomPos = map.GetRandomPosition();
    //    GameObject newResource = Instantiate(Objects[randomIndex], randomPos, Quaternion.identity, parentTransform);
    //}

    IEnumerator ReSpawn(float respwanTime)
    {
        int randomIndex = Random.Range(0, Objects.Length);

        yield return new WaitForSeconds(respwanTime);

        Vector3 randomPos = map.GetRandomPosition();
        if (spawnCount < maxSpawnCount)
        {
            StartCoroutine(Spawn(1f));
            spawnCount++;
        }
        StartCoroutine(ReSpawn(1f));
    }

    IEnumerator Spawn(float respwanTime)
    {
        int randomIndex = Random.Range(0, 100);

        yield return new WaitForSeconds(respwanTime);
        Vector3 randomPos = map.GetRandomPosition();
        if (randomIndex < 10)
        {
            
            if(randomIndex < 5)
            {
                GameObject newResource = Instantiate(Objects[4], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("4");
            }
            else
            {
                GameObject newResource = Instantiate(Objects[5], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("5");
            }

        }
        else if (randomIndex < 40)
        {
            if (randomIndex < 20)
            {
                GameObject newResource = Instantiate(Objects[3], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("3");
            }
            else
            {
                GameObject newResource = Instantiate(Objects[2], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("2");
            }
        }
        else
        {
            if (randomIndex < 70)
            {
                GameObject newResource = Instantiate(Objects[1], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("1");
            }
            else
            {
                GameObject newResource = Instantiate(Objects[0], randomPos, Quaternion.identity, parentTransform);
                Debug.Log("0");
            }
        }
    }
}