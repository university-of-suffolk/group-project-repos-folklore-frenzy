using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static float currentScore; // This is the score we actually change - holds the updated score.
    private float displayScore; // This acts as the temporary score variable, which holds the previous score.
    public float scorePerSecond; // This is how quick the display score changes to the current score.
    void Start()
    {
        currentScore = 0; // This is the score set inside the code.
        displayScore = 0; // This is the score we see on the screen.
    
        StartCoroutine(ScoreUpdater()); // This begins a loop which checks when we change the player's score.
    }

    void Update()
    {
        // DEV TOOLS: Press spacebar to increment the score.
        if (Input.GetKeyDown("space"))
        {
            currentScore += 100;
        }

        // DEV TOOLS: Press backspace to decrease the score.
        if (Input.GetKeyDown("backspace"))
        {
            currentScore -= 100;
        }

        // This if-statement makes sure that the player's score never falls below zero!
        if (currentScore < 0)
        {
            currentScore = 0;
        }
    }

    // This loop takes the previous score ("displayScore") and compares it with the current score ("currentScore").
    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            // Has the current score been increased above its previous score?
            if (displayScore < currentScore)
            {
                displayScore++; // Increment the display score by 1.
                scoreText.text = "¥" + displayScore.ToString();
            }

            // Has the current score been decreased below its previous score?
            if (displayScore > currentScore)
            {
                displayScore--; // Subtract the display score by 1.
                scoreText.text = "¥" + displayScore.ToString();
            }

            yield return new WaitForSeconds(scorePerSecond); // Delay per change.
        }
    }
}
