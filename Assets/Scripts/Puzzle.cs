using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private Question[] availableQuestions;

    public bool IsDone;
    public int Point = 1;

    public virtual int AvailablePoint { get { return Point; } }

    private Question selectedQuestion;

    public event Action<Puzzle> OnSolve;
    public event Action<Puzzle> OnFailed;
    public event Action<Puzzle> OnSelected;

    /// <summary>
    /// Selects this instance.
    /// </summary>
    public void Select()
    {
        OnSelected?.Invoke(this);
    }

    /// <summary>
    /// Deselects this instance.
    /// </summary>
    public void Deselect()
    {
        OnSelected?.Invoke(null);
    }

    /// <summary>
    /// Gets the selected question.
    /// </summary>
    /// <returns></returns>
    public Question GetSelectedQuestion()
    {
        if (selectedQuestion == null)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableQuestions.Count());
            selectedQuestion = availableQuestions[randomIndex];
            selectedQuestion.QuestionNumber = randomIndex;
        }
        return selectedQuestion;
    }

    /// <summary>
    /// Attempts the specified choice.
    /// </summary>
    /// <param name="choice">The choice.</param>
    /// <returns></returns>
    public bool Attempt(AnswerChoice choice)
    {
        if (selectedQuestion.IsCorrect(choice))
        {
            OnSolve?.Invoke(this);
            return true;
        }
        else
        {
            OnFailed?.Invoke(this);
            return false;
        }

    }

    /// <summary>
    /// Attaches the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public virtual void AttachController(GameController controller)
    {
        OnSolve += controller.OnPuzzleSolve;
        OnFailed += controller.OnPuzzleFailed;
        OnSelected += controller.OnPuzzleSelected;
    }

    /// <summary>
    /// Detaches the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public virtual void DetachController(GameController controller)
    {
        OnSolve -= controller.OnPuzzleSolve;
        OnFailed -= controller.OnPuzzleFailed;
        OnSelected -= controller.OnPuzzleSelected;
    }
}
