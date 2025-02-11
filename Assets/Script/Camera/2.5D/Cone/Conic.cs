#if UNITY_EDITOR // Compilator operator which respond to an certain condition to ignore it if there a build 
using UnityEditor; 
#endif // End of the compilator operator
using UnityEngine;
using static CamInstance;

public class Conic : MonoBehaviour
{
    [SerializeField] Transform RO; // Reference of the origin of the right wall of detection
    [SerializeField] Transform RE; // Reference of the end of the right wall of detection
    [SerializeField] Transform LO; // Reference of the origin of the left wall of detection
    [SerializeField] Transform LE; // Reference of the end of the left wall of detection
    [SerializeField] Cameratype CamType; // Assign a type for the camera 
    Transform Character;
    Transform Rail;
    Ray Left; // Litteraly a ray to do the detection of the left side 
    Ray Right; // Litteraly a ray to do the detection of the right side 
    Ray Front; // Litteraly a ray to do the detection of the front side 
    Ray Back; // Litteraly a ray to do the detection of the back side 
    bool FirstTouch; // Verify if the target touch the raycast for the first time, if true, set the firstPosition
    Vector3 FirstPositon; // Keep an eye on the firstposition where the target touch 

    private void Start()
    {
        Rail = MainInstance.I.CamM.CameraRefs[CamType].transform;
    }

    // Update is called once per frame
    void Update()
    {
        Character = MainInstance.I.ActChar; // Link the reference 
        float LimL = Vector3.Distance(LO.position, LE.position); // Récupère la distance entre le point A et B // Distance(A,B);
        float LimR = Vector3.Distance(RO.position, RE.position); // Récupère la distance entre le point A et B // Distance(A,B);
        float LimF = Vector3.Distance(LE.position, RE.position); // Récupère la distance entre le point A et B // Distance(A,B);
        float LimB = Vector3.Distance(RO.position, LO.position); // Récupère la distance entre le point A et B // Distance(A,B);
        Vector3 TargLeft = LE.position - LO.position; // Récupère la direction entre un point A et B // Vector3 = B - A;    
        Vector3 TargRight = RE.position - RO.position; // Récupère la direction entre un point A et B // Vector3 = B - A;
        Vector3 TargFront = RE.position - LE.position; // Récupère la direction entre un point A et B // Vector3 = B - A;
        Vector3 TargBack = LO.position - RO.position; // Récupère la direction entre un point A et B // Vector3 = B - A;
        Left = new Ray(LO.position, TargLeft); // Crée le Ray allant dans la direction mentionné sous le nom de la variable
        Right = new Ray(RO.position, TargRight); // Crée le Ray allant dans la direction mentionné sous le nom de la variable
        Front = new Ray(LE.position, TargFront); // Crée le Ray allant dans la direction mentionné sous le nom de la variable
        Back = new Ray(RO.position, TargBack); // Crée le Ray allant dans la direction mentionné sous le nom de la variable
        Debug.DrawRay(LO.position, TargLeft,Color.red); // Dessine un ray de déboguage de couleur rouge selon les même paramêtre que le Ray à la même position
        Debug.DrawRay(RO.position, TargRight, Color.red); // Dessine un ray de déboguage de couleur rouge selon les même paramêtre que le Ray à la même position
        Debug.DrawRay(LE.position, TargFront, Color.red); // Dessine un ray de déboguage de couleur rouge selon les même paramêtre que le Ray à la même position
        Debug.DrawRay(RO.position, TargBack, Color.red); // Dessine un ray de déboguage de couleur rouge selon les même paramêtre que le Ray à la même position
        if (Physics.Raycast(Left,LimL, MainInstance.I.PMask)) // Si le Raycast touche le character
        {
            Debug.DrawRay(LO.position, TargLeft, Color.green); // Dessine un ray des même position que le Ray qui à été envoyé de couleur verte
            Debug.Log("Touché L"); // Renvoie un message de console que tel limite à été touché
            CamMovement();
        }
        else if (Physics.Raycast(Right, LimR, MainInstance.I.PMask)) // Si le Raycast touche le character
        {
            Debug.DrawRay(RO.position, TargRight, Color.green); // Dessine un ray des même position que le Ray qui à été envoyé de couleur verte
            Debug.Log("Touché R"); // Renvoie un message de console que tel limite à été touché
            CamMovement();
        }
        else if (Physics.Raycast(Front, LimF, MainInstance.I.PMask)) // Si le Raycast touche le character
        {
            Debug.DrawRay(LE.position, TargFront, Color.green); // Dessine un ray des même position que le Ray qui à été envoyé de couleur verte
            Debug.Log("Touché F"); // Renvoie un message de console que tel limite à été touché
            CamMovement();
        }
        else if (Physics.Raycast(Back, LimB, MainInstance.I.PMask)) // Si le Raycast touche le character
        {
            Debug.DrawRay(RO.position, TargBack, Color.green); // Dessine un ray des même position que le Ray qui à été envoyé de couleur verte
            Debug.Log("Touché B"); // Renvoie un message de console que tel limite à été touché
            CamMovement();
        }
        else if (FirstTouch && FirstPositon != Vector3.zero) // if the target touch a raycast and the follow position is not set to default (default can be change if Vector3.zero is to light) 
        {
            FirstTouch = false; // Reset the authorization
            FirstPositon = Vector3.zero; // Reset the follower point
        }
    }
    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        #if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            Gizmos.DrawLine(LO.position, LE.position);
            Gizmos.DrawLine(RO.position, RE.position);
            return;
        }
        #endif
    }

    void CamMovement()
    {
        if (!FirstTouch) 
        {
            FirstTouch = true;
            FirstPositon = Character.position;
        }

    }

    //private float BoundRay(float y)
    //{
    //    float MaxVis = y;
    //    if (y > POV.position.y && !(CheckBound(Target.position.x, TopLimit.position) > Target.position.y))
    //    {
    //        MaxVis = CheckBound(Target.position.x, TopLimit.position);
    //    }
    //    else if (y < POV.position.y && !(CheckBound(Target.position.x, BotLimit.position) < Target.position.y))
    //    {
    //        MaxVis = CheckBound(Target.position.x, BotLimit.position);
    //    }
    //    MaxVis = Mathf.Clamp(MaxVis, BotLimit.position.y, TopLimit.position.y);
    //    // MaxVis /= Vector2.Distance(POV.position, Target.position); // Pour créer une véritable sensation de cône de détection
    //    return MaxVis;
    //}

    //private float CheckBound(float x, Vector3 Limit) // si erreur voire l'origine
    //{
    //    //float y = (Limit.y - POV.position.y) / (Limit.x - POV.position.x) * (x - POV.position.x)  + POV.position.y;
    //    Vector2 LimtRel = Limit - POV.position;
    //    float a = LimtRel.y / LimtRel.x * (x - POV.position.x) + POV.position.y;
    //    DebugObj.position = new Vector2(x, a);
    //    return a;
    //}
    private bool DetectFrtOrBck(bool front)
    {
        if (front)
        {

        }
        else 
        {

        }
        return false;
    }
}
