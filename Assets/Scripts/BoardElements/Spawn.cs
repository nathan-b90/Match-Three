using UnityEngine;

[ExecuteInEditMode]
public sealed class Spawn : MonoBehaviour
{
    public bool isSpawn = true;

    private void OnDrawGizmos()
    {
        if (!isSpawn)
        {
            Gizmos.DrawIcon(transform.position, "blocked.png", false);
        }
    }
}
