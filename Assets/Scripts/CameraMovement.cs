using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 target;

    public float smoothTime;

    public GameObject TargetObject;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 targetV2 = TargetObject.transform.position; // Gets the 2D coordinates of the 2D object that represents the end of the level.
        Vector3 targetV3 = transform.position; // Gets the current 3D position of the GameObject it's assigned to, in this case it's the camera
        targetV3.x = targetV2.x; targetV3.y = targetV2.y; //Only changes the x and y so that the cameras original z position isn't affected.
        this.target = targetV3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, this.target, ref currentVelocity, smoothTime);
    }
}
