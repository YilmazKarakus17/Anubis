using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public GameObject Player;
    public float distanceLimit;
    void FixedUpdate() {
        Vector2 dist = new Vector2(transform.position.x-Player.transform.position.x, transform.position.y-Player.transform.position.y);
        float distance = dist.magnitude;
        if (distance < distanceLimit && distance > 0 || distance > 0 && distance < (distanceLimit*-1)) {
            transform.localScale = new Vector3(2.0f, 2.0f, 0f);
        }
    }
}
