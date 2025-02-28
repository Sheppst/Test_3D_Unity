using System.Linq;
using UnityEngine;

public class CharMovementCamIndp : MonoBehaviour
{
    [SerializeField] float speed; // Variable which give the speed of the character
    [SerializeField] float groundRadius; // Radius that detect the proximity thatt the character have with the ground
    [SerializeField] float crouchRadius; // Radius that detect the proximity thatt the character have with a roof
    [SerializeField] float crouchHeight; // Height of the character when the crouch button is pressed or there a roof on his head
    [SerializeField] float maxSldTm; // Timer which will keep an eye on the building speed coefficient 
    [SerializeField] float sldFr; // Force that it will be add to push the character during the slide
    [SerializeField] KeyCode Jump; // Variable which refer about what keybind do the jump
    [SerializeField] KeyCode Crouch; // Variable which refer about what keybind do the crouch
    [SerializeField] Transform Ground; // Origin of the point to detect the ground
    [SerializeField] Transform Roof; // Origin of the point to detect a roof
    [SerializeField] LayerMask Gmask; // Data mask that hide all other collider in the scene that not link in it

    CharControllerCamIndp C; // Reference to the controller of the character
    bool crouch; // Booleen that give the information that the character is crouching
    bool jump; // Booleen that give the autisation that the character can jump
    bool CheckMvt; // Booleen which check if the player is still pressing a movemnt input
    float horx; // Variable which give the information for the controller on, in what direction, the player go in the X-axis
    float horz; // Variable which give the information for the controller on, in what direction, the player go in the Z-axis
    float sldTm;

    public enum MvtMod // Only used to inform other 
    {
        None,
        Walk,
        Run,
        Jump,
        Crouch,
    }
    public MvtMod ActMvtMod;

    public enum State // Only use to inform itself
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Crouching
    }
    public State ActSt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        C = GetComponent<CharControllerCamIndp>(); // Link the reference with the component attached on the gameObject
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckMvt = false;
        if (CheckForMovement())
            CheckMvt = true;
        horx = Input.GetAxisRaw("Horizontal"); // Assign the movement value 
        horz = Input.GetAxisRaw("Vertical"); // Assign the movement
        if ((horx != 0 | horz != 0) && ActSt != State.Crouching)
        {
            ActSt = State.Running;
        }
        if (Ground) // if the reference is not null
        {
            if (Input.GetKeyDown(Jump) && Physics.OverlapSphere(Ground.position, groundRadius, Gmask).Length >= 1) // Check if the Jump imput is pressed and the ground is detect 
                jump = true; // Authorize the jump
        }
        if (Input.GetKeyDown(Crouch) && ActSt != State.Crouching)
        {
            GetComponent<CapsuleCollider>().height /= 2;
            ActSt = State.Crouching;
        }
        if ((!Input.GetKey(Crouch) || !Input.anyKey) && Physics.OverlapSphere(Roof.position, crouchRadius, Gmask).Any(tag => tag.CompareTag("Roof")) && ActSt == State.Crouching)
        {
            GetComponent<CapsuleCollider>().height *= 2;
            if (C.vel.magnitude <= 0.01)
                ActSt = State.Idle;
            else 
                ActSt = State.Running;
        }
        C.ActMvtMod = ActMvtMod;
        Debug.Log(Physics.OverlapSphere(Roof.position, crouchRadius, Gmask).Length);
        Debug.Log(Physics.OverlapSphere(Roof.position, crouchRadius, Gmask).Any(tag => tag.CompareTag("Roof")));
    }

    void FixedUpdate()
    {
        C.Movement(speed, horx, horz, jump, crouch, CheckMvt, speed/2); // Do the movement on the physic update
    }

    private void OnDrawGizmos() // Visual Debug
    {
        Gizmos.color = Color.red;
        if (Ground)
            Gizmos.DrawWireSphere(Ground.position, groundRadius);
        if (Roof)
            Gizmos.DrawWireSphere(Roof.position, crouchRadius);
    }

    bool CheckForMovement()
    {
        if (C.Targ == Vector3.zero)
            return true;
        if (Input.GetAxisRaw("Horizontal") != horx || Input.GetAxisRaw("Vertical") != horz)
            return true;
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            return true;
        return false;
    }
}
