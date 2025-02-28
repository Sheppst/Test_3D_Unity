using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float groundDrag;
    [Header ("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public Collider Body;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handlinbg")]
    public float MaxSlopeAngle;
    public RaycastHit slopeHit;
    bool exitingSlope;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 Point;

    Rigidbody rb;

    public enum MVtState
    {
        walking,
        sprinting,
        crouching,
        air,
    }
    public MVtState state;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        if (Body)
        {
            RaycastHit hit;
            // ground check
            Vector3 or = new Vector3(Body.bounds.center.x, Body.bounds.min.y, Body.bounds.center.z);
            grounded = Physics.Raycast(or, Vector3.down, out hit, 0.15f, whatIsGround);
            Debug.DrawRay(or, Vector3.down * 0.15f, Color.blue);
        }
            //Collider[] tab = Physics.OverlapSphere(GroundCheck.position, 0.15f, whatIsGround);
            //grounded = tab.Length != 0;

        MyInput();
        SpeedControl();
        if (!CheckRoof.In)
            StateHandler();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping= 0.1f;

        if (Input.GetKeyDown(crouchKey))
        {
            Body.isTrigger = true;
        }
        else if (!Input.GetKey(crouchKey) && !CheckRoof.In)
        {
            Body.isTrigger = false;
        }
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }

    void StateHandler()
    {
        if (grounded && Input.GetKey(crouchKey))
        {
            state = MVtState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MVtState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MVtState.walking;
            moveSpeed = walkSpeed;
        }
        else
            state = MVtState.air;   
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // limit velocity if needed
            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

    }

    private void Jump()
    {

        exitingSlope = true;
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        Vector3 or = new Vector3(Body.bounds.center.x, Body.bounds.min.y, Body.bounds.center.z);
        if (Physics.Raycast(or, Vector3.down, out slopeHit, 0.15f, whatIsGround))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < MaxSlopeAngle && angle != 0f;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow + Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position,0.15f);
    }
}