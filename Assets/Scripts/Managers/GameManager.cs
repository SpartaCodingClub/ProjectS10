using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager
{
    #region SpawnResources
    public int SpawnCount { get; set; }
    private int maxSpawnCount = 10;

    public void ReSpawn()
    {
        if (SpawnCount < maxSpawnCount)
        {
            Managers.Instance.StartCoroutine(WaitForSpawn(3f));
        }
    }

    private IEnumerator WaitForSpawn(float respwanTime, int amount = 1)
    {
        yield return new WaitForSeconds(respwanTime);

        for (int i = 0; i < amount; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        int randomIndex = Random.Range(0, 100);
        Vector3 randomPos = CurrentMap.GetRandomPosition();

        if (randomIndex < 10)
        {
            if (randomIndex < 5)
            {
                Managers.Resource.Instantiate("Crystal", randomPos);
            }
            else
            {
                Managers.Resource.Instantiate("Crystal 2", randomPos);
            }
        }
        else if (randomIndex < 55)
        {
            if (randomIndex < 20)
            {
                Managers.Resource.Instantiate("Crystal Blue", randomPos);
            }
            else
            {
                Managers.Resource.Instantiate("Crystal Blue 2", randomPos);
            }
        }
        else
        {
            if (randomIndex < 70)
            {
                Managers.Resource.Instantiate("Crystal Purple", randomPos);
            }
            else
            {
                Managers.Resource.Instantiate("Crystal Purple 2", randomPos);
            }
        }

        SpawnCount++;
    }
    #endregion

    public Camera MainCamera { get; private set; }
    public Map CurrentMap { get; set; }
    public PlayerController Player { get; set; }

    public NavMeshSurface NavMeshSurface { get; private set; }
    private GameObject navMeshObject;

    private UI_Stage stageUI;

    public void Initialize()
    {
        DOTween.SetTweensCapacity(200, 125);

        MainCamera = Camera.main;

        navMeshObject = Resources.Load<GameObject>($"{Define.PATH_PLAYER}/PlayerSurface");
        NavMeshSurface = Managers.Resource.Instantiate(navMeshObject).GetComponent<NavMeshSurface>();
    }

    public void Start()
    {
        Managers.Audio.Play(Clip.Music_Game);
        Managers.UI.Show<UI_Title>().OnClosed += GameStart;
        Managers.UI.Show<UI_Build>();
        Managers.Game.Player.enabled = false;

        Managers.Instance.StartCoroutine(WaitForSpawn(0.0f, maxSpawnCount));
    }

    private void GameStart()
    {
        Managers.Game.Player.enabled = true;
        stageUI = Managers.UI.Show<UI_Stage>();
        stageUI.SetTimer(6.0f, WaveStart);
    }

    private void WaveStart()
    {
        CurrentMap.Open();
        DOVirtual.DelayedCall(1.0f, () => Managers.Game.Player.ForceMovePlayer(new Vector3(0, 0, -5f), true));
    }
}