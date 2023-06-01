using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("Componants")]
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask building;
    [SerializeField] LayerMask ramp;
    bool hitBuilding;

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

    [Header("Turn controls")]
    [SerializeField] float turnSens = 10f;
    [HideInInspector] public Vector3 MovementDirection;
    [SerializeField] float rampCheckOffset;
    [SerializeField] float rampCheckdistance;
    [SerializeField] bool isRamp;

    [Header("Input")]
    float Horizontal;
    float Vertical;
    float newRotation;
    [SerializeField] KeyCode driftKey = KeyCode.Space;

    bool scoreChanged = false;

    [Header("Effects")]
    // Damage vignette
    public GameObject vignette;
    Animator vignetteAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        vignetteAnim = vignette.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only get the player input when the game is not paused. (I.e., the player can't move while paused)
        if (Time.timeScale == 1f)
        {
            // get directional input
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
        }

        if (Horizontal == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
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

        //Add relative downwards force to replace gravity
        rb.AddForce(transform.up * -1 * gravityScale, ForceMode.Acceleration);

        // change rotation of cart when going left or right
        if (Horizontal != 0 && !freezeTurn)
        {
            //print("Turning");
            rb.constraints = RigidbodyConstraints.None;
            // if there is horizontal input rotate
            newRotation = Horizontal * turnSens * Time.fixedDeltaTime;
            transform.Rotate(0, newRotation, 0, Space.World);
            // change the velocity to be in the direction of travel so it wont drift
            if (!Input.GetKey(driftKey))
            {
                Vector3 newVelocity = rb.velocity.magnitude * transform.forward;
                rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
            }
        }

        if (!hitBuilding)
        {
            // set the maxSpeed
            if (rb.velocity.magnitude < Speed) // if going fowards
            {
                //Debug.Log("Applying forward force");
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
            //print("collided with building");
            freezeTurn = true;
            vignetteAnim.SetTrigger("Fade"); // Displays red vignette on damage
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

            Invoke("unfreezeTurn", 0.35f); // unfreeze the rotate (avoiding the player jittering against the obstacle)
        }
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
        if (collision.gameObject.CompareTag("Building"))
        {
            Debug.Log("Collide with obstacle");

            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;

            reboundDirection = transform.forward * -1;
            //reboundDirection.y = 0f;
            hitBuilding = true;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + Vector3.down * rampCheckOffset, transform.position + Vector3.down * rampCheckOffset + transform.forward * rampCheckdistance);

        //Gizmos.DrawLine(transform.position + frontLeftOffset, transform.position + frontLeftOffset + transform.forward * collisionRange);
        //Gizmos.DrawLine(transform.position + frontMiddleOffset, transform.position + frontMiddleOffset + transform.forward * collisionRange);
        //Gizmos.DrawLine(transform.position + frontRightOffset, transform.position + frontRightOffset + transform.forward * collisionRange);
        //Gizmos.DrawLine(transform.position + leftFrontOffset, transform.position + leftFrontOffset + transform.right * -1 * collisionRange);
        //Gizmos.DrawLine(transform.position + leftBackOffset, transform.position + leftBackOffset + transform.right * -1 * collisionRange);
        //Gizmos.DrawLine(transform.position + rightFrontOffset, transform.position + rightFrontOffset + transform.right * collisionRange);
        //Gizmos.DrawLine(transform.position + rightBackOffset, transform.position + rightBackOffset + transform.right * collisionRange);
        //Gizmos.DrawLine(transform.position + leftCornerOffset, transform.position + leftCornerOffset + (transform.right * -1) + transform.forward * collisionRange);
        //Gizmos.DrawLine(transform.position + rightCornerOffset, transform.position + rightCornerOffset + transform.right + transform.forward * collisionRange);
    }
}
