using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{

    // Variables

    public int countdownDuration; // This is the starting number to count down from.
    public TextMeshProUGUI countdownText; // This is the text which shows the countdown on screen.
    float timeRemaining; // This float is responsible for doing the math, which we'll set to the text.
    public AudioSource audioSource; // This can play sounds for a clock!
    public AudioClip tickSFX; // This sound is used when the clock ticks.
    
    public AudioSource musicAudioSource;
    [Tooltip("This is for testing! Music will probably not be played from this object in the future.")]
    public AudioClip bgMusic;

    public static bool gameStarted = false;

    public Camera openingCutscene;
    public GameObject cinematicBars;
    Animator cinematicBarsAnimator;
    private void Awake()
    {
        openingCutscene.enabled = true;
        Active(true); // This function is used to show/hide the countdown UI.
        countdownDuration += 1; // Unity starts one second too early, hence why an extra second was added.
        Time.timeScale = 0f; // This starts the game PAUSED!
        timeRemaining = countdownDuration; // This sets the time remaining, to what we set the time to start at in the Inspector.
        cinematicBarsAnimator = cinematicBars.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Countdown()); // This runs the main countdown function.
        openingCutscene.enabled = true;
    }

    private void Update()
    {
        countdownText.text = timeRemaining.ToString("0"); // When the timeRemaining is changed, it will update the text.

        // This pauses the music during the pause screen!
        if(gameStarted && Time.timeScale == 0f && musicAudioSource.isPlaying)
        {
            musicAudioSource.Pause();
        }
        else if(gameStarted && Time.timeScale == 1f && !musicAudioSource.isPlaying)
        {
            musicAudioSource.UnPause();
        }
    }

    IEnumerator Countdown()
    {
        // From zero - countdownDuration, tick the countdown by -1 every real second (not game seconds, as game is paused).
        for (int i = 0; i < countdownDuration; i++)
        {
            timeRemaining -= 1;
            audioSource.PlayOneShot(tickSFX);
            yield return new WaitForSecondsRealtime(1);
        }
        
        // The countdown has ticked down to zero! 
        Time.timeScale = 1f; // UNPAUSES the game.
        gameStarted = true;
        Active(false); // Hides the countdown UI.

        // --> This is where we can add a function to play background music in our game! <-- //
        // e.g., audioSource.PlayOneShot(musicBGSFX); or playMusic(); or MusicManager.playMusic(); //
        musicAudioSource.PlayOneShot(bgMusic);
    }

    // Shows/Hides UI.
    void Active(bool active)
    {
        if (active) // If we pass "true" into the function, show the UI.
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else // If we pass "false" into the function, hide the UI.
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            cinematicBarsAnimator.SetTrigger("Out");
        }
    }


}
