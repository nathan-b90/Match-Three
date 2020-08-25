using UnityEngine;
using System.Collections;

/// <summary>
/// Main board class, controlling board input & gameplay.
/// </summary>
[RequireComponent(typeof(Grid))]
public sealed class Board : MonoBehaviour
{
    /// <summary>
    /// Event fired when a tile is selected and added to snake.
    /// </summary>
    public event SnakeChangedHandler OnSnakeChanged;

    /// <summary>
    /// Event fired when a snake is submitted and cleared from the board.
    /// </summary>
    public event SnakeDoneHandler OnSnakeDone;

    /// <summary>
    /// Event fired when a lock is destroyed.
    /// </summary>
    public event LockDestroyedHandler OnLockDestroyed;

    /// <summary>
    /// Event fired when a rock is smashed.
    /// </summary>
    public event RockSmashedHandler OnRockSmashed;

    private Grid grid;
    private TilePot tilePot;
    private Snake snake;

    private bool isSelectionStarted, isSelectionEnabled;

    /// <summary>
    /// Sets up board according to rules
    /// </summary>
    /// <param name="rules"></param>
    public void InitBoard(LevelRules rules)
    {
        grid = GetComponent<Grid>();
        grid.BuildBoard();

        tilePot = GetComponent<TilePot>();
        tilePot.ConfigurePot(rules);

        SpawnFreshBoard();
    }

    #region board creation & maintenance

    private void SpawnFreshBoard()
    {
        foreach (Cell cell in grid.Cells)
        {
            Tile tile = cell.GetTile();

            if (tile)
            {
                cell.RemoveTile();
                Destroy(tile.gameObject);
            }
        }

        StartCoroutine(FillCells());
    }

    private IEnumerator FillCells()
    {
        yield return new WaitForEndOfFrame();

        foreach (Cell cell in grid.Cells)
        {
            if (cell.IsDisabled || cell.IsRock)
                continue;

            if (!cell.GetTile())
            {
                Tile newTile = tilePot.GetNext();
                newTile.transform.SetParent(grid.Spawns[(int)cell.GetCoordinates().x].transform);
                newTile.transform.localPosition = Vector3.zero;
                newTile.SetCell(cell);

                yield return new WaitForEndOfFrame();
            }
        }

        grid.SetAllAdjoiningCells();

        if (grid.IsDeadLocked()) // if no valid moves, refresh board
        {
            SpawnFreshBoard();
            yield break;
        }

        isSelectionEnabled = true;
    }

    private IEnumerator ClearSnakeTiles()
    {
        yield return new WaitForEndOfFrame();

        AudioManager.Instance.PlayAudio(Sound.SnakeDestroyed);

        for (int i = 0; i < snake.GetCount(); i++)
        {
            Tile tile = snake.GetTileAt(i);
            Cell tileCell = tile.GetTileCell(); // get cell under tile

            // cell blocker checks
            if (tileCell.IsLocked)
            {
                tileCell.RemoveBlocker();
                OnLockDestroyed?.Invoke();
            }

            grid.GetFourAdjacentCells(tileCell.GetCoordinates()).ForEach(cell =>
            {
                if (cell && cell.IsRock)
                {
                    cell.RemoveBlocker();
                    OnRockSmashed?.Invoke();
                }
            });

            tileCell.RemoveTile();
            Destroy(tile.gameObject);

            yield return new WaitForSeconds(0.05f);
        }

        OnSnakeDone?.Invoke(snake);

        StartCoroutine(ShiftTilesDown());
    }

    private IEnumerator ShiftTilesDown()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < grid.Columns; i++)
        {
            for (int j = 0; j < grid.Rows; j++)
            {
                Cell cell = grid.GetCellAtCoordinate(new Vector2(i, j));

                if (cell.IsDisabled || cell.IsRock)
                    continue;

                if (cell.GetTile())
                {
                    Cell lowestEmptyCell = grid.GetLowestEmptyCellInColumn(new Vector2(i, j));

                    if (lowestEmptyCell)
                    {
                        Tile tile = cell.RetrieveTile();
                        tile.SetCell(lowestEmptyCell);
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        StartCoroutine(FillCells());
    }

    #endregion

    #region selection methods

    /// <summary>
    /// On tile tap, start a selection
    /// </summary>
    public void SelectionStart()
    {
        if (!isSelectionEnabled)
            return;

        isSelectionStarted = true;
        snake = new Snake(); // init new snake
    }

    /// <summary>
    /// Select a valid tile and add it to snake
    /// </summary>
    /// <param name="tile"></param>
    public void TileSelection(Tile tile)
    {
        if (!isSelectionStarted)
            return;

        if (snake.GetCount().Equals(0))
        {
            snake.Add(tile);
            grid.DimInvalidTiles(tile.GetTileColour());
        }
        else
        {
            if (snake.IsTileValidToAdd(tile, grid))
            {
                snake.Add(tile);
            }

            if (snake.IsTileValidToRemove(tile))
            {
                snake.RemoveLast();
            }
        }

        OnSnakeChanged?.Invoke(snake);
    }

    /// <summary>
    /// On mouse up, end a snake selection
    /// </summary>
    public void SelectionEnd()
    {
        if (!isSelectionStarted)
            return;

        isSelectionStarted = false;
        grid.BrightenAllTiles();

        if (snake.GetCount() < 3)
        {
            snake.Clear();
            return;
        }

        isSelectionEnabled = false;

        StartCoroutine(ClearSnakeTiles());
    }

    #endregion
}