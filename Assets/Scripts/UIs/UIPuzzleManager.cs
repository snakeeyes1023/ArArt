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
    public MonoBehaviour PopupPanel;

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

        PopupPanel.gameObject.SetActive(currentPuzzle != null);
    }

    private void OnConfirmButtonClicked()
    {
        if (Choices.GetSelectedAnswer() is AnswerChoice choice)
        {
            if (currentPuzzle.Attempt(choice))
            {
                currentPuzzle = null;
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

    public void ChangeCurrentPuzzle(Puzzle puzzle)
    {
        currentPuzzle = puzzle;
        Question question = currentPuzzle.GetSelectedQuestion();


        QuestionNumber.text = "Question " + currentPuzzle.Number;
        QuestionText.text = question.QuestionText;

        Choices.SetQuestion(question);
    }
}