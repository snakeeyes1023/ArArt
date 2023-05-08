using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGroupedChoice : MonoBehaviour
{
    [SerializeField] private GameObject radioButtonPrefab;

    private Toggle[] radioButtons;
    private AnswerChoice[] answerChoices;


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

    private Toggle AddRadioButton(string labelText)
    {
        GameObject newRadioButton = Instantiate(radioButtonPrefab, transform);
        newRadioButton.GetComponentInChildren<Text>().text = labelText;

        Toggle toggleComponent = newRadioButton.GetComponent<Toggle>();
        toggleComponent.onValueChanged.AddListener(delegate { OnRadioButtonValueChanged(toggleComponent); });

        return toggleComponent;
    }
}
