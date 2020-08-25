using UnityEngine;

/// <summary>
/// Disposable component granting cell setup in edit mode.
/// </summary>
[ExecuteInEditMode]
[DisallowMultipleComponent]
public sealed class CellSetup : MonoBehaviour
{
    public CellState cellState;

    private void OnDrawGizmos()
    {
        switch (cellState)
        {
            case CellState.Disabled:
                Gizmos.DrawIcon(transform.position, "disabled_cell.png", false);
                break;

            case CellState.Locked:
                Gizmos.DrawIcon(transform.position, "locked_cell.png", false);
                break;

            case CellState.Rock:
                Gizmos.DrawIcon(transform.position, "blocked.png", false);
                break;

            default:
                Gizmos.DrawIcon(transform.position, "active_cell.png", false);
                break;
        }
    }
}