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

    public int Point;
    public bool IsDone;
    public int MaxPoint;
    public int Number;
    

    private Question selectedQuestion;

    public event Action<Puzzle> OnSolve;
    public event Action<Puzzle> OnFailed;

    public event Action OnSelected;

    /// <summary>
    /// Selects the random question.
    /// </summary>
    private void SelectRandomQuestion()
    {
        selectedQuestion = availableQuestions[UnityEngine.Random.Range(0, availableQuestions.Count())];
    }

    public void Select()
    {
        SelectRandomQuestion();
        OnSelected?.Invoke();
    }

    public Question GetSelectedQuestion()
    {
        if (selectedQuestion == null)
        {
            Select();
        }
        return selectedQuestion;
    }

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
        OnSolve += controller.OnSolve;
        OnFailed += controller.OnFailed;
    }

    /// <summary>
    /// Detaches the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public virtual void DetachController(GameController controller)
    {
        OnSolve -= controller.OnSolve;
        OnFailed -= controller.OnFailed;
    }
}
