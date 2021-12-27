using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healthPoints = 0;
    [SerializeField] private bool oneTimeUse = true;

    //If player collides with the health potion it will increase the players hp by the healthPoints
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Player>().increaseHealthByPoint(this.healthPoints);
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