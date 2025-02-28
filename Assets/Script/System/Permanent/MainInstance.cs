using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static CamAsset;
using static MvtAxis;

public class MainInstance : MonoBehaviour
{
    public static MainInstance I; // Create an instance for the script
    public Transform ActChar; // Create a Reference to the actual Character
    public Transform ActCharCold; // Create a Reference to the actual Character
    public Transform Facing; // Create a reference to the actual movement reference
    public CinemachineCamera Cam; // Create a reference to the actual movement cam
    public CamInstance CamM; // Create a reference to the camera manager
    public CamCineInstance CamCineM;
    public Vector3 CamPos; // Variable which follow the actual camera
    public LayerMask PMask; // LayerMask of the player
    public Dir FacingTag; // Enum that set the protractor which give the player a particular ways to walk
    public CamTag ActTag; // Enum that keep a track about the ID of the actual cam
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
