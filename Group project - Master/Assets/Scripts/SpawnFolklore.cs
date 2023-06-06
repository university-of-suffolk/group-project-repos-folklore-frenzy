using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SpawnFolklore : MonoBehaviour
{
    [SerializeField]GameObject Folklore;

    [Header("Folklore")]
    [SerializeField] GameObject kuchisakeOnna;
    [SerializeField] GameObject akaManto;
    [SerializeField] GameObject yukiOnna;
    [SerializeField] GameObject gashadokuro;
    [SerializeField] GameObject oni;

    Transform defaultSpawn;
    GameObject folkloreSpawnLocations;
    public static int currentFolkloreLocation;

    bool tutorial = true;

    int randomFolklorePos;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiating Initial Folklore //

        Spawn();
    }

    public void Spawn()
    {
        Debug.Log("spawn folklore");

        // Set Folklore
        // get random which folklore it is based on the % chance  ( Generate a 1 - 100 number)
        int randomFolklore = Random.Range(0, 100);
        if (randomFolklore < 51)
        {
            // Is Kuchisake Onna (Slit-Mouthed Woman) with value of 200
            Folklore = kuchisakeOnna;
            PlayerInventory.folkloreIndex = 1;
        }
        else if (randomFolklore < 71)
        {
            // Is Aka Manto (Red Cloak) with value of 400
            Folklore = akaManto;
            PlayerInventory.folkloreIndex = 2;
        }
        else if (randomFolklore < 86)
        {
            // Is Yuki Onna (Snow Woman) with value of 800
            Folklore = yukiOnna;
            PlayerInventory.folkloreIndex = 3;
        }
        else if (randomFolklore < 96)
        {
            // Is Gashadokuro (Starving Skeleton) with value of 1600
            Folklore = gashadokuro;
            PlayerInventory.folkloreIndex = 4;
        }
        else
        {
            // Is Oni (Demon) with value of 3200
            Folklore = oni;
            PlayerInventory.folkloreIndex = 5;
        }

        // Random Folklore Spawn Locations //

        folkloreSpawnLocations = GameObject.Find("FolkloreSpawnLocations"); // Finds the GameObject storing folklore spawn locations.

        if (tutorial)
        {
            tutorial = false;
            randomFolklorePos = 0; // The first delivery will always spawn the folklore at the first spawn location.
        }
        else
        {
            randomFolklorePos = Random.Range(0, 10); // 10* Possible Locations (10 Empty Child GameObjects)
            currentFolkloreLocation = randomFolklorePos;
        }

        // This takes the random number, and finds the selected array from the Empty GameObjects inside of the folkloreSpawnLocations GameObject (e.g., The highest Child GameObject is represented as [0]). The default spawn is then selected from the array.
        defaultSpawn = folkloreSpawnLocations.transform.GetChild(randomFolklorePos);

        Instantiate(Folklore, defaultSpawn);
    }
}
