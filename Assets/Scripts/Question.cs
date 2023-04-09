using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Question
{
    public int QuestionNumber;
    public string QuestionText;

    public AnswerChoice[] Choices;
}
