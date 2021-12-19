using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittlePlatforms : MonoBehaviour
{
    public float TTL; //Time to live
    private float countdownTimer;
    private bool startCountDown;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            startCountDown = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.countdownTimer = this.TTL;
        this.startCountDown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.startCountDown){
            countdownTimer -= Time.deltaTime;
            if (this.countdownTimer <= 0){
                 Destroy(gameObject);
            }
        }
    }
}
