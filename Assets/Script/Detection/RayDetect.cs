using UnityEngine;

public class RayDetect : MonoBehaviour
{
    public enum LayerEn
    {
        GroupLayer1,
        GroupLayer2,
    }

    public enum ActionPossible
    {
        Bounce,
        Stop,
        Push,
        Pull
    }
    [SerializeField] float RayRange;
    [SerializeField] float PushOrPullStrengh;
    [SerializeField] LayerMask[] Masks;
    [SerializeField] Transform SpawnSafe;
    bool hit;
    Ray R;
    Rigidbody rb;
    Transform Character;

    public LayerEn MaskIndex;
    public ActionPossible ReactAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        SpawnSafe.parent = null;
    }
    void Start()
    {    
        R = new();
        rb = GetComponent<Rigidbody>();
        Character = MainInstance.I.ActChar;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character)
            Character = MainInstance.I.ActChar;
        hit = false;
        R = new Ray(transform.position,Vector3.down * RayRange);
        RaycastHit[] ray = Physics.RaycastAll(R, Mathf.Infinity, Masks[(int) MaskIndex]);
        for (int i = 0; i < ray.Length; i++) 
        {
            hit = true;
        }
        if (hit)
            DoSmthWhDet(ReactAction);
        if (!Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity ,Masks[(int)LayerEn.GroupLayer1]))
        {
            transform.position = SpawnSafe.position;
            transform.rotation = Quaternion.identity;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    void DoSmthWhDet(ActionPossible a)
    {
        if (a == ActionPossible.Stop)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (a == ActionPossible.Push)
            PushOrPull(true);
        else if (a == ActionPossible.Pull)
            PushOrPull(false);
        else if (a == ActionPossible.Bounce)
        {
            rb.AddForce(-rb.linearVelocity, ForceMode.Acceleration);
        }
    }

    void PushOrPull(bool Sw)
    {
        if (Sw)
        {
            Vector3 Targ = transform.position - Character.position; Targ.y = 0; Targ.Normalize();
            rb.AddForce(Targ * PushOrPullStrengh, ForceMode.Impulse);
        }
        else
        {
            Vector3 Targ =  Character.position - transform.position; Targ.y = 0; Targ.Normalize();
            rb.AddForce(Targ * PushOrPullStrengh, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (hit)
            Gizmos.color = Color.green;
        Gizmos.DrawRay(R);

    }
}
