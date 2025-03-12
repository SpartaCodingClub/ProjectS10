using System;
using UnityEngine;
using VInspector;

public class Map_Block : Map_Tile
{
    #region Inspector
    [ShowInInspector, ReadOnly] public bool Expandable;
    #endregion

    public enum Direction
    {
        North,
        East,
        South,
        West,
        Count
    }

    private Map map;
    private Renderer _renderer;

    private readonly GameObject[] walls = new GameObject[(int)Direction.Count];
    private readonly GameObject[] colums = new GameObject[(int)Direction.Count];

    public void Initialize(Map map)
    {
        this.map = map;
        _renderer = GetComponent<Renderer>();

        string[] names = Enum.GetNames(typeof(Direction));
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = transform.Find(names[i]).gameObject;
        }

        Transform Colums = transform.Find(nameof(Colums));
        for (int i = 0; i < colums.Length; i++)
        {
            colums[i] = Colums.GetChild(i).gameObject;
        }
    }

    private void OnMouseEnter()
    {
        if (_renderer == null)
        {
            return;
        }

        if (Expandable == false)
        {
            _renderer.material.color = Color.red;
            return;
        }

        _renderer.material.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (_renderer == null)
        {
            return;
        }

        _renderer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (_renderer == null)
        {
            return;
        }

        if (Expandable == false)
        {
            return;
        }

        map.CreateTile(this);
    }

    public void SetActive_Wall(Direction direction)
    {
        int startIndex = (int)direction;
        for (int i = startIndex; i < startIndex + 2; i++)
        {
            colums[i % (int)Direction.Count].SetActive(true);
        }

        walls[(int)direction].SetActive(true);
    }

    public void SetActive_Wall(Map_Block block)
    {
        Vector2Int direction = block.Cell - Cell;
        if (direction.y == 1) SetActive_Wall(Direction.North);
        if (direction.x == 1) SetActive_Wall(Direction.East);
        if (direction.y == -1) SetActive_Wall(Direction.South);
        if (direction.x == -1) SetActive_Wall(Direction.West);
    }
}