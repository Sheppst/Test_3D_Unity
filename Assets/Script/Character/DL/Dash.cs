using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Refs")]
    public Collider Body;
    Rigidbody rb;
    PlayerMovementAdvanced PM;

    [Header("Dashing")]
    public bool dashCastTime;
    public float dashProcess;
    float dashRange;
    bool focus;


    [Header("Input")]
    public KeyCode DashKey = KeyCode.LeftAlt;
    [Range(0, 6)] public int DashMouse = 0;
    public KeyCode StopFocus = KeyCode.X;
    [Range(0, 6)] public int StopMouse = 1;

    [Header("Visu")]
    public Material DashPreshot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PM = GetComponent<PlayerMovementAdvanced>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ChosenOne = Vector3.zero;
        RaycastHit hitInfo;
        if (Input.GetKeyDown(DashKey) || Input.GetMouseButtonDown(DashMouse))
            Focus();
        if (Input.GetKeyDown(StopFocus) ||  Input.GetMouseButtonDown(StopMouse))
            focus = false;
        if (focus && Physics.Raycast(transform.position, transform.forward * dashRange,out hitInfo, dashRange, ~MainInstance.I.PMask))
        {
            ChosenOne = hitInfo.point;

        }
        if (Input.GetKeyUp(DashKey) && PM.Dash && focus)
            DoDash(ChosenOne);
    }

    void Focus()
    {
        focus = true;

    }

    void DoDash(Vector3 Destination)
    {

    }
}
