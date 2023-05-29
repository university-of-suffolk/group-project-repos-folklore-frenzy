using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject timerCountdown;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // When the return button is pressed, unpause!
    public void ReturnGame()
    {
        Active();
    }

    // When the main menu button is pressed, return to the main menu.
    public void MainMenu()
    {
        Time.timeScale = 1f; // Unpauses on the main menu
        SceneManager.LoadScene(0); // The main menu should always be the first scene!
    }

    // Shows/Hides the main menu.
    public void Active()
    {
        if (Time.timeScale == 1f && timerCountdown.GetComponent<TimerCountdown>().roundOver == false)
        {
            gameObject.SetActive(true); // Shows the menu UI.
            Time.timeScale = 0f; // Pauses the game.
        }
        else if (Time.timeScale == 0f && timerCountdown.GetComponent<TimerCountdown>().roundOver == false)
        {
            Time.timeScale = 1f; // Unpauses the game.
            gameObject.SetActive(false); // Hides the menu UI.
        }
    }
}
