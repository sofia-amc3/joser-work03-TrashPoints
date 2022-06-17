using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public bool audioEnabled = true;
    public AudioSource source;

    private void Awake()
    {
        source.volume = Constants.MAX_VOLUME;
    }

    public void Toggle()
    {
        if (audioEnabled)
        {
            source.volume = 0;
            audioEnabled = false;
        }
        else
        {
            source.volume = Constants.MAX_VOLUME;
            audioEnabled = true;
        }
    }
}
