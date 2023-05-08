using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Question
{
    public string QuestionText;

    [SerializeField]
    private AnswerChoice[] Choices;

    /// <summary>
    /// Récupère les choix pour la question selon le niveau de difficulté
    /// </summary>
    /// <returns>Tableau mélangé de choix</returns>
    public IEnumerable<AnswerChoice> GetChoices()
    {
        int totalChoice = PlayerPrefs.GetInt("Difficulty", 0) + 1;

        List<AnswerChoice> choices = new List<AnswerChoice>(Choices.Length);

        // ajoute le choix valide
        choices.Add(GetValidChoice());

        // ajouter les mauvais choix
        choices.AddRange(Choices
            .Where(x => !x.IsValid)
            .OrderBy(c => Guid.NewGuid())
            .Take(totalChoice));


        return choices.OrderBy(c => Guid.NewGuid());
    }

    private AnswerChoice GetValidChoice()
    {
        return Choices.First(x => x.IsValid);
    }

    public bool IsCorrect(AnswerChoice choice)
    {
        return Choices.Any(x => x == choice && x.IsValid);
    }
}