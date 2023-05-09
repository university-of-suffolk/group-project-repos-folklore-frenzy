using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
    }

    // When the return button is pressed, unpause!
    public void ReturnGame()
    {
        Active();
    }

    // When the main menu button is pressed, return to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene(0); // The main menu should always be the first scene!
    }

    // Shows/Hides the main menu.
    public void Active()
    {
        if (!PauseManager.isPaused)
        {
            gameObject.SetActive(true); // Shows the menu UI.
            Time.timeScale = 0f; // Pauses the game.
        }
        else
        {
            Time.timeScale = 1f; // Unpauses the game.
            gameObject.SetActive(false); // Hides the menu UI.
        }
    }
}
