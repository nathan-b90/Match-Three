using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Sets up the board, cells & spawners
/// </summary>
public sealed class Grid : MonoBehaviour
{
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public List<Cell> Cells { get; private set; }
    public List<Spawn> Spawns { get; private set; }

    /// <summary>
    /// Gathers all cells and spawners on board.
    /// </summary>
    /// <returns></returns>
    public void BuildBoard()
    {
        Cells = GetComponentsInChildren<Cell>().ToList();
        Spawns = GetComponentsInChildren<Spawn>().ToList();

        for (int index = 0; index < Cells.Count; index++)
        {
            if (Columns.Equals(Spawns.Count))
            {
                Rows++;
                Columns = 0;
            }

            Cells[index].SetCoordinates(new Vector2(Columns, Rows));
            Columns++;
        }

        Rows++;
    }

    /// <summary>
    /// Sets valid adjoining cells for all cells on grid
    /// </summary>
    public void SetAllAdjoiningCells()
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            if (!Cells[i].GetTile())
                continue;

            Cells[i].SetJoiningCells(GetEightAdjacentCells(Cells[i].GetCoordinates()));
        }
    }

    /// <summary>
    /// Returns true if there is a valid move on the board
    /// </summary>
    /// <returns></returns>
    public bool IsDeadLocked()
    {
        if (Cells.Any(cell => cell.GetValidAdjoinedCells() > 1))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Returns the cell at a specified coordinate.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public Cell GetCellAtCoordinate(Vector2 coordinates)
    {
        foreach (Cell cell in Cells)
        {
            if (cell.GetCoordinates().Equals(coordinates))
            {
                return cell;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the lowest empty cell in the same column.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public Cell GetLowestEmptyCellInColumn(Vector2 coordinates)
    {
        Cell cell;

        for (int i = 0; i < coordinates.y; i++)
        {
            cell = GetCellAtCoordinate(new Vector2(coordinates.x, i));

            if (!cell.GetTile() && !cell.IsDisabled && !cell.IsRock)
            {
                return cell;
            }
        }

        return null;
    }

    private readonly Vector2[] fourWayCells = new Vector2[]
     {
        new Vector2(-1, 0), // left
        new Vector2(0, 1), // top
        new Vector2(1, 0), // right
        new Vector2(0, -1) // bottom
     };

    private readonly Vector2[] eightWayCells = new Vector2[]
     {
        new Vector2(-1, 0), // left
        new Vector2(-1, 1), // top left
        new Vector2(0, 1), // top
        new Vector2(1, 1), // top right
        new Vector2(1, 0), // right
        new Vector2(1, -1), // bottom right
        new Vector2(0, -1), // bottom
        new Vector2(-1, -1) // bottom left
     };

    /// <summary>
    /// Returns 4 cells up, down, left & right of coordinates
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public List<Cell> GetFourAdjacentCells(Vector2 coordinates)
    {
        List<Cell> cells = new List<Cell>();

        for (int i = 0; i < 4; i++)
        {
            Cell cell = GetCellAtCoordinate(new Vector2(
                coordinates.x + fourWayCells[i].x,
                coordinates.y + fourWayCells[i].y));

            if (cell)
                cells.Add(cell);
        }

        return cells;
    }

    /// <summary>
    /// Returns all 8 cells around coordinates
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public List<Cell> GetEightAdjacentCells(Vector2 coordinates)
    {
        List<Cell> cells = new List<Cell>();

        for (int i = 0; i < 8; i++)
        {
            Cell cell = GetCellAtCoordinate(new Vector2(
                coordinates.x + eightWayCells[i].x,
                coordinates.y + eightWayCells[i].y));

            if (cell)
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    /// <summary>
    /// Returns true if toCell is connected to fromCell
    /// </summary>
    /// <param name="fromCell"></param>
    /// <param name="toCell"></param>
    /// <returns></returns>
    public bool IsCellAdjacent(Cell fromCell, Cell toCell)
    {
        float horizontalDistance = Mathf.Abs(fromCell.GetCoordinates().x - toCell.GetCoordinates().x);
        float verticalDistance = Mathf.Abs(fromCell.GetCoordinates().y - toCell.GetCoordinates().y);

        return horizontalDistance == 1 && verticalDistance == 0 ||
            horizontalDistance == 0 && verticalDistance == 1 ||
            horizontalDistance == 1 && verticalDistance == 1;
    }

    #region animations

    /// <summary>
    /// Dims all tiles that don't match the selected tile's colour.
    /// </summary>
    public void DimInvalidTiles(TileColour tileColour)
    {
        Tile tile;

        foreach (Cell cell in Cells)
        {
            tile = cell.GetTile();

            if (tile)
            {
                if (tile.GetTileColour() != tileColour)
                {
                    tile.DimTile(true);
                }
            }
        }
    }

    /// <summary>
    /// Removes dim from all active tiles.
    /// </summary>
    public void BrightenAllTiles()
    {
        Tile tile;

        foreach (Cell cell in Cells)
        {
            tile = cell.GetTile();

            if (tile)
            {
                tile.DimTile(false);
            }
        }
    }

    #endregion

}