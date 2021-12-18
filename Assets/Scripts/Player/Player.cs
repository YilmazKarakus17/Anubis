using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    private Rigidbody2D rigidbody;

    //Player Scripts Variables
    private PlayerMovement movement;

    //Player Stats Variables
    public float healthPoints = 100;
    public float souls = 0;

    //Boolean Variables
    private bool invulnerable;
    private bool alive;
    
    /*========================== Health Manipulation Methods ==========================*/
    //Decreases the players total health by the given percentage
    public void decreaseHealthByPercentage(float percentageOfDamage){
        this.healthPoints = this.healthPoints*(1-(percentageOfDamage/100));
    }

    //Decreases the players total health by the given points
    public void decreaseHealthByPoint(float points){
        this.healthPoints -= points;
    }

    //Increases the players total health by the given percentage
    public void increaseHealthByPercentage(float percentageOfDamage){
        this.healthPoints = this.healthPoints*(1+(percentageOfDamage/100));
    }

    //Increases the players total health by the given points
    public void increaseHealthByPoint(float points){
        this.healthPoints += points;
    }

    /*========================== Invulnerability Methods ==========================*/
    //Returns true if the player is still alive
    public bool isPlayerAlive()
    {
        if (this.healthPoints > 0){
            return true;
        }
        return false;
    }

    //Returns true if the player is invulnerable
    public bool isInvulnerable(){
        return this.invulnerable;
    }

    /*========================== Special Unity Methods  ==========================*/
    // Start is called before the first frame update
    void Start()
    {
        this.alive = true;
        this.invulnerable = false;
        this.movement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
