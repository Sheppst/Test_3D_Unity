using UnityEngine;

public class CBa : MonoBehaviour
{
    [SerializeField] float SmTm = 0.5f; // Smootime for the use of SmoothDamp
    Rigidbody rb; // Instance of the Rigibody
    Vector3 vel = Vector3.zero; // Variable which keep an eye on the actual linearVelocity of the rigibody, the initial value give a burst in a precise direction  
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Link of the instance
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement(float speed, float horX, float horZ) // Method that give the force to the body of the character to move
    {
        Vector3 targ = speed * horX * transform.right + speed * horZ * transform.forward; // Apply a target on worldSpace and stock it in a local variable
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targ, ref vel, SmTm); // Give the linearVelocity the needed force to move
    }
}
