using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Represents a level goal (which isn't score) on the UI
/// </summary>
[Serializable]
public sealed class LevelGoal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalText;

    [SerializeField] private GameObject goalTick;

    /// <summary>
    /// Shows/hides goal UI if level has goal
    /// </summary>
    /// <param name="hasGoal"></param>
    public void SetupGoal(bool hasGoal)
    {
        gameObject.SetActive(hasGoal);
        goalTick.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="score"></param>
    /// <param name="scoreGoal"></param>
    public void UpdateGoal(int score, int scoreGoal)
    {
        goalText.text = $"{score}/{scoreGoal}";

        if (score >= scoreGoal)
        {
            SetGoalAchieved();
        }
    }

    private void SetGoalAchieved()
    {
        goalTick.SetActive(true);
        AudioManager.Instance.PlayAudio(Sound.ObjectiveComplete);
    }
}