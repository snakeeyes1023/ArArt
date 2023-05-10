using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Gère le boutton de fin de partie
/// </summary>
public class EndGameButton : MonoBehaviour
{
    [SerializeField]
    private GameController GameController;

    /// <summary> 
    /// Déclenche la méthode pour arrêter le jeu (scoreboard)
    /// </summary>
    public void EndGame()
    {
        GameController.OnGameEnded();
    }
}
