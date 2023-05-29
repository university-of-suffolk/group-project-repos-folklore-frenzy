using JetBrains.Annotations;
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
    public GameObject Customer;
    GameObject customerSpawnLocations;
    Transform defaultSpawn;

    GameObject cartFolklore; // This is the folklore we see inside the cart!

    public static bool tutorialCustomer = true;
    int randomCustomerPos;

    // Start is called before the first frame update
    void Start()
    {
        // Random Customer Spawn Locations //

        customerSpawnLocations = GameObject.Find("CustomerSpawnLocations"); // Finds the GameObject storing customer spawn locations.

        if (tutorialCustomer)
        {
            tutorialCustomer = false;
            randomCustomerPos = 0; // The first customer always goes to this location!
        }
        else
        {
            // 10* Possible Locations (10 Empty Child GameObjects)
            print("RANDOMISE CUSTOMER SPAWN " + SpawnFolklore.currentFolklore);
            if (SpawnFolklore.currentFolklore == 1)
            {
                //1 or 8
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 0; }
                else { randomCustomerPos = 7; }
                Debug.Log("POSITION 1: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 2)
            {
                //5 or 6
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 4; }
                else { randomCustomerPos = 5; }
                Debug.Log("POSITION 2: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 3)
            {
                // 7 and 5
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 6; }
                else { randomCustomerPos = 4; }
                Debug.Log("POSITION 3: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 4)
            {
                //2 7 4
                int RandPos = Random.Range(0, 3);
                if (RandPos == 1) { randomCustomerPos = 1; }
                else if (RandPos == 2) { randomCustomerPos = 6; }
                else { randomCustomerPos = 3; }
                Debug.Log("POSITION 4: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 5)
            {
                //2 3 9
                int RandPos = Random.Range(0, 3);
                if (RandPos == 1) { randomCustomerPos = 1; }
                else if (RandPos == 2) { randomCustomerPos = 2; }
                else { randomCustomerPos = 8; }
                Debug.Log("POSITION 5: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 6)
            {
                //3 10
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 2; }
                else { randomCustomerPos = 9; }
                Debug.Log("POSITION 6: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 7)
            {
                // 1 6 8
                int RandPos = Random.Range(0, 3);
                if (RandPos == 1) { randomCustomerPos = 1; }
                else if (RandPos == 2) { randomCustomerPos = 5; }
                else { randomCustomerPos = 7; }
                Debug.Log("POSITION 7: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 8)
            {
                // 10 6
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 4; }
                else { randomCustomerPos = 9; }
                Debug.Log("POSITION 8: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 9)
            {
                // 5 9
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 4; }
                else { randomCustomerPos = 8; }
                Debug.Log("POSITION 9: " + RandPos + "," + randomCustomerPos);
            }
            else if (SpawnFolklore.currentFolklore == 10)
            {
                //5 10
                int RandPos = Random.Range(0, 2);
                if (RandPos == 1) { randomCustomerPos = 4; }
                else { randomCustomerPos = 9; }
                Debug.Log("POSITION 10: " + RandPos + "," + randomCustomerPos);
            }
        }
        print("Random Customer Position: " + randomCustomerPos);
        // This takes the random number, and finds the selected array from the Empty GameObjects inside of the customerSpawnLocations GameObject (e.g., The highest Child GameObject is represented as [0]). The default spawn is then selected from the array.
        defaultSpawn = customerSpawnLocations.transform.GetChild(randomCustomerPos);
        print("Spawn: " + defaultSpawn);

        // get random which folklore it is based on the % chance  ( Generate a 1 - 100 number)
        randomFolklore = Random.Range(1, 101);

        // Set Folklore //

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

            //Instantiate the customer.
            Instantiate(Customer, defaultSpawn);

            //Show folklore inside cart!
            cartFolklore = other.transform.Find("FolkloreCartObject").gameObject;
            cartFolklore.SetActive(true);
            Destroy(gameObject);
        }
    }
}
