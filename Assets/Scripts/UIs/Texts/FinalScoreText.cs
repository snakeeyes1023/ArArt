using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Permet d'afficher le score final depuis les players prefs
/// </summary>
public class FinalScoreText : MonoBehaviour
{
    public TextMeshProUGUI TextMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();

        TextMeshPro.text = PlayerPrefs.GetString("FinalScoreMessage", "Bravo!");
    }
}
