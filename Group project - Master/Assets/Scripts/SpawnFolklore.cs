using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFolklore : MonoBehaviour
{
    [SerializeField]GameObject Folklore;
    Transform defaultSpawn;
    GameObject folkloreSpawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        // Random Folklore Spawn Locations //

        folkloreSpawnLocations = GameObject.Find("FolkloreSpawnLocations"); // Finds the GameObject storing folklore spawn locations.

        int randomFolklorePos = Random.Range(0, 3); // 3* Possible Locations (3 Empty Child GameObjects)

        // This takes the random number, and finds the selected array from the Empty GameObjects inside of the folkloreSpawnLocations GameObject (e.g., The highest Child GameObject is represented as [0]). The default spawn is then selected from the array.
        defaultSpawn = folkloreSpawnLocations.transform.GetChild(randomFolklorePos);


        // Instantiating Initial Folklore //

        Spawn();
    }

    public void Spawn()
    {
        Debug.Log("spawn folklore");
        Instantiate(Folklore, defaultSpawn);
    }



    /*
    public IEnumerator Spawn() // This will just spawn the prefab, not decide which one it is, That will be done by the collection script on awake. This function can be called elsewhere.
    {
        Debug.Log("spawn folklore");
        Instantiate(Folklore, defaultSpawn);
        yield return null;
    }
    */
}
