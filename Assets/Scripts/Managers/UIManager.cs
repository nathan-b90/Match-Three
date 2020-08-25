using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manager for the in-game UI
/// </summary>
public sealed class UIManager : Manager<UIManager>
{
    // moves
    [SerializeField] private TextMeshProUGUI movesText;

    // score
    [SerializeField] private Image scoreBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    // other goals
    [SerializeField] private LevelGoal locksGoal, rocksGoal;

    /// <summary>
    /// Set up UI when new level is started
    /// </summary>
    /// <param name="data"></param>
    public void SetupGameUI(Data data)
    {
        scoreBar.gameObject.SetActive(data.scoreGoal > 0);
        locksGoal.SetupGoal(data.locksGoal > 0);
        rocksGoal.SetupGoal(data.rocksGoal > 0);

        UpdateUI(data);
    }

    private void Start()
    {
        // Subscribe for updates
        GameManager.Instance.OnGameDataUpdated += UpdateUI;
    }

    private void UpdateUI(Data data)
    {
        if (movesText != null)
        {
            movesText.text = $"{data.movesLeft}";
        }

        if (scoreText != null && scoreBar != null)
        {
            scoreText.text = !data.scoreGoal.Equals(0) ?
                $"{data.score}/{data.scoreGoal}" :
                $"{data.score}";

            UpdateScoreBar(data.score, data.scoreGoal);
        }

        if (data.locksGoal > 0 && locksGoal != null)
        {
            locksGoal.UpdateGoal(data.locks, data.locksGoal);
        }

        if (data.rocksGoal > 0 && rocksGoal != null)
        {
            rocksGoal.UpdateGoal(data.rocks, data.rocksGoal);
        }
    }

    private void UpdateScoreBar(int score, int scoreGoal)
    {
        float f = scoreGoal / 100;
        scoreBar.fillAmount = score / f / 100;
    }
}