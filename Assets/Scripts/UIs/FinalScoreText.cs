using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
