using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    private void Start()
    {
        Active(false);
    }


    // When the restart button is pressed, reload the scene!
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // When the main menu button is pressed, return to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene(0); // The main menu should always be the first scene!
        Time.timeScale = 1f;
    }

    // When the shop button is pressed, go to the shop! (Currently unavailable)
    public void Shop()
    {
        //SceneManager.LoadScene("ShopScene")
        Debug.Log("Shop.");
    }

    // Shows/Hides the main menu.
    public void Active(bool state)
    {
        if (state)
        {
            Time.timeScale = 0f; // Pauses the game.
            gameObject.SetActive(true); // Shows the menu UI.
        }
        else
        {
            gameObject.SetActive(false); // Hides the menu UI.
        }
    }
}
