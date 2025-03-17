using DG.Tweening;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager
{
    public Camera MainCamera { get; private set; }
    public Map CurrentMap { get; set; }
    public PlayerController Player { get; set; }

    private GameObject NavMeshSurface { get; set; }

    public NavMeshSurface surface;

    private UI_Stage stageUI;

    public void Initialize()
    {
        DOTween.SetTweensCapacity(200, 125);

        MainCamera = Camera.main;

        NavMeshSurface = Resources.Load<GameObject>($"{Define.PATH_PLAYER}/PlayerSurface");

        surface = Managers.Resource.Instantiate(NavMeshSurface).GetComponent<NavMeshSurface>();
    }

    public void Start()
    {
        Managers.Audio.Play(Clip.Music_Game);
        Managers.UI.Show<UI_Title>();
        Managers.UI.Show<UI_Build>();

        stageUI = Managers.UI.Show<UI_Stage>();
        stageUI.SetTimer(60.0f, () => Debug.Log("TEST"));
    }
}