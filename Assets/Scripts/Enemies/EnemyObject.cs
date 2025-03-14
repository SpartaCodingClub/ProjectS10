using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyObject : MonoBehaviour
{
    private Coroutine waveRoutine;

    [SerializeField] private List<GameObject> enemyPrefabs; // 적을 생성할 프리팹 리스트

    [SerializeField] private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    private List<EnemyController> activeEnemies = new List<EnemyController>(); // 생성된 적

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    public void StartWave(int waveCount)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }


    public void StopWave()
    {
        StopAllCoroutines();
    }

    // 적 프리팹 소환 코루틴
    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤 적 프리팹 생성
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // 랜덤 위치 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(randomArea.xMin, randomArea.xMax),
            0,
            Random.Range(randomArea.yMin, randomArea.yMax)
            );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y, randomPosition.z), Quaternion.identity);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        activeEnemies.Add(enemyController);
    }

    // 적 프리팹 소환 범위 설정
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, 0.2f, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, 0, area.height);
            Gizmos.DrawCube(center, size);
        }

    }

    private void Update()
    {
        // 테스트용 웨이브 시작
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartWave(5);
        }
    }
}