using UnityEngine;

public class ModifOnlyOneCoordinate : MonoBehaviour
{
    public enum Coords
    {
        Y,
        X,
        Z
    }
    public Coords ModifCoords;
    public Transform FollowObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FollowObject)
            return;
        else
        {
            Replace();
        }
    }

    void Replace()
    {
        if (ModifCoords == Coords.Y)
        {
            transform.position = new Vector3(FollowObject.position.x, transform.position.y, FollowObject.position.z);
        }
        else if (ModifCoords == Coords.X)
        {
            transform.position = new Vector3(transform.position.x, FollowObject.position.y, FollowObject.position.z);
        }
        else if (ModifCoords == Coords.Z)
        {
            transform.position = new Vector3(FollowObject.position.x, FollowObject.position.y, transform.position.z);
        }
    }
}
