using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static CamAsset;

public class CamCineInstance : MonoBehaviour
{
    [SerializeField] CamAsset[] AssetCam;
    public Dictionary<CamTag, CinemachineCamera> CamRef = new();

    private void Awake()
    {
        foreach (var c in AssetCam)
        {
            if (!CamRef[c.camTag])
                CamRef.Add(c.camTag, c.CineCam);
        }
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
