using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] Rigidbody rb;

    [SerializeField] Transform orientation;

    [HideInInspector] public Vector3 MovementDirection;

    [SerializeField] float Speed;
    [SerializeField] float maxSpeed;

    bool move;
    
    [SerializeField] float turnSens = 10f;

    float newRotation;

    float Horizontal;
    float Vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // get directional input
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        // change rotation of cart when going left or right
        if (Horizontal != 0)
        {
            // if there is horizontal input rotate

            newRotation += Horizontal * turnSens * Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.x, newRotation, transform.rotation.z);

            // change the velocity to be in the direction of travel so it wont drift
            Vector3 newVelocity = rb.velocity.magnitude * orientation.forward;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z) * Vertical;

        }

        if (Vertical != 0) 
        {
            move = true;
        }
        else
        {
            move = false;
        }

        // calculate the direction of movement

        MovementDirection = transform.forward * Vertical;

        Debug.Log(Horizontal);
    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.drag = 0;

            float topSpeed = maxSpeed;

            // sets the top speed depending on if the player is going forwards or backwards
            if (Vertical < 0)
            {
                 topSpeed = maxSpeed / 2;
            }
            else
            {
                topSpeed = maxSpeed;
            }

            Debug.Log(topSpeed);

            // set the maxSpeed
            if (rb.velocity.magnitude < topSpeed) // if going fowards
            {
                rb.AddForce(Vector3.forward + MovementDirection * Speed * Time.fixedDeltaTime, ForceMode.Force);
            }

        }
        else
        {
            rb.drag = 3f;
        }
    }
}
