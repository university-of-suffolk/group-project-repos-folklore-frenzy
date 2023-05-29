using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPins : MonoBehaviour
{
    public int strikeCoolDown;
    bool onCooldown;

    public AudioClip strikeSFX;
    public AudioClip screamSFX;
    AudioSource audioSource;

    public int PinStrength;

    private void Start()
    {
        onCooldown = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!onCooldown)
            {
                onCooldown = true;
                GetComponent<Animator>().SetTrigger("Strike");
                audioSource.PlayOneShot(strikeSFX);
                audioSource.PlayOneShot(screamSFX);

                other.GetComponent<PlayerMove>().Speed -= PinStrength;

                StartCoroutine(StrikeCooldown());
            }
        }
    }

    IEnumerator StrikeCooldown()
    {
        yield return new WaitForSeconds(strikeCoolDown);
        onCooldown = false;
        GetComponent<Animator>().SetTrigger("Strike");
    }

    
}
