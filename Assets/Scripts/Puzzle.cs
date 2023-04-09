using System;
using System.Collections.Generic;
using System.Linq;
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


    private Question selectedQuestion;

    public event EventHandler<AnswerChoice> OnAttempt;

    /// <summary>
    /// Selects the random question.
    /// </summary>
    private void SelectRandomQuestion()
    {
        selectedQuestion = availableQuestions[UnityEngine.Random.Range(0, availableQuestions.Count())];
    }

    /// <summary>
    /// Gets the selected question.
    /// </summary>
    /// <returns></returns>
    public Question GetSelectedQuestion()
    {
        if (selectedQuestion == null)
        {
            SelectRandomQuestion();
        }
        return selectedQuestion;
    }

    public bool CheckAnswer(AnswerChoice answer)
    {
        if (selectedQuestion != null)
        {
            return answer.IsValid;
        }

        return false;
    }

    /// <summary>
    /// Attaches the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public virtual void AttachController(GameController controller)
    {
        OnAttempt += controller.Puzzle_OnAttempt;
    }

    /// <summary>
    /// Detaches the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public virtual void DetachController(GameController controller)
    {
        OnAttempt -= controller.Puzzle_OnAttempt;
    }
}
