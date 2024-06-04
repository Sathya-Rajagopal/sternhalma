using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> _units;
    public BaseUnit selectedUnit;
    public Vector2 selectedUnitCoordinate;
    public Tile selectedTile;
    public Dictionary<Tile, BaseUnit> tileToBaseUnit;

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void spawnObjects()
    {

        tileToBaseUnit = new Dictionary<Tile, BaseUnit>();

        List<Vector2> scissorList = new List<Vector2> {new Vector2(2, 0), new Vector2(0, 2), new Vector2(3, 3)};
        var scissorCount = scissorList.Count;

        for (int i = 0; i < scissorCount; i++)
        {
            var scissorPrefab = GetUnit<Scissor1>(Faction.Scissor);
            BaseUnit spawnedScissor = Instantiate(scissorPrefab);
            Tile scissorSpawnTile = GridManager.Instance.GetTileAtPosition(scissorList[i]);

            tileToBaseUnit.Add(scissorSpawnTile, spawnedScissor);

            scissorSpawnTile.SetUnit(spawnedScissor);
        }

        List<Vector2> rockList = new List<Vector2> { new Vector2(0, 1), new Vector2(1, 0)};
        var rockCount = rockList.Count;

        for (int i = 0; i < rockCount; i++)
        {
            var rockPrefab = GetUnit<Rock1>(Faction.Rock);
            BaseUnit spawnedrock = Instantiate(rockPrefab);
            Tile rockSpawnTile = GridManager.Instance.GetTileAtPosition(rockList[i]);

            tileToBaseUnit[rockSpawnTile] = spawnedrock;

            rockSpawnTile.SetUnit(spawnedrock);
        }
        List<Vector2> paperList = new List<Vector2> {new Vector2(0, 0), new Vector2(1, 2), new Vector2(3, 2)};
        var paperCount = paperList.Count;

        for (int i = 0; i < paperCount; i++)
        {
            var paperPrefab = GetUnit<Paper1>(Faction.Paper);
            BaseUnit spawnedpaper = Instantiate(paperPrefab);
            Tile paperSpawnTile = GridManager.Instance.GetTileAtPosition(paperList[i]);

            tileToBaseUnit[paperSpawnTile] = spawnedpaper;

            paperSpawnTile.SetUnit(spawnedpaper);
        }

        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }

    private T GetUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).First().UnitPrefab;
    }

    public void setSelectedUnit(BaseUnit unit)
    {
        selectedUnit = unit;
    }

    public void setSelectedUnitCoordinate(Vector2 coordinate)
    {
        selectedUnitCoordinate = coordinate;
    }

    public void setSelectedTile(Tile tile)
    {
        selectedTile = tile;
    }

    public BaseUnit GetBaseUnitFromTile(Tile tile)
    {
        if (tileToBaseUnit.TryGetValue(tile, out var unit))
        {
            return unit;
        }
        return null;
    }

    public void UpdateTiletoUnitDict(Tile selectedTile, Tile OccupiedTile, Tile midTile, BaseUnit unit)
    {
        tileToBaseUnit.Remove(selectedTile);
        tileToBaseUnit.Remove(midTile);
        tileToBaseUnit.Add(OccupiedTile, unit);
    }
}
