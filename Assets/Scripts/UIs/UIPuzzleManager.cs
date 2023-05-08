using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPuzzleManager : MonoBehaviour
{
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
        PopupPanel.gameObject.SetActive(currentPuzzle != null);
    }

    private void OnConfirmButtonClicked()
    {
        if (Choices.GetSelectedAnswer() is AnswerChoice choice 
            && currentPuzzle.Attempt(choice))
        {
            ClearContext();
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


        QuestionNumber.text = question.QuestionText;
        QuestionText.text = puzzle.GetPuzzleMessage();

        Choices.SetQuestion(question);
    }

    public void ClearContext()
    {
        currentPuzzle = null;
    }
}