using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    private SoundController soundController;

    private void Start()
    {
        soundController = FindObjectOfType<SoundController>(true);
    }

    public void Toggle()
    {
        soundController.Toggle();
    }

    private void Update()
    {
        if (soundController.audioEnabled)
        {
            if (gameObject.GetComponent<Image>().sprite != Resources.Load<Sprite>("Music/soundIcon"))
            {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Music/soundIcon");
            }
        }
        else
        {
            if (gameObject.GetComponent<Image>().sprite != Resources.Load<Sprite>("Music/noSoundIcon"))
            {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Music/noSoundIcon");
            }
        }
    }
}
