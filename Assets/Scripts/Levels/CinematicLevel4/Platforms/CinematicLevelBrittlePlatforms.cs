using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicLevelBrittlePlatforms : MonoBehaviour
{
    [SerializeField] private GameObject parentElement;
    [SerializeField] private float TTL; //Time to live
    private float countdownTimer;
    private bool startCountDown;

    [SerializeField] private float tiltAngle;
    private float tilt;
    private float direction;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            startCountDown = true;
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            startCountDown = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.countdownTimer = this.TTL;
        this.startCountDown = false;
        this.direction = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.startCountDown){
            countdownTimer -= Time.deltaTime;
            this.direction *= -1;
            this.tilt = tiltAngle;
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tilt*direction);
            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime*5);
            if (this.countdownTimer <= 0){
                Destroy(this.parentElement);
                //Destroy(gameObject);
            }
        }
    }
}
