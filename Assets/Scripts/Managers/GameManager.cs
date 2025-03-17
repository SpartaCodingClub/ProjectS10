using DG.Tweening;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager
{
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
        Managers.UI.Show<UI_Title>();
        Managers.UI.Show<UI_Build>();

        stageUI = Managers.UI.Show<UI_Stage>();
        stageUI.SetTimer(60.0f, () => Debug.Log("TEST"));
        ResourceObject.Instance.Spawn();
    }
}