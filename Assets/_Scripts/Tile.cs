using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _highlightOnSelect;

    public BaseUnit OccupiedUnit;
    public bool Walkable => OccupiedUnit == null;

    public void Init(bool isOffset)

    
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    private void OnMouseDown()
    {
        //Debug.Log("1");

        if (GameManager.Instance.GameState != GameState.PlayerTurn)
        {
            return;
        }

        BaseUnit selectedBaseUnit = UnitManager.Instance.selectedUnit;
        Tile selectedTile = UnitManager.Instance.selectedTile;
        //Debug.Log(selectedBaseUnit);

        if (OccupiedUnit == null && selectedBaseUnit == null)
        {
            UnitManager.Instance.setSelectedUnit(null);
            UnitManager.Instance.setSelectedTile(null);
            return;
        }

        else if (OccupiedUnit != null && selectedBaseUnit != null)
        {
            selectedTile._highlightOnSelect.SetActive(false);
            UnitManager.Instance.setSelectedUnit(null);
            UnitManager.Instance.setSelectedTile(null);

            return;
        }

        else if (OccupiedUnit != null && selectedBaseUnit == null)
        {
            UnitManager.Instance.setSelectedUnit((BaseUnit)OccupiedUnit);
            UnitManager.Instance.setSelectedUnitCoordinate(GridManager.Instance.GetPosOfTile(OccupiedUnit.OccupiedTile));
            UnitManager.Instance.setSelectedTile(OccupiedUnit.OccupiedTile);
            selectedTile = UnitManager.Instance.selectedTile;

            //Debug.Log("selected tile is");
            //Debug.Log(selectedTile);

            selectedTile._highlightOnSelect.SetActive(true);
            }

        else
        {
            Vector2 currentCoordinate = GridManager.Instance.GetPosOfTile(this);
            Vector2 selectedCoordinate = UnitManager.Instance.selectedUnitCoordinate;

            if ((((int)(currentCoordinate.x - selectedCoordinate.x) == 0) &&
                ((int)Math.Abs(currentCoordinate.y - selectedCoordinate.y) == 2)) ||
                (((int)(currentCoordinate.y - selectedCoordinate.y) == 0) &&
                ((int)Math.Abs(currentCoordinate.x - selectedCoordinate.x) == 2)))
            {

                if (selectedBaseUnit.Faction == Faction.Paper)
                {
                    var midX = (int)((currentCoordinate.x + selectedCoordinate.x) / 2);
                    var midY = (int)((currentCoordinate.y + selectedCoordinate.y) / 2);
                    Tile midTile = GridManager.Instance.GetTileAtPosition(new Vector2(midX, midY));
                    BaseUnit midUnit = UnitManager.Instance.GetBaseUnitFromTile(midTile);

                    if ((midUnit != null) && (midUnit.Faction == Faction.Rock))
                    {
                        SetUnit(UnitManager.Instance.selectedUnit);
                        UnitManager.Instance.UpdateTiletoUnitDict(selectedBaseUnit.OccupiedTile, OccupiedUnit.OccupiedTile, midTile, selectedBaseUnit);
                        UnitManager.Instance.setSelectedUnit(null);
                        Destroy(midUnit.gameObject);
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedTile(null);
                    }

                    else
                    {
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedUnit(null);
                        UnitManager.Instance.setSelectedTile(null);
                        return;
                    }
                }

                else if (selectedBaseUnit.Faction == Faction.Rock)
                {
                    var midX = (int)((currentCoordinate.x + selectedCoordinate.x) / 2);
                    var midY = (int)((currentCoordinate.y + selectedCoordinate.y) / 2);
                    Tile midTile = GridManager.Instance.GetTileAtPosition(new Vector2(midX, midY));
                    BaseUnit midUnit = UnitManager.Instance.GetBaseUnitFromTile(midTile);

                    if ((midUnit != null) && (midUnit.Faction == Faction.Scissor))
                    {
                        SetUnit(UnitManager.Instance.selectedUnit);
                        UnitManager.Instance.UpdateTiletoUnitDict(selectedBaseUnit.OccupiedTile, OccupiedUnit.OccupiedTile, midTile, selectedBaseUnit);
                        UnitManager.Instance.setSelectedUnit(null);
                        Destroy(midUnit.gameObject);
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedTile(null);
                    }

                    else
                    {
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedUnit(null);
                        UnitManager.Instance.setSelectedTile(null);
                        return;
                    }
                }

                else if (selectedBaseUnit.Faction == Faction.Scissor)
                {
                    var midX = (int)((currentCoordinate.x + selectedCoordinate.x) / 2);
                    var midY = (int)((currentCoordinate.y + selectedCoordinate.y) / 2);
                    Tile midTile = GridManager.Instance.GetTileAtPosition(new Vector2(midX, midY));
                    BaseUnit midUnit = UnitManager.Instance.GetBaseUnitFromTile(midTile);

                    if ((midUnit != null) && (midUnit.Faction == Faction.Paper))
                    {
                        SetUnit(selectedBaseUnit);

                        UnitManager.Instance.UpdateTiletoUnitDict(selectedBaseUnit.OccupiedTile, OccupiedUnit.OccupiedTile, midTile, selectedBaseUnit);
                        UnitManager.Instance.setSelectedUnit(null);

                        Destroy(midUnit.gameObject);
                        //Debug.Log("highlited tile is");
                        //Debug.Log(selectedTile);
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedTile(null);
                    }

                    else
                    {
                        selectedTile._highlightOnSelect.SetActive(false);
                        UnitManager.Instance.setSelectedUnit(null);
                        UnitManager.Instance.setSelectedTile(null);
                        return;
                    }
                }

                else
                {
                    selectedTile._highlightOnSelect.SetActive(false);
                    UnitManager.Instance.setSelectedUnit(null);
                    UnitManager.Instance.setSelectedTile(null);
                    return;
                }
            }

            else
            {
                selectedTile._highlightOnSelect.SetActive(false);
                UnitManager.Instance.setSelectedUnit(null);
                UnitManager.Instance.setSelectedTile(null);
                return;
            }
        }
    }
}
