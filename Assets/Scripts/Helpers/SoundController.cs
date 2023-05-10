using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère tous les sons du jeu
/// </summary>
public class SoundController : MonoBehaviour
{
    public Sounds soundsAvailable;

    private static SoundController _instance;

    public static SoundController Instance { get { return _instance; } }


    private void Awake()
    {
        // singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    /// <summary>
    /// Joue le son selectionné
    /// </summary>
    /// <param name="sound">Le son.</param>
    public void PlaySound(Func<Sounds, AudioClip> sound)
    {
        var selectedClip = sound(soundsAvailable);

        // Valide que le son exist et que l'usager à cocher qu'il voulait avoir des sons.
        if (selectedClip != null
            && PlayerPrefs.GetInt("Music") == 1)
        {
            AudioSource.PlayClipAtPoint(selectedClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Sound not found");
        }
    }
}

/// <summary>
/// Contient les sons disponibles
/// </summary>
[Serializable]
public class Sounds
{
    public AudioClip Fail;

    public AudioClip Success;

}