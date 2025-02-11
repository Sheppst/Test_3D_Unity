using UnityEngine;

public class LimFollow : MonoBehaviour
{
    [SerializeField] Transform Rail; // Reference of the Rail of the Camera
    [SerializeField] Transform Chara; // Reference of the appearance of the character

    // Update is called once per frame
    void Update()
    {
        if (Rail.gameObject.activeSelf) // If the Rail is Active do the modification
        {
            transform.parent = null; // Detach the transform of the Rail
            Vector3 Placement = Rail.position; // Create a local variable which will set the position of this object to the horizontal rail position
            Placement.y = Chara.position.y; // Set the vertical position to the appearance of the character to be sure to touch it
            transform.localPosition = Placement; // Apply the custom position
            transform.rotation = Rail.rotation; // This line can be deleted if we want to keep the lines out of the rail rotation
        }
        else // if the rail isn't active, Reset the properities of the limits 
        {// Set it position, rotation and family tie with the Rail as a global reference
            transform.SetPositionAndRotation(Rail.position, Rail.rotation);
            transform.parent = Rail;
        }
    }
}
