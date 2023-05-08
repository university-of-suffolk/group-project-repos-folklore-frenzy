using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    bool coinCollected = false;
    private float percentChance;

    public int coinValue;

    [Header("Coin Rarities")]

    [Tooltip("5% Chance")]
    public int Legendary = 1000;
    [Tooltip("10% Chance")]
    public int UltraRare = 800;
    [Tooltip("15% Chance")]
    public int Rare = 600;
    [Tooltip("20% Chance")]
    public int Uncommon = 400;
    [Tooltip("50% Chance")]
    public int Common = 200;

    [Header("Sound Effects")]
    public AudioClip coinSFX;
    AudioSource audioSource;

    MeshRenderer meshRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // This occurs when the player character has entered the Coin GameObject trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coinCollected)
            {
                coinCollected = true;

                Debug.Log(coinValue);
                ScoreManager.currentScore += coinValue;

                audioSource.PlayOneShot(coinSFX);

                meshRenderer.enabled = false;
            }
        }
    }

    private void Start()
    {
        // Generates a random percentage.
        percentChance = Random.Range(0.0f, 1.0f);

        // This if-statement decides how much the coin is worth based on random percentage chance.

        if(percentChance >= 0.0f && percentChance <= 0.50f) // 50%
        {
            coinValue = Common;
        }
        else if(percentChance <= 0.70f) // 20%
        {
            coinValue = Uncommon;
        }
        else if(percentChance <= 0.85f) // 15%
        {
            coinValue = Rare;
        }
        else if(percentChance <= 0.95f) // 10%
        {
            coinValue = UltraRare;
        }
        else // 5%
        {
            coinValue = Legendary;
        }
    }

}
