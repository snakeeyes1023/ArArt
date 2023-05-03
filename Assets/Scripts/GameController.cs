using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Props
    [SerializeField]
    private GameObject puzzleContainer;
    
    private List<Puzzle> puzzles;

    [SerializeField]
    private UIPuzzleManager uiPuzzleManager;

    private int _currentScore;
    public int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        private set
        {
            if (value < 0)
            {
                _currentScore = 0;
            }
            else
            {
                _currentScore = value;
            }
            OnScoreChanged?.Invoke(this, value);
        }
    }

    private int _streak;
    public int Streak
    {
        get
        {
            return _streak;
        }
        private set
        {
            if (value < 0)
            {
                _streak = 0;
            }
            else
            {
                _streak = value;

                if (_streak > LongestStreak)
                {
                    LongestStreak = _streak;
                }
            }
            OnStreakChanged?.Invoke(this, value);
        }
    }

    public int LongestStreak { get; private set; }

    public int MaxScore { get; private set; }



    #endregion

    public event EventHandler<int> OnScoreChanged;
    public event EventHandler<int> OnStreakChanged;

    void Start()
    {
        puzzles = puzzleContainer.GetComponentsInChildren<Puzzle>().ToList();
        
        foreach (var puzzle in puzzles)
        {
            puzzle.AttachController(this);
            MaxScore += puzzle.AvailablePoint;
        }
    }

    public void OnPuzzleSolve(Puzzle puzzle)
    {
        CurrentScore += puzzle.Point;
        Streak += 1;

        SoundController.Instance.PlaySound(x => x.Success);

        puzzle.DetachController(this);
        
        puzzles.Remove(puzzle);

        if (puzzles.Count == 0)
        {
            OnGameFinished();
        }
    }

    public void OnGameFinished()
    {
        PlayerPrefs.SetString("FinalScore", $"{_currentScore} / {MaxScore}");

        SceneManager.LoadScene(3);
    }

    public void OnPuzzleFailed(Puzzle puzzle)
    {
        Streak = 0;

        SoundController.Instance.PlaySound(x => x.Fail);
    }

    public void OnTreasureFounded(PuzzleAnim puzzle)
    {
        SoundController.Instance.PlaySound(x => x.Success);

        CurrentScore += puzzle.BonusPoint;
    }

    public void OnPuzzleSelected(Puzzle puzzle)
    {
        if (puzzle == null)
        {
            uiPuzzleManager.ClearContext();
        }
        else
        {
            uiPuzzleManager.ChangeCurrentPuzzle(puzzle);
        }
    }

    private void OnDestroy()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.DetachController(this);
        }
    }
}
