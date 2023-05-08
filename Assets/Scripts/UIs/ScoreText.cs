using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void GameController_OnScoreChanged(object sender, int newScore)
    {
        TextMeshPro.text = newScore.ToString() + " / " + GameController.MaxScore.ToString();
    }
}
