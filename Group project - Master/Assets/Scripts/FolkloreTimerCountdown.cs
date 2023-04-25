using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolkloreTimerCountdown : MonoBehaviour
{
    // Variables
    [Header("Text UI")]
    public TextMeshProUGUI TimerText;
    [Header("Time Values")]
    public int LifeTime = 30;
    private float CurrentTime;

    //Animator anim;

    // This boolean makes sure that if the player runs out of time to deliver the Folklore GameObject, the function DeliveryFail() is called once.
    private bool deliveryState = false;

    // This boolean is used to freeze the timer when the player makes a successful delivery.
    private bool freezeTimer = false;

    void Start()
    {
        // This starts the Folklore GameObject with the timer reset at LifeTime (30s).
        CurrentTime = LifeTime;

        TimerText = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // This automatically finds the first child in the Canvas - the TextMeshProUGui. This is great for instantiating!

        //anim = transform.GetChild(0).GetComponent<Animator>(); // This is used to add a little shake to the TextMeshProUGui when the timer reaches below 10.
    }

    void Update()
    {
        // The timer should only continue to decrement and fail the player if the delivery has not been made.
        if (!freezeTimer)
        {
            // If the timer reaches 0, freeze the timer!
            if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                //anim.SetTrigger("Fade"); // Fade out animation.
                DeliveryFail();
            }
            else // Run this code if the timer has not reached 0!
            {
                // Time.deltaTime decrements the currentTime every second, instead of every frame, and updates the timer UI text.
                CurrentTime -= 1 * Time.deltaTime;
                UpdateTimer();
            }
        }
        else // The timer has frozen, meaning the player has made the delivery!
        {
            DeliverySuccess();
        }

        // If the timer is below 10, begin to shake!
        if(Mathf.Round(CurrentTime) <= 10)
        {
            //anim.SetTrigger("Shake"); // Shake animation.
        }
    }

    // This function updates the timer UI text.
    void UpdateTimer()
    {
        TimerText.text = CurrentTime.ToString("0"); // The "0" makes the float value a whole number!
    }

    // This function is called when the player fails to make the delivery in time.
    void DeliveryFail()
    {
        if (!deliveryState)
        {
            deliveryState = true;
            Debug.Log("The current Folklore GameObject " + gameObject.transform.parent.name + " was delivered unsuccessfully!");
        }
    }

    void DeliverySuccess()
    {
        if (!deliveryState)
        {
            deliveryState = true;
            Debug.Log("The current Folklore GameObject " + gameObject.transform.parent.name + " was delivered successfully!");
        }
    }
}
