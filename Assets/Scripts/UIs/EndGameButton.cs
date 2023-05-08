using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameButton : MonoBehaviour
{
    [SerializeField]
    private GameController GameController;

    public void EndGame()
    {
        GameController.OnGameEnded();
    }
}
