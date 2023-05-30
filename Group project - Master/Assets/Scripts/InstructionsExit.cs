using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionsExit : MonoBehaviour
{
    // When we exit the instructions menu, the instructions button in the main menu should be the selected.
    [Header("Menu Selection Buttons On Close")]
    public GameObject instructionsMenuButtonClose;

    public void Exit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionsMenuButtonClose);
    }
}
