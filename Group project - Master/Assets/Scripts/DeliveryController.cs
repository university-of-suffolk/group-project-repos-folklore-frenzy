using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryController : MonoBehaviour
{
    [SerializeField] float randomOrder;
    [SerializeField] float orderIndex;

    private void Start()
    {
        // determine which folklore the customer wants.
        orderIndex = PlayerInventory.folkloreIndex;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided player in range");
            // check if have the folklore, in time
            if (PlayerInventory.folkloreIndex == orderIndex /*&& time*/)
            {
                //if there is, add money + other events
                PlayerInventory.hasFolklore = false;

                switch (orderIndex)
                {
                    case 1f:
                        //add 200 yen
                        //ScoreManager.currentScore += 200f;
                        break;
                    case 2f:
                        //add 400 yen
                        //ScoreManger.currentScore += 400f;
                        break;
                    case 3f:
                        //add 800 yen
                        //ScoreManger.currentScore += 800f;
                        break;
                    case 4f:
                        //add 1600 yen
                        //ScoreManager.currentScore += 1600f;
                        break;
                    case 5f:
                        //add 3200 yen
                        //ScoreManager.currentScore += 3200f;
                        break;

                }

                // destroy customer after transaction
                Destroy(transform.parent.gameObject);
            }
            //else if (time < 0)
            //{
                ////remove 200 yen;
                ////ScoreManager.currentScore -= 200f;
            //}
        }
    }
}
