using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "CamAsset", menuName = "Scriptable Objects/CamAsset")]
public class CamAsset : ScriptableObject
{
    public enum CamTag
    {
        None,
    }

    [Header ("Reference")]
    public Transform CamRef;
    public CinemachineCamera CineCam;

    [Header("Data")]
    public CamTag camTag;
}
