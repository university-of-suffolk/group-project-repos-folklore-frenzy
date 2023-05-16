using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score_text;
    [SerializeField] TextMeshProUGUI highscore_text;

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + ScoreManager.currentScore.ToString(); 

        if (!PlayerPrefs.HasKey("Highscore"))
        {
            highscore_text.text = "Highscore: " + ScoreManager.currentScore.ToString();
        }
        else if (ScoreManager.currentScore >= PlayerPrefs.GetFloat("Highscore"))
        {
            highscore_text.text = "Highscore: " + ScoreManager.currentScore.ToString();
        }
        else
        {
            highscore_text.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore").ToString();
        }
    }
}
