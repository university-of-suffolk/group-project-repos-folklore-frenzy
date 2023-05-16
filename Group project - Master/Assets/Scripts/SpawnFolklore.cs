using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFolklore : MonoBehaviour
{
    [SerializeField]GameObject Folklore;
    Transform defaultSpawn;
    GameObject folkloreSpawnLocations;

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

        // Random Folklore Spawn Locations //

        folkloreSpawnLocations = GameObject.Find("FolkloreSpawnLocations"); // Finds the GameObject storing folklore spawn locations.

        if (tutorial)
        {
            tutorial = false;
            randomFolklorePos = 0; // The first delivery will always spawn the folklore at the first spawn location.
        }
        else
        {
            randomFolklorePos = Random.Range(0, 3); // 3* Possible Locations (3 Empty Child GameObjects)
        }

        // This takes the random number, and finds the selected array from the Empty GameObjects inside of the folkloreSpawnLocations GameObject (e.g., The highest Child GameObject is represented as [0]). The default spawn is then selected from the array.
        defaultSpawn = folkloreSpawnLocations.transform.GetChild(randomFolklorePos);

        Instantiate(Folklore, defaultSpawn);
    }
}
