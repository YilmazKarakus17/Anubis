using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private Rigidbody2D r2d;
    private bool fly;
    private Vector2 target;
    private Vector2 currentVelocity = Vector2.zero;
    public GameObject TargetObject;
    private PlayerScript player;

    public float smoothTime = 10;
    
    void OnCollisionEnter2D(Collision2D otherCollider) {
        if (otherCollider.gameObject.tag == "Floor") {
            // Once the soul hits the ground, it will slow fly to the player.
            r2d.gravityScale = 0;
            fly = true;
        }
        if (otherCollider.gameObject.tag == "Player") {
            player.addOneSoul();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        r2d = gameObject.GetComponent<Rigidbody2D>();
        player = TargetObject.GetComponent<PlayerScript>();
        // At creation, the soul will act as a projectile, flying to a location.
        r2d.AddForce(new Vector2(Random.Range(-1, 1), 7.5f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (fly && TargetObject != null) {
            target = TargetObject.transform.position; // Gets the 2D coordinates of the player.
            // The soul flies slowly towards the player (NOT PROPERLY IMPLEMENTED!!!)
            transform.position = Vector2.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
        }
    }
}
