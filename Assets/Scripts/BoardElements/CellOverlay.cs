using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component controlling cell visuals according to state.
/// </summary>
public sealed class CellOverlay : MonoBehaviour
{
    [SerializeField] private Image overlay;
    [SerializeField] private Sprite lockedSprite, rockSprite;

    /// <summary>
    /// Sets initial overlay sprite at start of level.
    /// </summary>
    /// <param name="state"></param>
    public void SetOverlay(CellState state)
    {
        if (state == CellState.Locked)
        {
            overlay.sprite = lockedSprite;
            overlay.enabled = true;
        }

        if (state == CellState.Rock)
        {
            overlay.sprite = rockSprite;
            overlay.enabled = true;
        }
    }

    /// <summary>
    /// Removes overlay when locked cell has been unlocked or rock cell smashed.
    /// </summary>
    public void RemoveOverlay()
    {
        overlay.sprite = null;
        overlay.enabled = false;
    }
}