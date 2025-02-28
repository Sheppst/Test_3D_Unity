using UnityEngine;

public class MvtAxis : MonoBehaviour
{

    public enum Dir
    {
        None,
        Front,
        Back,
        Right,
        Left,
    }
    public Dir ActDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActDir = MainInstance.I.FacingTag;
        if (ActDir == Dir.None)
            Debug.LogError("La direction n'est pas renseigné");
        else if (ActDir == Dir.Front)
            transform.eulerAngles = Vector3.zero;
        else if (ActDir == Dir.Back)
            transform.eulerAngles = Vector3.up * 180;
        else if (ActDir == Dir.Left)
            transform.eulerAngles = Vector3.up * 90;
        else if (ActDir == Dir.Right)
            transform.eulerAngles = Vector3.down * 90;
    }
}
