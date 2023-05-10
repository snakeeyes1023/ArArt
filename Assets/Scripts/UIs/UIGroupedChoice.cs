using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gére les choix groupés 
/// (permet de selectionner une seul réponse et obtenir le choix sélectionné)
/// </summary>
public class UIGroupedChoice : MonoBehaviour
{
    [SerializeField] private GameObject radioButtonPrefab; // prefab du bouton radio

    private Toggle[] radioButtons; // tableau des boutons radioButtons [0] =  answerChoices [0] etc...
    private AnswerChoice[] answerChoices; // tableau des choix de réponses


    /// <summary>
    /// Retourne le choix sélectionné
    /// </summary>
    /// <returns>AnswerChoice correspondant</returns>
    public AnswerChoice GetSelectedAnswer()
    {
        if (radioButtons != null)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].isOn)
                {
                    return answerChoices[i];
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Initialise les choix selon les choix de la question 
    /// </summary>
    /// <param name="question">La question.</param>
    public void SetQuestion(Question question)
    {
        ClearContext();

        answerChoices = question.GetChoices().ToArray();


        List<Toggle> pendingRadioButtons = new List<Toggle>();
        foreach (AnswerChoice answerChoice in answerChoices)
        {
            pendingRadioButtons.Add(AddRadioButton(answerChoice.Answer));
        }

        radioButtons = pendingRadioButtons.ToArray();
    }

    /// <summary>
    /// Créer un bouton radio pour un choix de réponse
    /// </summary>
    /// <param name="labelText">Le texte à afficher.</param>
    /// <returns>L'instance créé</returns>
    private Toggle AddRadioButton(string labelText)
    {
        GameObject newRadioButton = Instantiate(radioButtonPrefab, transform);
        newRadioButton.GetComponentInChildren<Text>().text = labelText;

        Toggle toggleComponent = newRadioButton.GetComponent<Toggle>();
        toggleComponent.onValueChanged.AddListener(delegate { OnRadioButtonValueChanged(toggleComponent); });

        return toggleComponent;
    }


    /// <summary>
    /// Appelé lorsqu'un bouton radio change de valeur (déselectionne tous les autres)
    /// </summary>
    /// <param name="changedRadioButton">La valeur changé</param>
    private void OnRadioButtonValueChanged(Toggle changedRadioButton)
    {
        if (changedRadioButton.isOn)
        {
            foreach (Toggle radioButton in radioButtons)
            {
                if (radioButton != changedRadioButton)
                {
                    radioButton.isOn = false;
                }
            }
        }
    }

    /// <summary>
    /// Efface tous les boutons radio actuel
    /// </summary>
    private void ClearContext()
    {
        if (radioButtons != null)
        {
            foreach (Toggle radioButton in radioButtons)
            {
                Destroy(radioButton.gameObject);
            }

            radioButtons = null;
        }
    }
}
