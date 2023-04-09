using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> puzzles;

    public int CurrentScore { get; private set; }


    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.AttachController(this);
        }
    }

    public void Puzzle_OnAttempt(object sender, AnswerChoice e)
    {
        if (sender is Puzzle puzzle)
        {
            if (puzzle.CheckAnswer(e))
            {
                CurrentScore += puzzle.Point;
                puzzle.IsDone = true;
            }
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
