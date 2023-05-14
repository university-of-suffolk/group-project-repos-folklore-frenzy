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

    bool lostScore = false; // This boolean detects when the change in score is a decrease.
    bool gainScore = false; // This boolean detects when the change in score is an increase.

    public AudioSource tickAudioSource; // I used a seperate audio source since the tick SFX was quite loud!
    public AudioSource audioSource; // This is a regular audio source for sounds at regular volume.
    public AudioClip tickSFX; // This SFX plays quietly when the score changes.
    public AudioClip ScoreTotalSFX; // This SFX plays when the current score meets the display score.
    void Start()
    {
        currentScore = 0; // This is the score set inside the code.
        displayScore = 0; // This is the score we see on the screen.
    
        StartCoroutine(ScoreUpdater()); // This begins a loop which checks when we change the player's score.
    }

    void Update()
    {
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
                //displayScore++; // Increment the display score by 1.
                displayScore = currentScore; // Set score.
                scoreText.text = "¥" + displayScore.ToString();
                tickAudioSource.PlayOneShot(tickSFX); // Tick SFX plays until display score is equal to current score.
                gainScore = true; // Score has increased!
            }

            // Has the current score been decreased below its previous score?
            if (displayScore > currentScore)
            {
                //displayScore--; // Subtract the display score by 1.
                displayScore = currentScore; // Set score.
                scoreText.text = "¥" + displayScore.ToString();
                tickAudioSource.PlayOneShot(tickSFX); // Tick SFX plays until display score is equal to current score.
                lostScore = true; // Score has decreased!
            }

            if (displayScore == currentScore && gainScore)
            {
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(ScoreTotalSFX);
                gainScore = false;
            }
            else if (displayScore == currentScore && lostScore)
            {
                audioSource.pitch = 0.7f; // This makes the sound lower in pitch when you lose money!
                audioSource.PlayOneShot(ScoreTotalSFX);
                lostScore = false;
            }

            yield return new WaitForSeconds(scorePerSecond); // Delay per change.
        }
    }
}
