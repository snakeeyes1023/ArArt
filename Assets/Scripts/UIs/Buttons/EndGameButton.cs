using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// G�re le boutton de fin de partie
/// </summary>
public class EndGameButton : MonoBehaviour
{
    [SerializeField]
    private GameController GameController;

    /// <summary> 
    /// D�clenche la m�thode pour arr�ter le jeu (scoreboard)
    /// </summary>
    public void EndGame()
    {
        GameController.OnGameEnded();
    }
}
