using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CamInstance : MonoBehaviour
{
    [SerializeField] List<GameObject> Camera;
    public enum Cameratype
    {

    }
    Dictionary<CameraType, GameObject> CameraRefs;
    public CameraType ActCam;
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
        foreach(CameraType obj in CameraRefs.Keys) 
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
