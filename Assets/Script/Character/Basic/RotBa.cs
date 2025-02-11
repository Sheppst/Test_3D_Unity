using UnityEngine;

public class RotBa : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float MouseSens;
    [SerializeField] float MaxLookDown = 90;
    [SerializeField] float MaxLookUp = -90;
    float MouseX;
    float MouseY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        VisuRotat();
    }

    void VisuRotat()
    {
        Vector3 Xrotat = Vector3.right * Mathf.Clamp(-MouseY, MaxLookUp, MaxLookDown);
        Vector3 ActRot = cam.localEulerAngles + Xrotat;
        cam.localRotation = Quaternion.Euler(ActRot);
        transform.Rotate(Vector3.up * MouseX);
    }
}
