/// <summary>
/// Gameplay delegate event handlers.
/// </summary

#region gameplay

public delegate void SnakeChangedHandler(Snake snake);
public delegate void SnakeDoneHandler(Snake snake);
public delegate void LockDestroyedHandler();
public delegate void RockSmashedHandler();

#endregion

#region UI

public delegate void GameDataUpdateHandler(Data data);

#endregion