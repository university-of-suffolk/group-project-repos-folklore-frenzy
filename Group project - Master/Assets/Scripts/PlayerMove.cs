using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Componants")]
    [SerializeField] Rigidbody rb;

    [Header("Speed Controls")]
    [SerializeField] float Speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float speedChange = 5;
    float AppliedSpeed;
    
    [Header("Turn controls")]
    [SerializeField] float turnSens = 10f;
    [HideInInspector] public Vector3 MovementDirection;

    [Header("Input")]
    float Horizontal;
    float Vertical;
    float newRotation;

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

        if (Horizontal == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }

        // change rotation of cart when going left or right
        if (Horizontal != 0)
        {
            rb.constraints = RigidbodyConstraints.None;
            // if there is horizontal input rotate
            newRotation += Horizontal * turnSens * Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.x, newRotation, transform.rotation.z);

            // change the velocity to be in the direction of travel so it wont drift
            Vector3 newVelocity = rb.velocity.magnitude * transform.forward;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        }
        // check for vertical input and change the speed
        if (Vertical != 0) 
        {
            if (Vertical > 0) // going forwards
            {
                if (Speed < maxSpeed)
                {
                    Speed += speedChange * Time.deltaTime;
                }
            }
            else if (Vertical < 0)
            {
                // deccellerate when backwards is pressed
                if (Speed > minSpeed)
                {
                    Speed -= speedChange * Time.deltaTime;
                }
            }
        }
        // calculate the direction of movement
        MovementDirection = transform.forward;   
    }

    private void FixedUpdate()
    {
         // set the maxSpeed
         if (rb.velocity.magnitude < Speed) // if going fowards
         {
            rb.drag = 0f;
            AppliedSpeed = Speed * 500;
            rb.AddForce((/*Vector3.forward */MovementDirection) * AppliedSpeed * Time.fixedDeltaTime, ForceMode.Force);
         }
         else if (rb.velocity.magnitude > Speed)
         {
            rb.drag = 5f;
         }
    }
}
