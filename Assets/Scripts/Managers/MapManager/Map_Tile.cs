using UnityEngine;

public class Map_Tile : MonoBehaviour
{
    public Vector2Int Cell { get; private set; }

    private void Awake()
    {
        int x = (int)(transform.position.x / Map.TILE_SIZE);
        int y = (int)(transform.position.z / Map.TILE_SIZE);
        Cell = new(x, y);
    }

    public Vector3 GetPosition()
    {
        return new(Cell.x * Map.TILE_SIZE, 0.0f, Cell.y * Map.TILE_SIZE);
    }
}