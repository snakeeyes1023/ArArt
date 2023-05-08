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
    private List<Puzzle> failedPuzzles;
    private List<Puzzle> succeededPuzzles;

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

    void Awake()
    {
        // on va chercher tous les puzzles et initialise les événements
        puzzles = puzzleContainer.GetComponentsInChildren<Puzzle>().ToList();
        succeededPuzzles = new List<Puzzle>();
        failedPuzzles = new List<Puzzle>();
        
        foreach (var puzzle in puzzles)
        {
            puzzle.AttachController(this);
            MaxScore += puzzle.AvailablePoint;
        }
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

    /// <summary>
    /// Lorsqu'une puzzle à été réussi
    /// </summary>
    /// <param name="puzzle">Les puzzles réussis</param>
    public void OnPuzzleSolve(Puzzle puzzle)
    {
        SoundController.Instance.PlaySound(x => x.Success);

        // dans le cas ou le puzzle à déjà été essayé
        if (!failedPuzzles.Any(x => x == puzzle))
        {
            CurrentScore += puzzle.Point;
            Streak += 1;
            succeededPuzzles.Add(puzzle);
        }

        puzzle.DetachController(this);
        puzzles.Remove(puzzle);


        if (puzzles.Count == 0)
        {
            OnGameEnded();
        }
    }
    
    /// <summary>
    /// Lorsque le joueur à manquer un puzzle
    /// </summary>
    /// <param name="puzzle"></param>
    public void OnPuzzleFailed(Puzzle puzzle)
    {
        Streak = 0;

        SoundController.Instance.PlaySound(x => x.Fail);

        if (!failedPuzzles.Any(x => x == puzzle))
        {
            failedPuzzles.Add(puzzle);
        }
    }

    /// <summary>
    /// Lorsque le user trouve un treasore
    /// </summary>
    /// <param name="puzzle"></param>
    public void OnTreasureFounded(PuzzleAnim puzzle)
    {
        SoundController.Instance.PlaySound(x => x.Success);

        CurrentScore += puzzle.BonusPoint;
    }

    /// <summary>
    /// Lorsque la parti est terminé
    /// </summary>
    public void OnGameEnded()
    {
        PlayerPrefs.SetString("FinalScoreMessage", $"{_currentScore} / {MaxScore}");

        SceneManager.LoadScene(3);
    }

    private void OnDestroy()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.DetachController(this);
        }
    }
}
