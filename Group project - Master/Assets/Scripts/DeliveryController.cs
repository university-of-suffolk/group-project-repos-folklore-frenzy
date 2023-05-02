using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    [SerializeField] float randomOrder;
    [SerializeField] float orderIndex;

    GameObject folkloreSpawner;

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

                // destroy customer after transaction
                Destroy(transform.parent.gameObject);
                folkloreSpawner.GetComponent<SpawnFolklore>().Spawn(); // Spawn new folklore!
            }
            else if (FolkloreTimerCountdown.deliveryFailed) // The delivery has been made over the time limit.
            {
                ScoreManager.currentScore -= 200f;
                folkloreSpawner.GetComponent<SpawnFolklore>().Spawn(); // Spawn new folklore!
                Destroy(transform.parent.gameObject);
            }

        }
    }
}
