using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Main game manager
/// </summary>
public sealed class GameManager : Manager<GameManager>
{
    [SerializeField] private Dino dino;
    [SerializeField] private Transform boardHolder;

    private LevelRules rules;
    private Data data;
    private Board gameBoard;

    public event GameDataUpdateHandler OnGameDataUpdated;

    public void Configure(LevelRules rules)
    {
        if (!rules || !rules.GameBoard)
        {
            throw new Exception("Invalid ruleset / game board!");
        }

        this.rules = rules;

        InitNewData();
        InitGameBoard();

        AudioManager.Instance.PlayAudio(Sound.LevelIntro);
    }

    private void InitNewData()
    {
        data = new Data
        {
            scoreGoal = rules.ScoreGoal,
            locksGoal = rules.LocksGoal,
            rocksGoal = rules.RocksGoal,
            movesLeft = rules.MaximumMoves
        };

        UIManager.Instance.SetupGameUI(data);
    }

    private void InitGameBoard()
    {
        gameBoard = Instantiate(rules.GameBoard, boardHolder);

        gameBoard.OnSnakeChanged += SnakeChanged;
        gameBoard.OnSnakeDone += SnakeDone;
        gameBoard.OnLockDestroyed += LockDestroyed;
        gameBoard.OnRockSmashed += RockSmashed;

        gameBoard.InitBoard(rules);
    }

    private void SnakeChanged(Snake snake)
    {
        dino.AppendScoreText(snake);
    }

    private void SnakeDone(Snake snake)
    {
        dino.ClearScoreText();

        data.score += snake.GetTotalScore();
        data.movesLeft--;

        OnGameDataUpdated?.Invoke(data);
        CheckTargetGoals();
    }

    private void CheckTargetGoals()
    {
        bool scoreGoalMet = data.scoreGoal.Equals(0) || data.score >= data.scoreGoal;
        bool locksGoalMet = data.locksGoal.Equals(0) || data.locks >= data.locksGoal;
        bool rocksGoalMet = data.rocksGoal.Equals(0) || data.rocks >= data.rocksGoal;

        if (scoreGoalMet && locksGoalMet && rocksGoalMet)
        {
            StartCoroutine(EndGame(true)); // win
        }
        else if (data.movesLeft < 0)
        {
            StartCoroutine(EndGame(false)); // lose
        }
    }

    private void LockDestroyed()
    {
        data.locks++;
        AudioManager.Instance.PlayAudio(Sound.LockSmash);
    }

    private void RockSmashed()
    {
        data.rocks++;
        AudioManager.Instance.PlayAudio(Sound.RockBreak);
    }

    private IEnumerator EndGame(bool win)
    {
        AudioManager.Instance.PlayAudio(win ? Sound.WinSound : Sound.LoseSound);

        if (win)
        {
            int progress = rules.Index;
            PlayerPrefs.SetInt(Constants.LEVEL_PROGRESS_KEY, progress);

            dino.DinoJump();
        }

        if (gameBoard)
            Destroy(gameBoard.gameObject);

        yield return new WaitForSeconds(1);

        LevelSelectManager.Instance.ShowLevelSelect();
    }
}