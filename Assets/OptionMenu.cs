using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private bool soundMuted = false;
    public GameObject dot;
    private bool dotShown = false;

    private bool showOptionMenu = false;

    public GameObject optionMenu;
    
    public void MuteSound()
    {
        soundMuted = !soundMuted;
        AudioListener.volume = Math.Abs(1 - AudioListener.volume);
        dot.SetActive(soundMuted);
        Debug.Log(soundMuted);
    }

    
}
