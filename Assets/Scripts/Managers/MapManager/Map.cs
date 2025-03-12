using UnityEngine;
using VInspector;

public class Map : MonoBehaviour
{
    public static readonly float TILE_SIZE = 4.0f;

    #region Inspecor
    private bool isInitialized;

    [Tab("Inside Settings")]
    [SerializeField, DisableIf("isInitialized")] private int columns;
    [SerializeField, DisableIf("isInitialized")] private int rows;

    [Tab("Outside Settings")]
    [SerializeField, DisableIf("isInitialized")] private int margin;

    [EndTab]
    #endregion


    // 초기 던전 크기
    private static readonly int TILE_COLUMNS = 3;
    private static readonly int TILE_ROWS = 2;

    private GameObject prefab_Block;
    private GameObject prefab_Tile;
    private GameObject prefab_Wall;

    private Map_Tile[][] tiles;

    private void Awake()
    {
        if (isInitialized) return;
        isInitialized = true;

        prefab_Block = Resources.Load<GameObject>($"{Define.PATH_MAP}/Block");
        prefab_Tile = Resources.Load<GameObject>($"{Define.PATH_MAP}/Tile");
        prefab_Wall = Resources.Load<GameObject>($"{Define.PATH_MAP}/Wall");

        tiles = new Map_Tile[rows][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new Map_Tile[columns];
        }
    }

    private void Start()
    {
        // 블록 생성
        for (int row = 0; row < tiles.Length; row++)
        {
            for (int column = 0; column < tiles[row].Length; column++)
            {
                CreateBlock(row, column);
            }
        }

        // 타일 생성
        int startColumn = (columns - TILE_COLUMNS) / 2;
        int endColumn = startColumn + TILE_COLUMNS;
        for (int row = 0; row < TILE_ROWS; row++)
        {
            for (int column = startColumn; column < endColumn; column++)
            {
                CreateTile(tiles[row][column] as Map_Block);
            }
        }

        // 벽 생성 (가로)
        for (int column = 0; column < columns; column++)
        {
            if (tiles[0][column] is Map_Block)
            {
                CreateWall(0, column).SetActive_Wall(Map_Block.Direction.South);
            }

            CreateWall(rows - 1, column).SetActive_Wall(Map_Block.Direction.North);
        }

        // 벽 생성 (세로)
        for (int row = 0; row < rows; row++)
        {
            CreateWall(row, 0).SetActive_Wall(Map_Block.Direction.West);
            CreateWall(row, columns - 1).SetActive_Wall(Map_Block.Direction.East);
        }

        // 입구 타일 생성
        Transform Tiles = transform.Find(nameof(Tiles)).transform;
        for (int row = 0; row < 10; row++)
        {
            for (int column = -margin; column < columns + margin; column++)
            {
                Vector3 position = new(column * TILE_SIZE, 0.0f, row * -TILE_SIZE);
                Quaternion rotation = Quaternion.Euler(90.0f * Random.Range(0, 4) * Vector3.up);
                Instantiate(prefab_Tile, position, rotation, Tiles);
            }
        }

        // 외벽 생성
        Transform Walls = transform.Find(nameof(Walls)).transform;
        for (int row = 0; row < rows + margin; row++)
        {
            for (int column = -margin; column < columns + margin; column++)
            {
                if (row >= 0 && row < rows && column >= 0 && column < columns)
                {
                    continue;
                }

                Vector3 position = new(column * TILE_SIZE, 0.0f, row * TILE_SIZE);
                Instantiate(prefab_Block, position, Quaternion.identity, Walls);
            }
        }

        // 입구 위치 변경
        transform.Find("Entrance").transform.position = columns / 2 * TILE_SIZE * Vector3.right;
    }

    private void CreateBlock(int row, int column)
    {
        Vector3 position = new(column * TILE_SIZE, 0.0f, row * TILE_SIZE);
        Map_Block block = Instantiate(prefab_Block, position, Quaternion.identity, transform).GetComponent<Map_Block>();
        block.Initialize(this);

        tiles[row][column] = block;
    }

    public void CreateTile(Map_Block block)
    {
        int row = block.Cell.y;
        int column = block.Cell.x;

        Map_Block blockNorth = tiles[Mathf.Min(row + 1, rows - 1)][column] as Map_Block;
        if (blockNorth != null)
        {
            blockNorth.Expandable = true;
            blockNorth.SetActive_Wall(block);
        }

        Map_Block blockEast = tiles[row][Mathf.Min(column + 1, columns - 1)] as Map_Block;
        if (blockEast != null)
        {
            blockEast.Expandable = true;
            blockEast.SetActive_Wall(block);
        }

        Map_Block blockSouth = tiles[Mathf.Max(row - 1, 0)][column] as Map_Block;
        if (blockSouth != null)
        {
            blockSouth.Expandable = true;
            blockSouth.SetActive_Wall(block);
        }

        Map_Block blockWest = tiles[row][Mathf.Max(column - 1, 0)] as Map_Block;
        if (blockWest)
        {
            blockWest.Expandable = true;
            blockWest.SetActive_Wall(block);
        }

        Quaternion rotation = Quaternion.Euler(90.0f * Random.Range(0, 4) * Vector3.up);
        Map_Tile tile = Instantiate(prefab_Tile, block.GetPosition(), rotation, transform).GetComponent<Map_Tile>();
        tiles[block.Cell.y][block.Cell.x] = tile;

        Destroy(block.gameObject);
    }

    private Map_Block CreateWall(int row, int column)
    {
        Vector3 position = new(column * TILE_SIZE, 0.0f, row * TILE_SIZE);
        Map_Block wall = Instantiate(prefab_Wall, position, Quaternion.identity, transform).GetComponent<Map_Block>();
        wall.Initialize(this);

        return wall;
    }
}