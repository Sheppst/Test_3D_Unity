using TMPro;
using UnityEngine;

public class DetectEnter : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Transform Setter;
    [SerializeField] TextMeshPro Text;
    [SerializeField] TextMeshPro TextTarg;

    bool Enter;
    bool FalseExit;
    bool Exit;
    bool In;
    float SmallestScale;
    string b;
    // Update is called once per frame
    void Update()
    {
        Setter.rotation = transform.rotation; // Set the rotation of the point that the Target touch 
        FSC(); // Take the smallest data scale // Obsolete
        string a = "Entrer"; // Debug 
        
        if (Exit)
        {
            Enter = false;
            Exit = false;
            b = "Sortir";
        }
        if (FalseExit)
        {
            Enter = false;
            FalseExit = false;
            b = "Revenir";
        }
        if (Setter && Text)
        {
            Text.text = "Distance : " + Vector3.Distance(Setter.position, Target.position) +
                /* "\n Scale Vector : " + DetecterTwo.localScale.x +" "+ DetecterTwo.localScale.y + " " + DetecterTwo.localScale.z +*/ 
                "\n Exit Type : " + a + "->" + b;
        }
        TextTarg.text = "In " + In;
    }

    private void FixedUpdate()
    {
        In = false; // Set the In Booleen to false, if the Target is not in
        Collider[] CheckBox = Physics.OverlapBox(Setter.position, transform.localScale / 2, transform.rotation);
        for (int i = 0; i < CheckBox.Length; i++)
        {
            if (CheckBox[i].transform == Target)
            {
                In = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Setter.position = GetComponent<Collider>().ClosestPoint(other.transform.position);

        Enter = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (Enter)
        {
            if (In)
                FalseExit = true;
            else
                Exit = true; // Plus fiable pour detecter dans n'importe quel sens de sortie au dépit d'un calcul moins précis si la target se déplace trop vite
            //if (Vector3.Distance(Setter.position,other.transform.position) < Vector3.Distance(transform.position,other.transform.position))
            //    FalseExit = true;
            //else
            //    Exit = true;// Plus fiable pour detecter quelque soit la vitesse au dépit d'un calcul moins précis si la target se décale trop sur un côté 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Setter.position, transform.localScale);
        Gizmos.DrawRay(Setter.position, transform.up * transform.lossyScale.y/2);
        Gizmos.DrawRay(Setter.position, -transform.up * transform.lossyScale.y/2);
        Gizmos.DrawRay(Setter.position, transform.right * transform.lossyScale.x/2);
        Gizmos.DrawRay(Setter.position, -transform.right * transform.lossyScale.x/2);
        Gizmos.DrawRay(Setter.position, transform.forward * transform.lossyScale.z / 2);
        Gizmos.DrawRay(Setter.position, -transform.forward * transform.lossyScale.z / 2);
    }

    void FSC()
    {
        SmallestScale = transform.localScale.x;
        if (SmallestScale > transform.localScale.y)
            SmallestScale = transform.localScale.y;
        if (SmallestScale > transform.localScale.z)
            SmallestScale = transform.localScale.z;
    }

    Vector3 CheckHor()
    {
        Vector3 R = transform.localScale; R.x /= 2;
        if (transform.localScale.x > transform.localScale.y)
        {
            R = transform.localScale; R.y /= 2;
            if (transform.localScale.y > transform.localScale.z)
                R = transform.localScale; R.z /= 2;
        }
        else if (transform.localScale.x > transform.localScale.z)
        {
            R = transform.localScale; R.z /= 2;
        }
        return R;
    }
}
