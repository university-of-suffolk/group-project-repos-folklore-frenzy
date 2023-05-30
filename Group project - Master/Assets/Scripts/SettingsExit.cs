using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsExit : MonoBehaviour
{
    [Header("Menu GameObjects")]
    public GameObject warningBox;

    // When we exit the settings menu, the settings button in the main menu should be the selected.
    [Header("Menu Selection Buttons On Close")]
    public GameObject optionsMenuButtonClose;
    public GameObject warningBoxMenuButtonClose;

    // These are the buttons in each of the menus which should be highlighted first.
    [Header("Menu First Selection Buttons")]
    public GameObject warningBoxFirstButton;

    public void Exit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuButtonClose);
    }

    public void WarningBox()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(warningBoxFirstButton);
    }

    public void WarningBoxExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(warningBoxMenuButtonClose);
    }
}
