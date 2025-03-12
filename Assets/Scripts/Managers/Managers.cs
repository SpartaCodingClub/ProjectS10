using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }

    public static readonly CameraManager Camera = new();
    public static readonly DataManager Data = new();
    public static readonly GameManager Game = new();
    public static readonly InputManager Input = new();
    public static readonly ItemManager Item = new();
    public static readonly MapManager Map = new();
    public static readonly PoolManager Pool = new();
    public static readonly ResourceManager Resource = new();
    public static readonly UIManager UI = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}