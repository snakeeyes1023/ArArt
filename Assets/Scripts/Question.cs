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

    [SerializeField]
    private AnswerChoice[] Choices;

    public IEnumerable<AnswerChoice> GetChoices()
    {
        var difficulty = (AnswerDifficulty)PlayerPrefs.GetInt("Difficulty", 0);

        // get number of choices based on difficulty
        yield return Choices.FirstOrDefault(x => x.IsValid);

        int left = (int)difficulty + 1;

        foreach (var choice in Choices)
        {
            if (!choice.IsValid && left > 0)
            {
                yield return choice;
                left--;
            }
        }
    }

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

public enum AnswerDifficulty
{
    EASY = 0,
    MEDIUM = 1,
    HARD = 2
}
