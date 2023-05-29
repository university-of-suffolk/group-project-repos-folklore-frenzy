using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MinimapSymbolController : MonoBehaviour
{

    public GameObject cart;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cart.transform.position.x, cart.transform.position.y + 5f, cart.transform.position.z);
    }
}
