using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    //Player object inserted here
    public Transform player;


    // void late update is used so it happens after update and fixed update
    void LateUpdate()
    {
        // Changes the camera position to the players postion on the y axis
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Changes the camera rotation to the players rotation
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
