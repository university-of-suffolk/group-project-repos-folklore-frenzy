using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        // Assigns the variable as the screen
        resolutions = Screen.resolutions;

        // Clears the dropdown resolution options 
        resolutionDropdown.ClearOptions();

        // Creates the resoltuion options list
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Detects the screen resoution and sets it to default.
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Allows the player to set the resolution.
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Allows the player to adjust the master volume.
    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    // Allows the player to adjust the graphics quality.
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel (qualityIndex);
    }

    // Allows the player to toggle fullscreen
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
