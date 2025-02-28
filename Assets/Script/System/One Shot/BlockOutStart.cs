using Unity.Cinemachine;
using UnityEngine;
using static CamAsset;
using static MvtAxis;

public class BlockOutStart : MonoBehaviour
{
    [SerializeField] Transform ActChar; // Create a Reference to the actual Character
    [SerializeField] Transform ActCharCold; // Create a Reference to the actual Character
    [SerializeField] Transform Facing; // Create a reference to the actual movement reference
    [SerializeField] CinemachineCamera Cam; // Create a reference to the actual movement cam
    [SerializeField] CamInstance CamM; // Create a reference to the camera manager
    [SerializeField] CamCineInstance CamCineM; // Create a reference to the cinemachine manager
    [SerializeField] Vector3 CamPos; // Variable which follow the actual camera
    [SerializeField] LayerMask PMask; // LayerMask of the player
    [SerializeField] Dir FacingTag; // Enum that set the protractor which give the player a particular ways to walk
    [SerializeField] CamTag ActTag; // Enum that keep a track about the ID of the actual cam
    MainInstance M;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        M = MainInstance.I;
        M.ActChar = ActChar;
        M.ActCharCold = ActCharCold;
        M.Cam = Cam;
        M.CamM = CamM;
        M.CamPos = CamPos;
        M.CamCineM = CamCineM;
        M.ActTag = ActTag;
        M.Facing = Facing;
        M.FacingTag = FacingTag;
        M.PMask = PMask;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
