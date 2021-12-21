using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    private Rigidbody2D rigidbody;

    //Player Scripts Variables
    private PlayerMovement movement;
    private PlayerActionManager actionManager;
    private PlayerAnimator animator;

    //Player Stats Variables
    public float maxHealth;
    private float currentHealth;
    public float maxStamina;
    private float currentStamina;
    public float souls;

    //Boolean Variables
    private bool invulnerable;
    private bool alive;
    public bool disregardStamina = false;

    // variables used to wait for the animation to finish before destroying player object
    private bool alreadyCalledDeathMethod;
    public float deathAnimationWaitTime;
    private float timeOfDeath;

    // health bar and stamina bar variables
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    /*========================== Instance Methods ==========================*/
    private void updateAliveStatus(){
        if (this.currentHealth > 0){
            this.alive = true;
        }
        else{
            this.alive = false;
            this.playerDeath();
        }
    }
    
    private void playerDeath(){
        if (this.alreadyCalledDeathMethod == false){
            this.timeOfDeath = 0;
            this.animator.playDeathAnimation();
            this.alreadyCalledDeathMethod = true;
        }
        this.timeOfDeath += Time.deltaTime;
        if (this.timeOfDeath > this.deathAnimationWaitTime){
            Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            //Scene restart goes here.
        }
    }

    /*============ Health Manipulation Methods ============*/
    //===== Increase player health =====//
    //Resets the health
    public void resetHealth(){ 
        this.currentHealth = this.maxHealth;
        this.updateAliveStatus();
    }

    //Increases the players health by the given percentage
    public void increaseHealthByPercentage(float percentage){ 
        float hp = this.currentHealth*(1+(percentage/100));
        if (hp > this.maxHealth){
            this.currentHealth = this.maxHealth;
        }
        else{
            this.currentHealth = hp;
        }
        this.updateAliveStatus();
    }

    //Increases the players health by the given health points
    public void increaseHealthByPoint(float healthPoints){ 
        this.currentHealth += healthPoints; 
        this.updateAliveStatus();
    }

    //===== Upgrade Player Health =====//
    //Upgrades/Increases the players max health by the given percentage
    public void updateHealthByPercentage(float percentage){ 
        this.maxHealth = this.maxHealth*(1+(percentage/100));
        //WRITE CODE
    }

    //Upgrades/Increases the players max health by the given health points
    public void upgradeHealthByPoint(float healthPoints){ 
        this.maxHealth += healthPoints;
        //WRITE CODE
    }

    //===== Decrease player health =====//
    //Decreases the players health by the given percentage if they are not invulnerable
    public void decreaseHealthByPercentage(float percentageOfDamage){ 
        if (!this.isInvulnerable()){ 
            this.currentHealth = this.currentHealth*(1-(percentageOfDamage/100)); 
            healthBar.setHealth(currentHealth); // updating the health bar health
            this.updateAliveStatus();
        } 
    }

    //Decreases the players health by the given percentage regardless of invulnerability
    public void decreaseHealthByPercentageFORCE(float percentageOfDamage){ 
        this.currentHealth = this.currentHealth*(1-(percentageOfDamage/100)); 
        healthBar.setHealth(currentHealth); // updating the health bar health
        this.updateAliveStatus();
    }

    //Decreases the players health by the given health points if they are not invulnerable
    public void decreaseHealthByPoint(float healthPoints){ 
        if (!this.isInvulnerable()){ 
            this.currentHealth -= healthPoints;
            this.updateAliveStatus();
        } 
    }

    //Decreases the players health by the given health points regardless of invulnerability
    public void decreaseHealthByPointFORCE(float healthPoints){ 
        this.currentHealth -= healthPoints;
        this.updateAliveStatus();
    }

    //===== Kill player =====//
    //Kils the Player if they are not invulnerable
    public void killPlayer(){ 
        if (!this.isInvulnerable()){ 
            this.decreaseHealthByPercentageFORCE(100);
            this.updateAliveStatus();
        } 
    }

    //Kils the Player regardless of invulnerability
    public void killPlayerFORCE(){
        this.decreaseHealthByPercentageFORCE(100);
        this.alive = false;
        this.updateAliveStatus();
    }

    /*============ Stamina Manipulation Methods ============*/
    //===== Increase player stamina =====//
    //Resets the staming value
    public void resetStamina(){ this.currentStamina = this.maxStamina; }

    //Increases the players stamina by the given percentage
    public void increaseStaminaByPercentage(float percentage){ this.currentStamina = this.currentStamina*(1+(percentage/100)); }

    //Increases the players stamina by the given stamina points
    public void increaseStaminaByPoint(float staminaPoints){ this.currentStamina += staminaPoints; }

    //===== Decrease  the players stamina =====//
    /*  Decreases the players stamina by the given stamina points if stamina is not disregarded
        Returns true if the points is not more than the current stamina points and decreases the current points by the given stamina points
        Returns false if the points exceed the current stamina points, and decreases the current points by 0
    */
    public bool decreaseStaminaByPoint(float staminaPoints){ 
        if (this.currentStamina >= staminaPoints){
            if (!this.disregardStamina){ 
                this.currentStamina -= staminaPoints; 
                staminaBar.setStamina(currentStamina); // sets the stamina of the HUD
            } 
            return true;
        }
        if (!this.disregardStamina){ 
            this.currentStamina = 0;
        } 
        return false;
    }


    /*  Decreases the players stamina by the given stamina points regardless of if the stamina is not disregarded.
        Returns true if the points is not more than the current stamina points and decreases the current points by the given stamina points
        Returns false if the points exceed the current stamina points, and decreases the current points by 0
    */
    public bool decreaseStaminaByPointFORCE(float staminaPoints){
        if (this.currentStamina >= staminaPoints){
            this.currentStamina -= staminaPoints;
            staminaBar.setStamina(currentStamina); // sets the stamina of the HUD
            return true;
        }
        this.currentStamina = 0;
        return false;
    }

    //Decreases the players stamina by the given percentage if stamina is not disregarded
    public void decreaseStaminaByPercentage(float percentage){ if (!this.disregardStamina){ this.currentStamina = this.currentStamina*(1-(percentage/100)); } }
    //Decreases the players stamina by the given percentage regardless of if the stamina is not disregarded
    public void decreaseStaminaByPercentageFORCE(float percentage){ this.currentStamina = this.currentStamina*(1-(percentage/100)); }

    /*========================== Instance Methods ==========================*/
    /*============ Invulnerability Methods ============*/
    //Returns true if the player is still alive
    public bool isPlayerAlive()
    {
        return this.alive;
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
        this.movement = GetComponent<PlayerMovement>();
        this.actionManager = GetComponent<PlayerActionManager>();
        this.animator = gameObject.GetComponent<PlayerAnimator>();

        //Instantiating Player Stats Variables
        //this.maxHealth = SaveManager.instance.playerHealth;
        this.currentHealth = this.maxHealth;
        //this.maxStamina = SaveManager.instance.playerStamina;
        this.currentStamina = this.maxStamina;
        //this.souls = SaveManager.instance.playerSouls;

        //Instantiating 
        this.alive = true;
        this.invulnerable = false;
        this.alreadyCalledDeathMethod = false;
        this.deathAnimationWaitTime = 1f;

        // setting the health bar
        healthBar.setMaxHealth(maxHealth);
        staminaBar.setMaxStamina(maxStamina);
    }

    void FixedUpdate(){
        healthBar.setHealth(maxHealth);
        staminaBar.setStamina(maxStamina);
    }
}