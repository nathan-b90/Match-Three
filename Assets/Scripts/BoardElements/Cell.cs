using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CellOverlay))]
public sealed class Cell : MonoBehaviour
{
    [SerializeField] private ParticleSystem tileDust, lockBreak, rockSmash;

    private CellState state;

    /// <summary>
    /// Returns is cell is disabled
    /// </summary>
    public bool IsDisabled => state == CellState.Disabled;

    /// <summary>
    /// Returns if cell is locked
    /// </summary>
    public bool IsLocked => state == CellState.Locked;

    /// <summary>
    /// Returns if cell is a rock
    /// </summary>
    public bool IsRock => state == CellState.Rock;

    private CellOverlay overlay;
    private Vector2 coordinates;
    private Tile tile;

    private int validAdjoinedCells;

    private void Awake()
    {
        CellSetup setup = GetComponent<CellSetup>();
        state = setup.cellState;
        Destroy(setup);

        overlay = GetComponent<CellOverlay>();
        overlay.SetOverlay(state);
    }

    #region cell and position on grid

    /// <summary>
    /// Sets the cell coordinates (column, row).
    /// </summary>
    /// <param name="coordinates"></param>
    public void SetCoordinates(Vector2 coordinates)
    {
        this.coordinates = coordinates;
    }

    /// <summary>
    /// Returns the cell coordinates (column, row)
    /// </summary>
    /// <returns></returns>
    public Vector2 GetCoordinates()
    {
        return coordinates;
    }

    /// <summary>
    /// Sets eight-direction joined cells and checks colour
    /// </summary>
    /// <param name="joiningCells"></param>
    public void SetJoiningCells(List<Cell> joiningCells)
    {
        validAdjoinedCells = 0;

        joiningCells.ForEach(joiningCell =>
        {
            if (joiningCell.GetTile() && joiningCell.GetTile().GetTileColour() == tile.GetTileColour())
            {
                validAdjoinedCells++;
            }
        });
    }

    /// <summary>
    /// Returns the adjoined cells which have a tile of the same colour
    /// </summary>
    /// <returns></returns>
    public int GetValidAdjoinedCells()
    {
        return validAdjoinedCells;
    }

    #endregion
    #region blocker

    /// <summary>
    /// Removes the lock or rock from a cell.
    /// </summary>
    public void RemoveBlocker()
    {
        if (IsLocked)
            lockBreak.Play();

        if (IsRock)
            rockSmash.Play();

        state = CellState.Standard;
        overlay.RemoveOverlay();
    }

    #endregion
    #region tile

    /// <summary>
    /// Removes tile from cell and returns it.
    /// </summary>
    /// <returns></returns>
    public Tile RetrieveTile()
    {
        Tile tile = this.tile;
        this.tile = null;
        return tile;
    }

    /// <summary>
    /// Sets current tile for cell.
    /// </summary>
    /// <param name="tile"></param>
    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }

    /// <summary>
    /// Nullifies tile.
    /// </summary>
    public void RemoveTile()
    {
        tile = null;
        tileDust.Play();
    }

    /// <summary>
    /// Returns the tile if it exists.
    /// </summary>
    public Tile GetTile()
    {
        return tile;
    }

    #endregion
}