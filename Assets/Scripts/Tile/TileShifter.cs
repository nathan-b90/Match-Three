using UnityEngine;
using System.Collections;

/// <summary>
/// Component that grants the movement of a tile to a target cell.
/// </summary>
public sealed class TileShifter : MonoBehaviour
{
    [SerializeField] private TileAnimationSettings tileAnimationSettings;

    public IEnumerator MoveTile()
    {
        float timeToMove = 0;

        while (timeToMove < tileAnimationSettings.tileFallMaxTime)
        {
            timeToMove += Time.deltaTime * tileAnimationSettings.tileFallSpeed;
            float curvePercent = tileAnimationSettings.tileFallCurve.Evaluate(timeToMove / tileAnimationSettings.tileFallMaxTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, curvePercent);

            yield return null;
        }

        yield break;
    }
}