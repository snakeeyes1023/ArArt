using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPuzzleManager : MonoBehaviour
{
    public Puzzle Test;
    

    public TextMeshProUGUI QuestionNumber;
    public TextMeshProUGUI QuestionText;
    public Button ConfirmButton;
    public UIGroupedChoice Choices;

    private Puzzle currentPuzzle;

    void Start()
    {
        ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    private void Update()
    {
        /* if press the space bar */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCurrentPuzzle(Test);
        }
    }

    private void OnConfirmButtonClicked()
    {
        if (Choices.GetSelectedAnswer() is AnswerChoice choice)
        {
            if (currentPuzzle.Attempt(choice))
            {
                Close();
            }
            else
            {
                Debug.Log("Wrong!");
            }
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }

    private void Close()
    {


    }

    private void Show()
    {

    }

    public void ChangeCurrentPuzzle(Puzzle puzzle)
    {
        currentPuzzle = puzzle;
        Question question = currentPuzzle.GetSelectedQuestion();


        QuestionNumber.text = "Question " + currentPuzzle.Number;
        QuestionText.text = question.QuestionText;

        Choices.SetQuestion(question);

        Show();
    }
}