using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float ParallaxEffect; 
    private Vector3 _lastCamPos;

    void Start()
    {
        _lastCamPos = Cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = Cam.position - _lastCamPos;
        transform.position += new Vector3(delta.x * ParallaxEffect, delta.y * ParallaxEffect, 0);
        _lastCamPos = Cam.position;
    }
}