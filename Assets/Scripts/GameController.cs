using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script qui gère le déroulement du jeu
/// </summary>
public class GameController : MonoBehaviour
{
    #region Props
    [SerializeField]
    private GameObject puzzleContainer; // objet contenant les puzzles comme enfant

    private List<Puzzle> puzzles; // list des puzzle restant à résoudre
    private List<Puzzle> failedPuzzles; // regroupe les puzzles qui ont été ratés à la première tentative
    private List<Puzzle> succeededPuzzles; // regroupe les puzzles qui ont été réussis à la première tentative

    [SerializeField]
    private UIPuzzleManager uiPuzzleManager; // script qui gère l'UI des puzzles

    private int _currentScore;
    public int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        private set
        {
            _currentScore = value;
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
            _streak = value;

            if (_streak > LongestStreak)
            {
                LongestStreak = _streak;
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
        succeededPuzzles = new List<Puzzle>(puzzles);
        failedPuzzles = new List<Puzzle>();

        foreach (var puzzle in puzzles)
        {
            puzzle.AttachController(this);
            MaxScore += puzzle.AvailablePoint;
        }
    }

    /// <summary>
    /// Lorsque l'utilisateur vise un puzzle
    /// </summary>
    /// <param name="puzzle">Le puzzle visé</param>
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
    public void OnPuzzleSolved(Puzzle puzzle)
    {
        SoundController.Instance.PlaySound(x => x.Success);

        // dans le cas ou le puzzle à déjà été essayé on ne donne pas de point
        if (!failedPuzzles.Any(x => x == puzzle))
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
    /// <param name="puzzle">Le puzzle manqué</param>
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
    /// Lorsque l'utilisateur découvre un trésor
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
        ArArtSceneManager.ScoreBoard();
    }

    private void OnDestroy()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.DetachController(this);
        }
    }
}
