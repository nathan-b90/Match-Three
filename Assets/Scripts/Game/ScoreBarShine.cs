using UnityEngine;

/// <summary>
/// Simple X movement animation.
/// </summary>
public sealed class ScoreBarShine : MonoBehaviour
{
    [SerializeField]
    private RectTransform shineImage;

    [SerializeField, Tooltip("How ofter a shine appears")]
    private float period;

    [SerializeField, Tooltip("How fast a shine moves")]
    private float speed;

    private Vector2 from, to;
    private float timeSeed;
    private float t;

    private float startX;

    private void Start()
    {
        startX = shineImage.localPosition.x;
        timeSeed = Random.value;
    }

    void Update()
    {
        to = new Vector2(period * speed + startX, 0);
        from = new Vector2(startX, 0);

        t = (Time.time) % period;
        shineImage.anchoredPosition = Vector2.Lerp(from, to, t / period);
    }
}