using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Creates and holds a pot of tiles the game can use.
/// </summary>
public sealed class TilePot : MonoBehaviour
{
    /// <summary>
    /// Array of tile colours to be used on the board
    /// </summary>
    [SerializeField] private Tile[] tiles;

    /// <summary>
    /// The current level's rule settings.
    /// </summary>
    private LevelRules rules;

    /// <summary>
    /// Queue of tiles types to be selected from.
    /// </summary>
    private readonly Queue<TileSettings> randomisedTiles = new Queue<TileSettings>();

    /// <summary>
    /// Sets up a tile pot according to current level settings.
    /// </summary>
    /// <param name="rules"></param>
    public void ConfigurePot(LevelRules rules)
    {
        this.rules = rules;
        RegenerateDistribution();
    }

    private void RegenerateDistribution()
    {
        randomisedTiles.Clear();

        List<TileSettings> list = new List<TileSettings>();

        foreach (TileDistribution tileDistribution in rules.TileDistributionSettings.TileDistributions)
        {
            for (int i = 0; i < tileDistribution.DropChance; i++)
            {
                list.Add(tileDistribution.TileSettings);
            }
        }

        list.Shuffle();

        list.ForEach(element =>
        {
            randomisedTiles.Enqueue(element);
        });
    }

    /// <summary>
    /// Returns the next tile in the queue, according to the level's distribution.
    /// </summary>
    /// <returns></returns>
    public Tile GetNext()
    {
        int randomIndex = Random.Range(0, tiles.Length);
        Tile newTile = Instantiate(tiles[randomIndex], transform);

        if (randomisedTiles.Count.Equals(0))
        {
            RegenerateDistribution();
        }

        newTile.ConfigureTile(randomisedTiles.Dequeue());

        return newTile;
    }
}