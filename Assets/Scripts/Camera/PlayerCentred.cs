using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCentred : MonoBehaviour
{
    public GameObject Player;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector2 targetV2 = Player.transform.position; // Gets the 2D coordinates of the 2D object that represents the end of the level.
        Vector3 targetV3 = transform.position; // Gets the current 3D position of the GameObject it's assigned to, in this case it's the camera
        //Only changes the x and y so that the cameras original z position isn't affected.
        targetV3.x = targetV2.x; 
        targetV3.y = targetV2.y + yOffset; //Offsets the camera's y to not have the player object directly in the middle
        transform.position = targetV3;
    }
}
