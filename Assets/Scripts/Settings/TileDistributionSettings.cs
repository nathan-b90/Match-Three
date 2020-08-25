using UnityEngine;

[CreateAssetMenu(fileName = "TileDistributionSettings", menuName = "Tile Settings/Create New Tile Distribution Settings", order = 0)]
public sealed class TileDistributionSettings : ScriptableObject
{
    [Tooltip("")]
    public TileDistribution[] TileDistributions;
}