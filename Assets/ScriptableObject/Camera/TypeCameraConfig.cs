using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Scriptable Objects/TypeCameraConfig")]
public class TypeCameraConfig : ScriptableObject
{
    public enum CamTypRef
    {
        None,
        Rail,
        Follow,
        Elevating,
        Roof,
        Spy,
    }
    [Header ("Data")]
    public CamTypRef Type;
}
