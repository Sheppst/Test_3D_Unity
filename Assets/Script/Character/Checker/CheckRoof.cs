using System.Linq;
using UnityEngine;

public class CheckRoof : MonoBehaviour
{
    public static bool In;
    Vector3 Scale;
    Vector3 Position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Scale = transform.lossyScale / 2;
        //Scale.y *= 2;
        //Position = transform.position;
        //Position.y /= 2;
        //In = Physics.OverlapBox(transform.position, transform.lossyScale / 2, Quaternion.identity).Any(tag => tag.tag == "PlayerBody");
        Debug.Log(In);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
            In = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerBody"))
            In = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Position, Scale * 2);
    }
}
