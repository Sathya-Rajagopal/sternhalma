using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GridManager Instance;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;
    private Dictionary<Tile, Vector2> _poses;

    private void Awake()
    {
        Instance = this;
    }


    //void Start()
    //{
    //    GenerateGrid();
    //}

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        _poses = new Dictionary<Tile, Vector2>();

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";

                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(i, j)] = spawnedTile;
                _poses[spawnedTile] = new Vector2(i, j);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -20);
        GameManager.Instance.ChangeState(GameState.SpawnObjects);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }


    public Vector2 GetPosOfTile(Tile tile)
    {
        if (_poses.TryGetValue(tile, out var pos))
        {
            return pos;
        }
        return new Vector2(-1, -1);
    }
}

