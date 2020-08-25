using UnityEngine.SceneManagement;
using JetBrains.Annotations;

/// <summary>
/// Manager for the main menu
/// </summary>
public sealed class MainMenuManager : Manager<MainMenuManager>
{
    [UsedImplicitly] // by play button
    public void LoadGame()
    {
        SceneManager.LoadScene(GameScene.Game.ToString());
        SceneManager.LoadScene(GameScene.LevelSelect.ToString(), LoadSceneMode.Additive);
    }
}