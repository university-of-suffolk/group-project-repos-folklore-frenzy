using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer line;

    Vector3 targetPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Folklore(Clone)") != null && !PlayerInventory.hasFolklore)
        {
            targetPos = GameObject.FindGameObjectWithTag("Folklore").transform.position;

            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            line.SetPosition(0, playerPos);
            line.SetPosition(1, targetPos);
        }
        else if(GameObject.Find("Customer(Clone)") != null && PlayerInventory.hasFolklore)
        {
            targetPos = GameObject.FindGameObjectWithTag("Customer").transform.position;

            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            line.SetPosition(0, playerPos);
            line.SetPosition(1, targetPos);
        }

        



        
    }
}
