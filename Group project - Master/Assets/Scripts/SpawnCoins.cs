using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject coinSpawnLocations;
    Transform defaultSpawn;
    bool coinAssigned;
    int randomCoinPos;
    bool coinSpawned = false;

    private void Start()
    {
        // Random Coin Spawn Locations //  

        for(int i = 0; i < 5; i++)
        {
            coinAssigned = true;
            coinSpawned = false;

            while (coinAssigned)
            {
                randomCoinPos = Random.Range(0, 9); // 10* Possible Locations (10 Empty Child GameObjects)

                if(coinSpawnLocations.transform.GetChild(randomCoinPos).childCount == 1)
                {
                    //coinAssigned = true;
                }
                else
                {
                    coinAssigned = false;
                    break;
                }
 
            }
            
            // This takes the random number, and finds the selected array from the Empty GameObjects inside of the coinSpawnLocations GameObject (e.g., The highest Child GameObject is represented as [0]). The default spawn is then selected from the array.
            defaultSpawn = coinSpawnLocations.transform.GetChild(randomCoinPos);

            Debug.Log(defaultSpawn);

            if (!coinSpawned)
            {
                coinSpawned = true;
                GameObject coinObject = Instantiate(coinPrefab, defaultSpawn); // This instantiates 
            }
        }
    }
}
