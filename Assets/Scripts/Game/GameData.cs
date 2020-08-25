using System;

/// <summary>
/// Holds level progress information such as target/current score and moves left
/// </summary>
[Serializable]
public struct Data
{
    public int scoreGoal, score;

    public int locksGoal, locks;

    public int rocksGoal, rocks;

    public int movesLeft;
}