using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Componants")]
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask building;
    bool hitBuilding;

    [Header("Speed Controls")]
    [SerializeField] float Speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float speedChange = 5;

    [SerializeField] float pushbackForce;

    float AppliedSpeed;
    [SerializeField] bool freezeTurn;
    
    [Header("Turn controls")]
    [SerializeField] float turnSens = 10f;
    [HideInInspector] public Vector3 MovementDirection;

    [Header("Input")]
    float Horizontal;
    float Vertical;
    float newRotation;

    bool scoreChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hitBuilding = Physics.Raycast(transform.position, transform.forward, 1f, building);

        // Only get the player input when the game is not paused. (I.e., the player can't move while paused)
        if (!PauseManager.isPaused)
        {
            // get directional input
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
        }

        if (Horizontal == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }

        // change rotation of cart when going left or right
        if (Horizontal != 0 && !freezeTurn)
        {
            print("Turning");
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
        if (!hitBuilding)
        {
            // set the maxSpeed
            if (rb.velocity.magnitude < Speed) // if going fowards
            {
                Debug.Log("Applying forward force");
                rb.drag = 0f; // remove the drag so the player can affectivly accelerate
                AppliedSpeed = Speed * 500;
                rb.AddForce(MovementDirection * AppliedSpeed * Time.fixedDeltaTime, ForceMode.Force); // apply the forwards force.
            }
            else if (rb.velocity.magnitude > Speed) // if slowing down apply drag to do it gradually.
            {
                rb.drag = 5f;
            }
        }
        else // turning off foward force, to cleanly apply the bouceback when the player collides with an obstacle.
        {
            print("collided with building");
            freezeTurn = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.drag = 0f;
            Speed = 5; // lower speed to give the player a chance to correct their mistake without bouncing them off the same wall repeatedly.

            //rb.velocity = Vector3.zero;
            rb.AddForce(MovementDirection * -1 * pushbackForce * 500f * Time.fixedDeltaTime, ForceMode.Force); // applies force backwards to get players unstuck.

            // Decrease score on collision.
            if (!scoreChanged)
            {
                scoreChanged = true;
                ScoreManager.currentScore -= 100;
            }

            Invoke("unfreezeTurn", 0.35f /** Time.fixedDeltaTime*/); // unfreeze the rotate (avoiding the player jittering against the obstacle)
        }
        
    }

    private void unfreezeTurn()
    {
        print("Unfreezing turn");
        freezeTurn = false;
        rb.constraints = RigidbodyConstraints.None;
        scoreChanged = false; // Player will lose money again on next collision.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1f);
    }
}
