using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    [SerializeField] Toggle performanceModeToggle;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle musicToggle;

    private readonly string performanceKey = "performanceMode";
    private readonly string soundKey = "sound";
    private readonly string musicKey = "music";

    private void Start()
    {
        SetPrefValue(performanceModeToggle, performanceKey);
        SetPrefValue(soundToggle, soundKey);
        SetPrefValue(musicToggle, musicKey);
    }

    private void SetPrefValue(Toggle toggle, string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
            toggle.isOn = false;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt(key) == 0)
            {
                toggle.isOn = false;
            }
            else
            {
                toggle.isOn = true;
            }
        }
    }

    public void TogglePerformanceMode()
    {
        Toggle(performanceModeToggle, performanceKey);
    }

    public void ToggleMusic()
    {
        Toggle(musicToggle, musicKey);
    }

    public void ToggleSound()
    {
        Toggle(soundToggle, soundKey);
    }

    private void Toggle(Toggle toggle, string key)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
        PlayerPrefs.Save();
    }
}
