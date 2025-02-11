using System.Collections.Generic;
using UnityEngine;
using static TypeCameraConfig;

public class MainInstance : MonoBehaviour
{
    public static MainInstance I; // Create an instance for the script
    public Transform ActChar; // Create a Reference to the actual Character
    public Transform Cam; // Create a reference to the actual Cam
    public CamInstance CamM; // Create a reference to the camera manager
    public Vector3 CamPos; // Variable which follow the actual camera
    public LayerMask PMask; // LayerMask of the player
    [SerializeField] List<Component> Instance; // Pick every instance-script-like in a list and sort them out in awake
    private void Awake()
    {
        I = this; // Initialization of the Main 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
