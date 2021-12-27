using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostPotion : MonoBehaviour
{
    [SerializeField] private float boost = 15;
    [SerializeField] private bool oneTimeUse = true;

    //If player collides with the jump potion it will increase the players jump height by the boost amount
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().setBoostAllowed(true);
            other.gameObject.GetComponent<PlayerMovement>().setBoostForce(this.boost);
            if (this.oneTimeUse){
                Destroy(gameObject);
            }
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