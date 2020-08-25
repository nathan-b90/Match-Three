using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PlayerPrefsGUI : EditorWindow
{
    int levelIndex = 0;

    [MenuItem("Custom Menu/Open Player Prefs Window", priority = 1)]
    public static void OpenPlayerPrefsWindow()
    {
        PlayerPrefsGUI PlayerPrefsWindow = GetWindow<PlayerPrefsGUI>("Player Prefs");
        PlayerPrefsWindow.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("PLAYER PREFS", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("All indexes start at zero (0 is level 1)", EditorStyles.helpBox);

        EditorGUILayout.Space();

        int progress = PlayerPrefs.GetInt(Constants.LEVEL_PROGRESS_KEY, 0);

        EditorGUILayout.LabelField($"Current progress: {progress}", EditorStyles.helpBox);

        EditorGUILayout.Space();

        if (GUILayout.Button("Wipe all game progress"))
        {
            PlayerPrefs.DeleteKey(Constants.LEVEL_PROGRESS_KEY);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Set starting level:", EditorStyles.helpBox);

        levelIndex = EditorGUILayout.IntField("Level index: ", levelIndex);

        if (GUILayout.Button("Set player progress"))
        {
            PlayerPrefs.SetInt(Constants.LEVEL_PROGRESS_KEY, levelIndex);
        }
    }
}