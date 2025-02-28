using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CamInstance : MonoBehaviour
{
    [SerializeField] List<GameObject> Camera;
    public enum Cameratype
    {
        None,
        Conic,
    }
    public Dictionary<Cameratype, GameObject> CameraRefs = new();
    public Cameratype ActCam;
    public float SmthTm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i  = 0; i < Camera.Count; i++) 
        {
            CameraRefs.Add(Camera[i].GetComponent<GenProperties>().CamType, Camera[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Cameratype obj in CameraRefs.Keys) 
        {
            if (obj == ActCam)
            {
                CameraRefs[obj].SetActive(true);
            }
            else
            {
                CameraRefs[obj].SetActive(false);
            }
        }
    }
}
