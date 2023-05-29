using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSounds : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Sound Effects")]
    public AudioClip pickUpSFX;
    public AudioClip deliverySFX;

    public void deliverySoundWin(bool success)
    {
        if (success)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(deliverySFX);
        }
        else
        {
            audioSource.pitch = 0.7f;
            audioSource.PlayOneShot(deliverySFX);
        }
    }

    public void playPickUpSound()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(pickUpSFX);
    }

}
