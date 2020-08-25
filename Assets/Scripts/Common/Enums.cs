/// <summary>
/// Game scene names - used to call load operations
/// </summary>
public enum GameScene
{
    MainMenu,
    LevelSelect,
    Game
}

public enum CellState
{
    Standard,
    Disabled,
    Locked,
    Rock
}

public enum TileColour
{
    Yellow,
    Pink,
    Green,
    Blue,
    Orange,
    Purple
}

public enum TileType
{
    Standard,
    Cross,
    Circle,
    Square,
    Triangle,
    Star
}

/// <summary>
/// Sound identifiers
/// </summary>
public enum Sound
{
    TileSelect,
    SnakeDestroyed,
    LockSmash,
    RockBreak,
    WinSound,
    ButtonPress,
    ObjectiveComplete,
    LoseSound,
    LevelIntro
}