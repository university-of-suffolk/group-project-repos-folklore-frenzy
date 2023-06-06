using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody BoxBody;
    public float LaunchSpeed = 50f;

    private void Start()
    {
        BoxBody = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider ragdoll)
    {
        bool isPed = ragdoll.gameObject.CompareTag("Player");
        if (isPed == true) 
        {
            BoxBody.AddForce(Vector3.forward * LaunchSpeed,ForceMode.Impulse);
            BoxBody.useGravity = false;
        }
    }
}
