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

    // This boolean makes sure that when the round is over, the function RoundOver() is called once.
    private bool roundOver = false;

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
        }
    }
}
