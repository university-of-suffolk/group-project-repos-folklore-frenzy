using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed;
    void FixedUpdate()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
