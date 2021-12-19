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
    public float maxHealth = 100;
    private float currentHealth;
    public float maxStamina = 50;
    private float currentStamina;
    public float souls = 0;

    //Boolean Variables
    private bool invulnerable;
    private bool alive;
    public bool disregardStamina;

    /*========================== Instance Methods ==========================*/
    
    /*============ Health Manipulation Methods ============*/
    //Resets the health
    public void resetHealth(){
        this.currentHealth = this.maxHealth;
    }

    //Increases the players health by the given percentage
    public void increaseHealthByPercentage(float percentageOfDamage){
        this.currentHealth = this.currentHealth*(1+(percentageOfDamage/100));
    }

    //Decreases the players health by the given percentage
    public void decreaseHealthByPercentage(float percentageOfDamage){
        this.currentHealth = this.currentHealth*(1-(percentageOfDamage/100));
    }

    //Increases the players health by the given health points
    public void increaseHealthByPoint(float healthPoints){
        this.currentHealth += healthPoints;
    }

    //Decreases the players health by the given health points
    public void decreaseHealthByPoint(float healthPoints){
        this.currentHealth -= healthPoints;
    }

    /*============ Stamina Manipulation Methods ============*/
    //Resets the staming value
    public void resetStamina(){
        this.currentStamina = this.maxStamina;
    }

    //Decreases the players stamina by the given percentage
    public void decreaseStaminaByPercentage(float percentage){
        this.currentStamina = this.currentStamina*(1-(percentage/100));
    }

    //Increases the players stamina by the given percentage
    public void increaseStaminaByPercentage(float percentage){
        this.currentStamina = this.currentStamina*(1+(percentage/100));
    }

    //Increases the players stamina by the given stamina points
    public void increaseStaminaByPoint(float staminaPoints){
        this.currentStamina += staminaPoints;
    }

    //Decreases the players stamina by the given stamina points
    public void decreaseStaminaByPoint(float staminaPoints){
        this.currentStamina -= staminaPoints;
    }

    /*========================== Invulnerability Methods ==========================*/
    //Returns true if the player is still alive
    public bool isPlayerAlive()
    {
        if (this.currentHealth > 0){
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
        //Instantiating Player Scripts Variables
        this.movement = gameObject.GetComponent<PlayerMovement>();

        //Instantiating Player Stats Variables
        this.currentHealth = this.maxHealth;
        this.currentStamina = this.maxStamina;

        //Instantiating 
        this.alive = true;
        this.invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}