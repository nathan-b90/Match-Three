using UnityEngine;

/// <summary>
/// Base prefab for a game tile.
/// </summary>
[RequireComponent(typeof(TileShifter))]
public sealed class Tile : MonoBehaviour
{
    [SerializeField] private TileColour tileColour;

    private TileSettings settings;

    private TileShifter shifter;
    private TileVisuals visuals;

    private Cell cell;

    private Board board;

    private void Awake()
    {
        // gather components
        shifter = GetComponent<TileShifter>();
        visuals = GetComponentInChildren<TileVisuals>();
        board = GetComponentInParent<Board>();
    }

    /// <summary>
    /// Set up a new tile with settings.
    /// </summary>
    /// <param name="settings"></param>
    public void ConfigureTile(TileSettings settings)
    {
        this.settings = settings;
        visuals.SetTileVisuals(settings);
    }

    /// <summary>
    /// Set tile's cell and move tile to cell.
    /// </summary>
    /// <param name="cell"></param>
    public void SetCell(Cell cell)
    {
        this.cell = cell;
        cell.SetTile(this);

        transform.SetParent(cell.transform);
        transform.SetAsFirstSibling();

        GetComponent<RectTransform>().sizeDelta = cell.GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().transform.localScale = Vector2.one;

        StartCoroutine(shifter.MoveTile());
    }

    #region input actions

    private void OnMouseDown()
    {
        board.SelectionStart();
        OnMouseEnter();
    }

    private void OnMouseEnter()
    {
        board.TileSelection(this);
    }

    private void OnMouseUp()
    {
        board.SelectionEnd();
    }

    #endregion

    public void DimTile(bool dim)
    {
        visuals.DimTile(dim);
        visuals.SelectTile(false);
    }

    public void SelectTile(bool select)
    {
        visuals.SelectTile(select);
    }

    #region accessors

    public Cell GetTileCell()
    {
        return cell;
    }

    public TileColour GetTileColour()
    {
        return tileColour;
    }

    public TileType GetTileType()
    {
        return settings.TileType;
    }

    public int GetBaseScore()
    {
        return settings.BaseScore;
    }

    public float GetMultiplier()
    {
        return settings.Multiplier;
    }

    #endregion
}