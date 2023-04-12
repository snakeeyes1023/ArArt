using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGroupedChoice : MonoBehaviour
{
    [SerializeField] private GameObject radioButtonPrefab;

    private List<Toggle> radioButtons = new List<Toggle>();
    private IEnumerable<AnswerChoice> answerChoices;


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

    public AnswerChoice GetSelectedAnswer()
    {
        foreach (Toggle radioButton in radioButtons)
        {
            if (radioButton.isOn)
            {
                int index = radioButtons.IndexOf(radioButton);
                return answerChoices.ElementAt(index);
            }
        }

        return null;
    }

    public void SetQuestion(Question question)
    {
        ClearContext();

        answerChoices = question.GetChoices();

        foreach (AnswerChoice answerChoice in answerChoices)
        {
            AddRadioButton(answerChoice.Answer);
        }
    }

    private void ClearContext()
    {
        foreach (Toggle radioButton in radioButtons)
        {
            Destroy(radioButton.gameObject);
        }

        radioButtons.Clear();
    }

    private void AddRadioButton(string labelText)
    {
        GameObject newRadioButton = Instantiate(radioButtonPrefab, transform);
        newRadioButton.GetComponentInChildren<Text>().text = labelText;

        Toggle toggleComponent = newRadioButton.GetComponent<Toggle>();
        toggleComponent.onValueChanged.AddListener(delegate { OnRadioButtonValueChanged(toggleComponent); });
        radioButtons.Add(toggleComponent);
    }
}
