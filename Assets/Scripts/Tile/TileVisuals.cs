using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component controlling a tile's visuals.
/// </summary>
public sealed class TileVisuals : MonoBehaviour
{
    [Header("Assign tile sprites here (hover over for more info)")]
    [Tooltip("Element 0: Standard, Element 1: Cross, Element 2: Circle, Element 3: Square, Element 4: Triangle, Element 5: Star")]
    [SerializeField] private Sprite[] tileSprites;

    private Animator tileAnimator;

    private Image image;

    private void Awake()
    {
        tileAnimator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Sets tile symbol according to settings.
    /// </summary>
    /// <param name="settings"></param>
    public void SetTileVisuals(TileSettings settings)
    {
        image.sprite = tileSprites[(int)settings.TileType];
    }

    public void DimTile(bool dimmed)
    {
        tileAnimator.SetBool(Constants.TILE_ANIM_DIM_TRIGGER, dimmed);
    }

    public void SelectTile(bool selected)
    {
        tileAnimator.SetBool(Constants.TILE_ANIM_SELECT_TRIGGER, selected);
    }
}