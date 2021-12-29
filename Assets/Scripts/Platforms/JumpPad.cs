using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float boost = 15;

    //If player collides with the jump potion it will increase the players jump height by the boost amount
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().setBoostAllowedViaJumpPad(true);
            other.gameObject.GetComponent<PlayerMovement>().setBoostForce(this.boost);
        }
    }

    //While the player collides with the jump potion it will increase the players jump height by the boost amount
    void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().setBoostAllowedViaJumpPad(true);
            other.gameObject.GetComponent<PlayerMovement>().setBoostForce(this.boost);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
