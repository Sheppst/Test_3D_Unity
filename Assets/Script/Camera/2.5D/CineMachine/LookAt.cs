using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Target;
    // Update is called once per frame
    void Update()
    {
        Vector3 targ = Target.position;
        targ.y = transform.position.y;
        transform.LookAt(targ);
    }
}
