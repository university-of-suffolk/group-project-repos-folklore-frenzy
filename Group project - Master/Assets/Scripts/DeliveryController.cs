using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    [SerializeField] float randomOrder;
    [SerializeField] float orderIndex;

    GameObject folkloreSpawner;
    GameObject cartFolklore; // This is the folklore inside of the cart!

    public AudioClip DeliverySFX;

    private void Start()
    {
        folkloreSpawner = GameObject.Find("Folklore Spawner");

        // determine which folklore the customer wants.
        orderIndex = PlayerInventory.folkloreIndex;

        FolkloreTimerCountdown.deliveryFailed = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided player in range");
            // check if have the folklore, in time
            if (PlayerInventory.folkloreIndex == orderIndex && !FolkloreTimerCountdown.deliveryFailed)
            {
                //if there is, add money + other events
                PlayerInventory.hasFolklore = false;

                switch (orderIndex)
                {
                    case 1f:
                        //add 200 yen
                        ScoreManager.currentScore += 200f;
                        break;
                    case 2f:
                        //add 400 yen
                        ScoreManager.currentScore += 400f;
                        break;
                    case 3f:
                        //add 800 yen
                        ScoreManager.currentScore += 800f;
                        break;
                    case 4f:
                        //add 1600 yen
                        ScoreManager.currentScore += 1600f;
                        break;
                    case 5f:
                        //add 3200 yen
                        ScoreManager.currentScore += 3200f;
                        break;

                }

                // destroy customer and cart folklore after transaction

                cartFolklore = other.transform.Find("FolkloreCartObject").gameObject;
                cartFolklore.SetActive(false);

                other.GetComponent<AudioSource>().pitch = 1f;
                other.GetComponent<AudioSource>().PlayOneShot(DeliverySFX); // Play delivery sound from the player!

                Destroy(transform.parent.gameObject);
                folkloreSpawner.GetComponent<SpawnFolklore>().Spawn(); // Spawn new folklore!
            }
            else if (FolkloreTimerCountdown.deliveryFailed) // The delivery has been made over the time limit.
            {
                // Removes folklore inside of cart
                cartFolklore = other.transform.Find("FolkloreCartObject").gameObject;
                cartFolklore.SetActive(false);

                ScoreManager.currentScore -= 200f;

                other.GetComponent<AudioSource>().pitch = 0.7f;
                other.GetComponent<AudioSource>().PlayOneShot(DeliverySFX); // Play delivery sound from the player!

                folkloreSpawner.GetComponent<SpawnFolklore>().Spawn(); // Spawn new folklore!
                Destroy(transform.parent.gameObject);
            }

        }
    }
}
