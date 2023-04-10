using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Question
{
    public int QuestionNumber;
    public string QuestionText;

    public AnswerChoice[] Choices;

    public bool IsCorrect(AnswerChoice choice)
    {
        if (Choices.Any(x => x == choice && x.IsValid))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
