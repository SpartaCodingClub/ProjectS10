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
        Managers.UI.Show<UI_TitleStart>();
    }
}