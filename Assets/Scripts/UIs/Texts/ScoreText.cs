using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// G�re l'affichage du score actuelle (est mise � jour automatiquement)
/// </summary>
public class ScoreText : MonoBehaviour
{
    public GameController GameController;
    
    public TextMeshProUGUI TextMeshPro;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.OnScoreChanged += GameController_OnScoreChanged;

        GameController_OnScoreChanged(this, GameController.CurrentScore);
    }

    /// <summary>
    /// Met � jour le score afficher
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="newScore">The new score.</param>
    private void GameController_OnScoreChanged(object sender, int newScore)
    {
        TextMeshPro.text = newScore.ToString() + " / " + GameController.MaxScore.ToString();
    }
}
