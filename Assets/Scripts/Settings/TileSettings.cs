using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "TileSettings", menuName = "Tile Settings/Create New Tile Settings", order = 0)]
public sealed class TileSettings : ScriptableObject
{
    [Tooltip("The shape type of the tile")]
    public TileType TileType;

    [Tooltip("Base score on tile selection")]
    public int BaseScore;

    [Tooltip("Score multiplier on tile selection (anything below 1 will default to x1)")]
    public float Multiplier;
}