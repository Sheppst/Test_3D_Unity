using UnityEngine;
using static CharMovementCamIndp;

public class CharControllerCamIndp : MonoBehaviour
{
    [SerializeField] float SmthTm = 0.5f; // Variable about the transition time of the character velocity
    Transform Cam; // Reference of the actual active camera
    Rigidbody rb; // Reference on the rigibody of the gameObject
    [HideInInspector] public Vector3 vel = Vector3.zero; // Variable which keep a track on the velocity of the body
    [HideInInspector] public MvtMod ActMvtMod;
    [HideInInspector] public Vector3 Targ;
    MvtMod LastMvtMod;
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
            Cam = MainInstance.I.Facing; // else refresh on what the actual camera is 
        }
    }

    public void Movement(float speed, float horX, float horZ, bool Jump, bool Crouch, bool block, float SlowCrouch, float runCoef = 1, float jumpStrength = 1) // Main method which move the characterk, do the jump, do the slide
    {
        if (Cam)
        {
            if (ActMvtMod == MvtMod.Crouch && vel.magnitude <= SlowCrouch)
            {
                Targ = (Cam.right * horX + Cam.forward * horZ) * speed * SlowCrouch; // Create the direction where the character will lead it step
                Targ.y = 0;
                Targ = Targ.normalized * speed;
            }

            else if (vel.magnitude > SlowCrouch)
            {
                Targ = (Cam.right * horX + Cam.forward * horZ) * speed * runCoef ;
                Targ.y = 0;
                Targ = Targ.normalized * speed;
            }

            if (ActMvtMod == MvtMod.Walk)
            {
                Targ = (Cam.right * horX + Cam.forward *  horZ) * speed; // Create the direction where the character will lead it step
                Targ.y = 0;
                Targ = Targ.normalized * speed;
            }

            
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, Targ, ref vel, SmthTm); // Apply the velocity on the body smoothed on the direction + speed obtained
            
            if (Jump) 
            {
                rb.AddForce(Vector3.up * jumpStrength);
            }
        }
    }
}
