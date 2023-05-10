using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère les scènes du jeu et configuration
/// </summary>
public class ArArtSceneManager : MonoBehaviour
{
    public void Awake()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        PlayerPrefs.SetInt("Music", 1);

    }

    /// <summary>
    /// Charge la scène de jeu
    /// </summary>
    public static void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Affiche le menu principal
    /// </summary>
    public static void Show()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Affiche le manuel d'instruction
    /// </summary>
    public static void ReadManual()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Affiche le menu de fin de partie
    /// </summary>
    public static void ScoreBoard()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Quitte l'application
    /// </summary>
    public static void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Met à jour la difficulté du jeu
    /// </summary>
    /// <param name="difficulty">The difficulty.</param>
    public static void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    /// <summary>
    /// Met à jour la musique du jeu
    /// </summary>
    /// <param name="enable">if set to <c>true</c> [enable].</param>
    public static void EnableMusic(bool enable)
    {
        PlayerPrefs.SetInt("Music", enable ? 1 : 0);
    }
}
