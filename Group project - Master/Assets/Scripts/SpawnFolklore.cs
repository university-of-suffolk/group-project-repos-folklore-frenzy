using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFolklore : MonoBehaviour
{
    [SerializeField]GameObject Folklore;
    [SerializeField]Transform defaultSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn() // This will just spawn the prefab, not decide which one it is, That will be done by the collection scrip on awake.
    {
        Debug.Log("spawn folklore");
        Instantiate(Folklore, defaultSpawn);

        yield return new WaitForSecondsRealtime(20f);
        StartCoroutine(Spawn());
    }
}
