using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BoardCreatorGUI : EditorWindow
{
    int columns = 5;
    int rows = 5;

    [MenuItem("Custom Menu/Open Board Creator", priority = 0)]
    public static void OpenBoardCreationWindow()
    {
        BoardCreatorGUI boardCreationWindow = GetWindow<BoardCreatorGUI>("Board Creator");
        boardCreationWindow.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("BOARD CREATOR", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("This tool will create a new board prefab " +
            "according to the desired number of rows and columns. \n\n" +
            "Cells will be automatically arranged to fit the board.\n\n" +
            "Upon hitting create, the prefab will be created and loaded into the scene " +
            "and you will be editing the prefab itself with scene view open.\n\n" +
            "Here you can edit each cell individually.",
            EditorStyles.helpBox);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        columns = EditorGUILayout.IntField("Columns: ", columns);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        rows = EditorGUILayout.IntField("Rows: ", rows);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("CREATE"))
        {
            if (rows < 5 || columns < 5)
            {
                throw new Exception($"Board must have at least 5 rows & columns!");
            }

            EditorWindow sceneView = EditorWindow.GetWindow<SceneView>();
            sceneView.Focus();
            sceneView.Repaint();

            CreateBoardPrefab();

            BoardCreatorGUI boardCreationWindow = GetWindow<BoardCreatorGUI>("Board Creation");
            boardCreationWindow.Close();
        }
    }

    private void CreateBoardPrefab()
    {
        string blankBoardPath = $"Assets/Prefabs/BlankBoard.prefab";
        string copiedBoardPath = $"Assets/Prefabs/Boards/NewBoard{Random.Range(int.MinValue, 0)}.prefab";

        if (!AssertExistingAsset(blankBoardPath))
        {
            throw new Exception($"BlankBoard prefab needs to be at {blankBoardPath}!");
        }

        AssetDatabase.CopyAsset(blankBoardPath, copiedBoardPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GameObject boardHolder = GameObject.Find("BoardHolder");

        if (!boardHolder)
        {
            throw new Exception("Game scene must be open to create new boards!");
        }

        GameObject prefab = AssetDatabase.LoadAssetAtPath(copiedBoardPath, typeof(GameObject)) as GameObject;
        GameObject board = (GameObject)PrefabUtility.InstantiatePrefab(prefab, boardHolder.transform);

        SetupBoard(prefab, board);
    }

    private void SetupBoard(GameObject prefab, GameObject board)
    {
        string cellRoot = $"Assets/Prefabs/BoardElements/Cell.prefab";
        string spawnerRoot = $"Assets/Prefabs/BoardElements/Spawner.prefab";

        GameObject cellPrefab = AssetDatabase.LoadAssetAtPath(cellRoot, typeof(GameObject)) as GameObject;
        GameObject spawnerPrefab = AssetDatabase.LoadAssetAtPath(spawnerRoot, typeof(GameObject)) as GameObject;

        GridLayoutGroup grid = board.GetComponent<GridLayoutGroup>();

        // ensure child alignment is middle centre
        grid.childAlignment = TextAnchor.MiddleCenter;

        // ensure constraint is fixed column count
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

        // fix the column count according to columns
        grid.constraintCount = columns;

        float spacingX = grid.spacing.x * (columns - 1) + grid.padding.left + grid.padding.right;
        float spacingY = grid.spacing.y * (rows - 1) + grid.padding.top + grid.padding.bottom;

        // divide 650 (w) by the number of columns
        float cellWidth = (Constants.MAX_BOARD_WIDTH - spacingX) / columns;

        // divide 1000 (h) by the number of rows
        float cellHeight = (Constants.MAX_BOARD_HEIGHT - spacingY) / rows;

        // whichever outcome is lower, this is the cell height and width values
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        // set cell sizes
        grid.cellSize = new Vector2(cellSize, cellSize);

        // shift cells up to account for top row of spawners
        grid.padding.bottom = grid.padding.bottom + Mathf.RoundToInt(cellSize);

        Vector2 boardSize = new Vector2(cellSize * columns + spacingX, cellSize * rows + spacingY);

        board.GetComponent<RectTransform>().sizeDelta = boardSize;

        for (int i = 0; i < columns * rows; i++)
        {
            PrefabUtility.InstantiatePrefab(cellPrefab, board.transform);
        }

        for (int i = 0; i < columns; i++)
        {
            PrefabUtility.InstantiatePrefab(spawnerPrefab, board.transform);
        }

        Selection.activeObject = board;
        SceneView.lastActiveSceneView.FrameSelected();

        prefab.GetComponent<RectTransform>().sizeDelta = boardSize;

        // apply prefab changes, automated so they can't be undone
        PrefabUtility.ApplyPrefabInstance(board, InteractionMode.AutomatedAction);
        AssetDatabase.OpenAsset(prefab);
    }

    /// <summary>
    /// Returns true if asset exists at specified path.
    /// </summary>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    private bool AssertExistingAsset(string assetPath)
    {
        string assetName = Path.GetFileNameWithoutExtension(assetPath);
        string assetFolder = Path.GetDirectoryName(assetPath);

        if (AssetDatabase.FindAssets(assetName, new[] { assetFolder }).Length > 0)
        {
            return true;
        }

        return false;
    }
}