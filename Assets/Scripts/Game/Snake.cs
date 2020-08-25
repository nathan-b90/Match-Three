using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Snake object, which holds selected tiles.
/// </summary>
[Serializable]
public sealed class Snake
{
    private readonly List<Tile> tiles = new List<Tile>();

    private float multiplier;
    private int totalScore;

    public void Add(Tile tile)
    {
        tiles.Add(tile);
        tile.SelectTile(true);

        float pitch = 1 + ((float)tiles.Count / 10);
        AudioManager.Instance.PlayAudio(Sound.TileSelect, pitch);

        ScoreSnake();
    }

    public void RemoveLast()
    {
        tiles[tiles.Count - 1].SelectTile(false);
        tiles.RemoveAt(tiles.Count - 1);

        AudioManager.Instance.PlayAudio(Sound.TileSelect, 0.5f);

        ScoreSnake();
    }

    private void ScoreSnake()
    {
        int baseScore = tiles.Sum(element => element.GetBaseScore());
        multiplier = Mathf.Clamp(tiles.Sum(element => element.GetMultiplier()), 1, Mathf.Infinity);

        totalScore = Mathf.RoundToInt(baseScore * multiplier);
    }

    public bool Contains(Tile tile)
    {
        return tiles.Contains(tile);
    }

    public void Clear()
    {
        tiles.Clear();
    }

    public Tile GetTileAt(int index)
    {
        return tiles[index];
    }

    public int GetCount()
    {
        return tiles.Count();
    }

    public bool IsTileValidToRemove(Tile tile)
    {
        if (tiles.Count < 2)
            return false;

        return tiles.Contains(tile) && tiles[tiles.Count - 2].Equals(tile);
    }

    public bool IsTileValidToAdd(Tile tile, Grid grid)
    {
        bool isNotAdded = !tiles.Contains(tile);
        bool isSameColour = tiles.Last().GetTileColour().Equals(tile.GetTileColour());
        bool isAdjacent = grid.IsCellAdjacent(tiles.Last().GetTileCell(), tile.GetTileCell());

        return isNotAdded && isSameColour && isAdjacent;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}