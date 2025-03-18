using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType
{
    E_Archer,
    E_King,
    E_Knight,
    E_Mage,
    E_Spearman,
    E_Count,

    E_MiniBossCavalry,
    E_Boss_Dragon
}

public class EnemyManger
{
    private Coroutine waveRoutine;

    private readonly List<EnemyController> activeEnemies = new List<EnemyController>(); // 생성된 적

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    public void StartWave(int waveCount)
    {
        if (waveRoutine != null)
            Managers.Instance.StopCoroutine(waveRoutine);

        waveRoutine = Managers.Instance.StartCoroutine(SpawnWave(waveCount));
    }


    public void StopWave()
    {
        Managers.Instance.StopAllCoroutines();
    }

    // 적 프리팹 소환 코루틴
    private IEnumerator SpawnWave(int waveCount)
    {
        int spawnWaveCount = waveCount * 2;
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < spawnWaveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        if (waveCount % 2 == 0)
        {
            BossSpawnRandomEnemy(Resources.Load<GameObject>($"Objects/{EnemyType.E_MiniBossCavalry}"));
        }

        if (waveCount % 3 == 0)
        {
            BossSpawnRandomEnemy(Resources.Load<GameObject>($"Objects/{EnemyType.E_Boss_Dragon}"));
        }

        enemySpawnComplite = true;
    }


    private void SpawnRandomEnemy()
    {
        // 랜덤 적 프리팹 생성
        GameObject randomPrefab = Resources.Load<GameObject>($"Objects/{(EnemyType)Random.Range(0, (int)EnemyType.E_Count)}");

        // Rect 영역 내부의 랜덤 위치 계산
        Vector3 enemySpawnPosition = Managers.Game.CurrentMap.EnemySpawnPosition;
        Vector3 randomPosition = new Vector3(
            Random.Range(-enemySpawnPosition.x, enemySpawnPosition.x),
            0,
            enemySpawnPosition.z
            );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Managers.Resource.Instantiate(randomPrefab);
        spawnedEnemy.transform.position = new Vector3(randomPosition.x, randomPosition.y, randomPosition.z);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        activeEnemies.Add(enemyController);
    }

    // 적 프리팹 소환 범위 설정
    //private void OnDrawGizmosSelected()
    //{
    //    if (spawnAreas == null) return;

    //    Gizmos.color = gizmoColor;
    //    foreach (var area in spawnAreas)
    //    {
    //        Vector3 center = new Vector3(area.x + area.width / 2, 0.2f, area.y + area.height / 2);
    //        Vector3 size = new Vector3(area.width, 0, area.height);
    //        Gizmos.DrawCube(center, size);
    //    }

    //}

    int i = 1;
    private void Update()
    {

        // 테스트용 웨이브 시작
        if (Input.GetKeyDown(KeyCode.L))
        {

            StartWave(i);
            i++;
        }
    }

    private void BossSpawnRandomEnemy(GameObject boss)
    {
        //if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        //{
        //    Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
        //    return;
        //}

        // Rect 영역 내부의 랜덤 위치 계산
        Vector3 enemySpawnPosition = Managers.Game.CurrentMap.EnemySpawnPosition;
        Vector3 randomPosition = new Vector3(
            Random.Range(-enemySpawnPosition.x, enemySpawnPosition.x),
            0,
            enemySpawnPosition.z
            );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Managers.Resource.Instantiate(boss);
        spawnedEnemy.transform.position = new Vector3(randomPosition.x, randomPosition.y, randomPosition.z);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        activeEnemies.Add(enemyController);
    }
}