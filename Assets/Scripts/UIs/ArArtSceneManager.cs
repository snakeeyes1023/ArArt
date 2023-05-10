using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArArtSceneManager : MonoBehaviour
{
    public void Awake()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        PlayerPrefs.SetInt("Music", 1);

    }
    public static void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public static void Show()
    {
        SceneManager.LoadScene(0);
    }

    public static void ReadManual()
    {
        SceneManager.LoadScene(1);
    }

    public static void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public static void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    public static void EnableMusic(bool enable)
    {
        PlayerPrefs.SetInt("Music", enable ? 1 : 0);
    }
}
