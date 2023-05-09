using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    // Variables
    [Header("Text UI")]
    public TextMeshProUGUI TimerText;
    [Header("Time Values")]
    public int StartingTime = 180;
    private float CurrentTime;

    // This boolean is used to determine when the round is over.
    private bool roundOver = false;

    // This is used to display the game over screen.
    public GameObject gameOverUI;

    private void Start()
    {
        // This resets the time and starts the game at the starting time.
        CurrentTime = StartingTime;
        UpdateTimer();  
    }

    private void Update()
    {
        // If the timer reaches 0, freeze the timer!
        if(CurrentTime <= 0)
        {
            CurrentTime = 0;
            RoundOver();
        }
        else // Run this code if the timer has not reached 0!
        {
            // Time.deltaTime decrements the currentTime every second, instead of every frame, and updates the timer UI text.
            CurrentTime -= 1 * Time.deltaTime;
            UpdateTimer();
        }
    }

    // This function updates the timer UI text.
    void UpdateTimer()
    {
        TimerText.text = CurrentTime.ToString("0"); // The "0" makes the float value a whole number!
    }


    // This function is called ONCE when the timer has run out.
    void RoundOver()
    {
        if (!roundOver)
        {
            roundOver = true;
            Debug.Log("The timer has run out and the round is over!");

            // Checking if there is a saved highscore, if there is then compare with the current score. If it is beaten, save the new highscore over the old.
            if (PlayerPrefs.HasKey("Highscore"))
            {
                float currentHighscore = PlayerPrefs.GetFloat("Highscore");
                if (ScoreManager.currentScore > currentHighscore)
                {
                    PlayerPrefs.SetFloat("Highscore", ScoreManager.currentScore);
                }
            }
            else // saves the highscore if there is no save data.
            {
                PlayerPrefs.SetFloat("Highscore", ScoreManager.currentScore);
            }

            gameOverUI.GetComponent<GameOverMenu>().Active(true); // Displays the game over screen and pauses the game.
        }
    }
}
