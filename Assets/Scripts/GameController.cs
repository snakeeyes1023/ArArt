using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Props
    
    [SerializeField]
    private List<Puzzle> puzzles;

    private Puzzle currentPuzzle;

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

    public int MaxScore { get; private set; }

    #endregion

    public event EventHandler<int> OnScoreChanged;


    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.AttachController(this);
            MaxScore += puzzle.MaxPoint;
        }
    }

    public void OnSolve(Puzzle puzzle)
    {
        CurrentScore += puzzle.Point;
    }

    public void OnFailed(Puzzle puzzle)
    {
        
    }

    public void SelectPuzzle(Puzzle selectedPuzzle)
    {
        currentPuzzle = selectedPuzzle;
    }


    private void OnDestroy()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.DetachController(this);
        }
    }
}
