using UnityEngine;

public class CharControllerCamIndp : MonoBehaviour
{
    [SerializeField] float SmthTm = 0.5f; // Variable about the transition time of the character velocity
    Transform Cam; // Reference of the actual active camera
    Rigidbody rb; // Reference on the rigibody of the gameObject
    Vector3 vel = Vector3.zero; // Variable which keep a track on the velocity of the body
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Link the reference with
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey) // if any key is actif right now, skip this
        {
            Cam = MainInstance.I.Cam; // else refresh on what the actual camera is 
        }
    }

    public void Movement(float speed, float horX, float horZ, bool Jump, bool Crouch) // Main method which move the characterk, do the jump, do the slide
    {
        if (Cam)
        {
            Vector3 Targ = (Cam.right * horX + Cam.forward *  horZ) * speed; // Create the direction where the character will lead it step
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, Targ, ref vel, SmthTm); // Apply the velocity on the body smoothed on the direction + speed obtained
        }
    }
}
