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
            StartCoroutine(Spawn(5f));
        }

    }
    private void Update()
    {
        if (spawnCount < maxSpawnCount)
        {
            StartCoroutine(ReSpawn(3f));
        }
    }


    private void Spawn()
    {
        int randomIndex = Random.Range(0, Objects.Length);
        Vector3 randomPos = map.GetRandomPosition();
        GameObject newResource = Instantiate(Objects[randomIndex], randomPos, Quaternion.identity, parentTransform);
    }

    IEnumerator ReSpawn(float respwanTime)
    {
        int randomIndex = Random.Range(0, Objects.Length);

        yield return new WaitForSeconds(respwanTime);
        Debug.Log(" 재생성");

        Vector3 randomPos = map.GetRandomPosition();
        if (spawnCount < maxSpawnCount)
        {
            GameObject newResource = Instantiate(Objects[randomIndex], randomPos, Quaternion.identity, parentTransform);
            spawnCount++;
            StartCoroutine(ReSpawn(1f));
        }
    }

    IEnumerator Spawn(float respwanTime)
    {
        int randomIndex = Random.Range(0, Objects.Length);
        Debug.Log("ResopwanResource");

        yield return new WaitForSeconds(respwanTime);
        Debug.Log(" 생성");

        Vector3 randomPos = map.GetRandomPosition();
        GameObject newResource = Instantiate(Objects[randomIndex], randomPos, Quaternion.identity, parentTransform);
    }
}