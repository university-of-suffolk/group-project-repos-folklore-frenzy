using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public AudioClip[] impactSFX;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetTrigger("Smash");


            int impactNo = Random.Range(0, impactSFX.Length);

            audioSource.PlayOneShot(impactSFX[impactNo]);
        }
    }
}
