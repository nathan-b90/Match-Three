using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public sealed class TileDistribution
{
    [Tooltip("Type of tile that will be spawned on a board using this distribution")]
    public TileSettings TileSettings;

    [Tooltip("Chance of this tile being spawned, relative to the total drop chance of all tiles in this distribution")]
    public int DropChance;
}