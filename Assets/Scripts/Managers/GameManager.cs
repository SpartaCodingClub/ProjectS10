using DG.Tweening;
using UnityEngine;

public class GameManager
{
    public Camera MainCamera { get; private set; }
    public Map CurrentMap { get; set; }
    public PlayerController Player { get; set; }

    private UI_Stage stageUI;
    ResourceObject resourceObject;
    public void Initialize()
    {
        DOTween.SetTweensCapacity(200, 125);

        MainCamera = Camera.main;
    }

    public void Start()
    {
        Managers.Audio.Play(Clip.Music_Game);
        Managers.UI.Show<UI_Title>();
        Managers.UI.Show<UI_Build>();

        stageUI = Managers.UI.Show<UI_Stage>();
        stageUI.SetTimer(60.0f, () => Debug.Log("TEST"));
        resourceObject = new ResourceObject();
    }
}