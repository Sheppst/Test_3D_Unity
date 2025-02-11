using UnityEngine;

public class MvtBa : MonoBehaviour
{
    [SerializeField] float speed; // Variable which give the character it movement speed
    CBa C; // Instance of the script which act on it physics 
    float horX; // Variable wich stock the right/left movement input of the user
    float horZ;// Variable wich stock the front/backward movement input of the user
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        C = GetComponent<CBa>(); // Link the instance
    }

    // Update is called once per frame
    void Update()
    {
        horX = Input.GetAxisRaw("Horizontal"); // Apply the movement data in the variable
        horZ = Input.GetAxisRaw("Vertical"); // Apply the movement data in the variable
    }

    private void FixedUpdate()
    {
        C.Movement(speed, horX, horZ); // Call the Movement method which will do the physics
    }
}
