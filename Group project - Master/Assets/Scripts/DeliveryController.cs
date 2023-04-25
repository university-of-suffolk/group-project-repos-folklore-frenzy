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
        // get random which folklore it is based on the % chance  ( Generate a 1 - 100 number)
        randomOrder = Random.Range(1, 101);

        if (randomOrder < 51)
        {
            // Is Kuchisake Onna (Slit-Mouthed Woman) with value of 200
            orderIndex = 1f;
        }
        else if (randomOrder < 71)
        {
            // Is Aka Manto (Red Cloak) with value of 400
            orderIndex = 2f;
        }
        else if (randomOrder < 86)
        {
            // Is Yuki Onna (Snow Woman) with value of 800
            orderIndex = 3f;
        }
        else if (randomOrder < 96)
        {
            // Is Gashadokuro (Starving Skeleton) with value of 1600
            orderIndex = 4f;
        }
        else
        {
            // Is Oni (Demon) with value of 3200
            orderIndex = 5f;
        }

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
                        // remove 200 yen
                        break;
                    case 2f:
                        //remove 400 yen
                        break;
                    case 3f:
                        //remove 800 yen
                        break;
                    case 4f:
                        //remove 1600 yen
                        break;
                    case 5f:
                        //remove 3200 yen
                        break;

                }

                // destroy customer after transaction
                Destroy(transform.parent.gameObject, 2f);
            }

            

            //else do nothing (events to be added)
        }
    }
}
