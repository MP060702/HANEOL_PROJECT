using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = new Vector3(smoothed.x, smoothed.y, transform.position.z);
    }
}