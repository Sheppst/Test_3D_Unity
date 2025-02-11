using UnityEngine;

public class CharMovementCamIndp : MonoBehaviour
{
    [SerializeField] float speed; // Variable which give the speed of the character
    [SerializeField] float groundRadius; // Radius that detect the proximity thatt the character have with the ground
    [SerializeField] float crouchRadius; // Radius that detect the proximity thatt the character have with a roof
    [SerializeField] KeyCode Jump; // Variable which refer about what keybind do the jump
    [SerializeField] KeyCode Crouch; // Variable which refer about what keybind do the crouch
    [SerializeField] Transform Ground; // Origin of the point to detect the ground
    [SerializeField] Transform Roof; // Origin of the point to detect a roof
    [SerializeField] LayerMask Gmask; // Data mask that hide all other collider in the scene that not link in it

    CharControllerCamIndp C; // Reference to the controller of the character
    bool crouch; // Booleen that give the information that the character is crouching
    bool jump; // Booleen that give the autisation that the character can jump
    float horx; // Variable which give the information for the controller on, in what direction, the player go in the X-axis
    float horz; // Variable which give the information for the controller on, in what direction, the player go in the Z-axis
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        C = GetComponent<CharControllerCamIndp>(); // Link the reference with the component attached on the gameObject
    }

    // Update is called once per frame
    void Update()
    {
        horx = Input.GetAxisRaw("Horizontal"); // Assign the movement value 
        horz = Input.GetAxisRaw("Vertical"); // Assign the movement value
        if (Ground) // if the reference is not null
        {
            if (Input.GetKeyDown(Jump) && Physics.OverlapSphere(Ground.position, groundRadius, Gmask).Length >= 1) // Check if the Jump imput is pressed and the ground is detect 
                jump = true; // Authorize the jump
        }
        if (Roof)  // if the reference is not null
        {

        }
    }

    void FixedUpdate()
    {
        C.Movement(speed, horx, horz, jump, crouch); // Do the movement on the physic update
    }

    private void OnDrawGizmos() // Visual Debug
    {
        Gizmos.color = Color.red;
        if (Ground)
            Gizmos.DrawWireSphere(Ground.position, groundRadius);
        if (Roof)
            Gizmos.DrawWireSphere(Roof.position, crouchRadius);
    }
}
