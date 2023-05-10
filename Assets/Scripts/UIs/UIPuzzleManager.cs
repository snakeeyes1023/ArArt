using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Script qui g�re l'UI des puzzles (la popup)
/// </summary>
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
        PopupPanel.gameObject.SetActive(currentPuzzle != null); // optimisation si le projet va plus loin
    }

    /// <summary>
    /// Lorsque le bouton de confirmation est cliqu� on v�rifie la r�ponse 
    /// Si la r�ponse est valide il s'occupe de fermer le puzzle
    /// </summary>
    private void OnConfirmButtonClicked()
    {
        if (Choices.GetSelectedAnswer() is AnswerChoice choice 
            && currentPuzzle.Attempt(choice))
        {
            ClearContext();
        }
    }

    /// <summary>
    /// Affiche le puzzle dans la popup
    /// </summary>
    /// <param name="puzzle">The puzzle.</param>
    public void ChangeCurrentPuzzle(Puzzle puzzle)
    {
        currentPuzzle = puzzle;

        if (currentPuzzle.GetSelectedQuestion() is Question question)
        {
            QuestionNumber.text = question.QuestionText;
            QuestionText.text = puzzle.GetPuzzleMessage();

            Choices.SetQuestion(question);
        }
        else
        {
            Debug.LogError("Aucune question n'a �t� s�lectionn�e", puzzle);
        }
    }

    /// <summary>
    /// Ferme le puzzle de la popup
    /// </summary>
    public void ClearContext()
    {
        currentPuzzle = null;
    }
}