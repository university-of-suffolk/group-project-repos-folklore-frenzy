using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    // This boolean can be accessed by other scripts to disable certain functions if the game is paused (like player movement).
    public static bool isPaused;


    // This Update() function sets isPaused to false when a script changes the Time.timeScale to zero.
    // NOTE: To pause the game use Time.timeScale - isPaused only checks if Time.timeScale is zero.
    void Update()
    {
        if (Time.timeScale > 0)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
