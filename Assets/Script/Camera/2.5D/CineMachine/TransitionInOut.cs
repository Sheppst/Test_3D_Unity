using Unity.Cinemachine;
using UnityEngine;
using static CamAsset; // Get the class in static to pick the enum CamTag

public class TransitionInOut : MonoBehaviour
{
    public static CamTag ActTag; // ID of the actual camera
    [SerializeField] CamTag TransTag; // ID of the Camera that we'll use to do the transition
    public CinemachineCamera ActCam;  // Actual Cinemachine Component
    [SerializeField] CinemachineCamera TransCam; // Cinemachine component that we'll use to do the transition
    public bool trans; // Booleen which trigger the transition 

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActCam = MainInstance.I.Cam; // Set permanantly the actual cam with the reference in the main instance
        ActTag = MainInstance.I.ActTag; // Set permanantly the actual cam ID with the reference in the main instance
        if (!TransCam)
        {
            Debug.LogError("ActTag n'est pas renseigné");
            return;
        }
        if (!TransCam)
        {
            Debug.LogError("ActCam n'est pas renseigné");
            return;
        }
        if (trans)
        {// Do the change and modify the transition variable to the ancient one
            MainInstance.I.ActTag = TransTag; TransTag = ActTag;  
            MainInstance.I.Cam = TransCam; TransCam = ActCam;
            trans = false; // Set off the the transition booleen
        }
    }
}
