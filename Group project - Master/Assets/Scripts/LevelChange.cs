using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public void LoadMain(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
