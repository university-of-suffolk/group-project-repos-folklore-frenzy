using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFolklore : MonoBehaviour
{
    // get which fokelore this is
    // check if the player is near
    // delete when the player collects it

    [SerializeField] float randomFolklore;
    [SerializeField] float newfolkloreIndex;

    // Start is called before the first frame update
    void Start()
    {
        // get random which folklore it is based on the % chance  ( Generate a 1 - 100 number)
        randomFolklore = Random.Range(1, 101);

        if (randomFolklore < 51)
        {
            // Is Kuchisake Onna (Slit-Mouthed Woman) with value of 200
            newfolkloreIndex = 1f;
        }
        else if (randomFolklore < 71)
        {
            // Is Aka Manto (Red Cloak) with value of 400
            newfolkloreIndex = 2f;
        }
        else if (randomFolklore < 86)
        {
            // Is Yuki Onna (Snow Woman) with value of 800
            newfolkloreIndex = 3f;
        }
        else if (randomFolklore < 96)
        {
            // Is Gashadokuro (Starving Skeleton) with value of 1600
            newfolkloreIndex = 4f;
        }
        else
        {
            // Is Oni (Demon) with value of 3200
            newfolkloreIndex = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // if the player enters the trigger of the folklore check if the player already has a folklore before proceeding.
        if (other.CompareTag("Player") && !PlayerInventory.hasFolklore)
        {
            // store the value of the folklore that the player has in the inventory
            PlayerInventory.hasFolklore = true;
            // store the index of the folklore that the player has in the inventory
            PlayerInventory.folkloreIndex = newfolkloreIndex;

            Destroy(gameObject);
        }
    }
}
