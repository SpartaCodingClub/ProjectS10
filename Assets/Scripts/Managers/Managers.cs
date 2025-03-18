using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }

    public static readonly AudioManager Audio = new();
    public static readonly BuildingManager Building = new();
    public static readonly DataManager Data = new();
    public static readonly GameManager Game = new();
    public static readonly ItemManager Item = new();
    public static readonly PoolManager Pool = new();
    public static readonly ResourceManager Resource = new();
    public static readonly UIManager UI = new();
    public static readonly EnemyManger Enemy = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        Audio.Initialize();
        Game.Initialize();
        Pool.Initialize();
        Resource.Initialize();
        UI.Initialize();
    }

    private void Start()
    {
        Game.Start();
        Item.Start();
        InvokeRepeating(nameof(SurfaceBuild), 1, 0.5f);
    }

    private void SurfaceBuild()
    {
        Game.NavMeshSurface.BuildNavMesh();
    }

    private void Update()
    {
        Building.Update();
    }
}