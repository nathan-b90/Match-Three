using UnityEngine;
using TMPro;

/// <summary>
/// Class to control the dinosaur atop the board
/// </summary>
public sealed class Dino : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText, multiplierText;

    private Animator dinoAnimator;

    private void Awake()
    {
        dinoAnimator = GetComponent<Animator>();
    }

    public void AppendScoreText(Snake snake)
    {
        if (snake.GetCount() < 3)
        {
            ClearScoreText();
        }
        else
        {
            totalScoreText.text = snake.GetTotalScore().ToString();

            multiplierText.text = snake.GetMultiplier() > 1
                ? $"x{snake.GetMultiplier()}"
                : null;
        }
    }

    public void ClearScoreText()
    {
        totalScoreText.text = multiplierText.text = null;
    }

    public void DinoJump()
    {
        dinoAnimator.SetTrigger("Jump");
    }
}