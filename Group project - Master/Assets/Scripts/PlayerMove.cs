using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Componants")]
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask building;
    bool hitBuilding;
    [SerializeField] GameObject orientation;

    [Header("Speed Controls")]
    [HideInInspector] public float Speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float speedChange = 5;

    [SerializeField] float pushbackForce;

    float AppliedSpeed;
    [SerializeField] bool freezeTurn;

    [SerializeField] Vector3 reboundDirection;

    public float gravityScale;

    [Header("Rotation Controll")]
    [SerializeField] Vector3 frontLeftOffset;
    [SerializeField] Vector3 frontRightOffset;
    [SerializeField] Vector3 backLeftOffset;
    [SerializeField] Vector3 backRightOffset;

    //distances
    [SerializeField] float frontLeftDistance;
    [SerializeField] float frontRightDistance;
    [SerializeField] float backLeftDistance;
    [SerializeField] float backRightDistance;


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

        rotationController();
        
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

        ////Debug.Log("Rotation" + transform.eulerAngles);
        //if (Mathf.Abs(transform.eulerAngles.x) > 45 || Mathf.Abs(transform.eulerAngles.z) > 45)
        //{
        //    // Fix for flip, Limits player rotation to 45 degrees
        //    //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        //    //transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);

        //    Debug.Log("Reset Rotation");
        //}

        //rotationController();

    }

    private void FixedUpdate()
    {

        //Add relative downwards force to replace gravity
        rb.AddForce(transform.up * -1 * gravityScale, ForceMode.Acceleration);

        // change rotation of cart when going left or right
        if (Horizontal != 0 && !freezeTurn)
        {
            print("Turning");
            rb.constraints = RigidbodyConstraints.None;
            // if there is horizontal input rotate
            newRotation = Horizontal * turnSens * Time.fixedDeltaTime;
            transform.Rotate(0, newRotation, 0, Space.World);
            // change the velocity to be in the direction of travel so it wont drift
            Vector3 newVelocity = rb.velocity.magnitude * transform.forward;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        }

        if (!hitBuilding || Mathf.Round(rb.velocity.magnitude) != 0)
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
            
            rb.drag = 0f;
            Speed = 5; // lower speed to give the player a chance to correct their mistake without bouncing them off the same wall repeatedly.

            //rb.velocity = Vector3.zero;
            rb.AddForce( reboundDirection.normalized * pushbackForce * 500f * Time.fixedDeltaTime, ForceMode.Force); // applies force backwards to get players unstuck.

            // Decrease score on collision.
            if (!scoreChanged)
            {
                scoreChanged = true;
                ScoreManager.currentScore -= 100;
            }

            hitBuilding = false;

            Invoke("unfreezeTurn", 0.35f /** Time.fixedDeltaTime*/); // unfreeze the rotate (avoiding the player jittering against the obstacle)
        }
    }

    private void rotationController()
    {
        // Will be used to controll rotation on event that it is possible to topple the player

        //gameObject.transform.eulerAngles = new Vector3(orientation.transform.eulerAngles.x, transform.eulerAngles.y, orientation.transform.eulerAngles.z);
        Debug.Log("ROTATION CONTROL ACTIVATED");
    }

    private void unfreezeTurn()
    {
        print("Unfreezing turn");
        freezeTurn = false;
        rb.constraints = RigidbodyConstraints.None;
        scoreChanged = false; // Player will lose money again on next collision.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building") || collision.gameObject.CompareTag("Pedestrian"))
        {
            Debug.Log("Collide with obstacle");

            rb.constraints = RigidbodyConstraints.FreezeRotation;

            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;

            reboundDirection = gameObject.transform.position - position;

            //rb.constraints = RigidbodyConstraints.FreezeRotationX;
            //rb.constraints = RigidbodyConstraints.FreezeRotationZ;

            //reboundDirection = gameObject.transform.position - collision.gameObject.transform.position;
            reboundDirection.y = 0f;
            hitBuilding = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (transform.up));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            other.GetComponent<Animator>().SetTrigger("Smash");
        }
    }
}
