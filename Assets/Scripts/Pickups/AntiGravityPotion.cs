using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityPotion : MonoBehaviour
{
    [SerializeField] private bool oneTimeUse = true;

    //If player collides with the health potion it will increase the players hp by the healthPoints
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = other.gameObject.GetComponent<Rigidbody2D>().gravityScale*-1;
            Vector3 scaler = other.gameObject.transform.localScale;
            scaler.y *= -1;
            other.gameObject.transform.localScale = scaler;
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
