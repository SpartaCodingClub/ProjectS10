using UnityEngine;

public class GameManager
{
    public Camera MainCamera { get; private set; }
    public Map CurrentMap { get; set; }
    public PlayerController Player { get; set; }

    public void Initialize()
    {
        MainCamera = Camera.main;
    }

    public void Start()
    {
        Managers.Audio.Play(Clip.Music_Game);
        Managers.UI.Show<UI_Title>();

        // 추후 플레이어 컨디션 스크립트와 연결
        Managers.UI.Show<UI_PlayerCondition>();
    }
}