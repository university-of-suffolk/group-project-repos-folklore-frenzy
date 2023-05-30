using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    // These are the buttons in each of the menus which should be highlighted first.
    [Header("Menu First Selection Buttons")]
    public GameObject mainMenuFirstButton;
    public GameObject optionsMenuFirstButton; 
    public GameObject instructionsMenuFirstButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);      
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Instructions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionsMenuFirstButton);
    }

    public void Options()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }
}
