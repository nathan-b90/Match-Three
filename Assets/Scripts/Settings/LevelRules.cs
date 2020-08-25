using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "LevelRules", menuName = "Levels/Create New Level Rule Settings", order = 0)]
public sealed class LevelRules : ScriptableObject
{
    [Header("General")]

    [Tooltip("Ruleset index")]
    public int Index;

    [Tooltip("Game board prefab to use")]
    public Board GameBoard;

    [Space]

    [Header("Level goals (can have multiple goals)")]

    [Tooltip("Target score (0 - Not set)")]
    public int ScoreGoal;

    [Tooltip("Target locks to break (0 - Not set)")]
    public int LocksGoal;

    [Tooltip("Target rocks to smash (0 - Not set)")]
    public int RocksGoal;

    [Space]

    [Header("Gameplay features")]

    [Tooltip("Maximum moves before level failure")]
    public int MaximumMoves;

    [Tooltip("Custom tile distribution settings for this level")]
    public TileDistributionSettings TileDistributionSettings;

    public override string ToString()
    {
        return $"Level Settings " +
               $"{nameof(Index)}: {Index}, " +
               $"{nameof(GameBoard)}: {GameBoard}, " +
               $"{nameof(ScoreGoal)}: {ScoreGoal}, " +
               $"{nameof(LocksGoal)}: {LocksGoal}, " +
               $"{nameof(MaximumMoves)}: {MaximumMoves}, " +
               $"{nameof(TileDistributionSettings)}: {TileDistributionSettings}";
    }
}