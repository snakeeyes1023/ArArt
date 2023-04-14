using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public Sounds soundsAvailable;

    private static SoundController _instance;

    public static SoundController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlaySound(Func<Sounds, AudioClip> sound)
    {
        var selectedClip = sound(soundsAvailable);

        if (selectedClip != null)
        {
            AudioSource.PlayClipAtPoint(selectedClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Sound not found");
        }
    }
}


[Serializable]
public class Sounds
{
    public AudioClip Fail;

    public AudioClip Success;

}