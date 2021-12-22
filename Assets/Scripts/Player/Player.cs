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
    public float currentHealth;
    public float maxStamina;
    public float currentStamina;
    public float staminaRegenTime;
    private float staminaRegenCountDown;
    public float souls;
    

    //Boolean Variables
    private bool invulnerable;
    private bool alive;
    public bool disregardStamina = false;
    public bool isHurt;

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

    /*============ Increase player health Methods ============*/
    //Resets the health
    public void resetHealth(){ 
        this.currentHealth = this.maxHealth;
        this.updateHealthBar();
        this.updateAliveStatus();
    }

    //Increases the players health by the given health points
    public void increaseHealthByPoint(float healthPoints){ 
        float hp = this.currentHealth + healthPoints;
        if (hp > this.maxHealth){
            this.currentHealth = this.maxHealth;
        }
        else{
            this.currentHealth = hp;
        }
        this.updateHealthBar();
        this.updateAliveStatus();
    }

    /*============ Decrease player health Methods ============*/
    //Decreases the players health by the given health points if they are not invulnerable
    public void decreaseHealthByPoint(float healthPoints){ 
        if (!this.isInvulnerable()){
            float hp = this.currentHealth - healthPoints;
            if (hp <= 0){
                this.currentHealth = 0;
            }
            else{
                this.currentHealth = hp;
            }
            this.updateHealthBar(); // updating the health bar health
            this.updateAliveStatus();
        } 
    }

    //Decreases the players health by the given health points regardless of invulnerability
    public void decreaseHealthByPointFORCE(float healthPoints){ 
        float hp = this.currentHealth - healthPoints;
        if (hp <= 0){
            this.currentHealth = 0;
        }
        else{
            this.currentHealth = hp;
        }
        this.updateHealthBar(); // updating the health bar health
        this.updateAliveStatus();
    }

    //===== Kill player =====//
    //Kils the Player if they are not invulnerable
    public void killPlayer(){ 
        if (!this.isInvulnerable()){ 
            this.currentHealth = 0;
            this.updateHealthBar(); // updating the health bar health
            this.updateAliveStatus();
        } 
    }

    //Kils the Player regardless of invulnerability
    public void killPlayerFORCE(){
        this.currentHealth = 0;
        this.updateHealthBar(); // updating the health bar health
        this.updateAliveStatus();
        this.updateAliveStatus();
    }

    /*============ Stamina Manipulation Methods ============*/
    //===== Increase player stamina =====//
    //Resets the staming value
    public void resetStamina(){ 
        this.currentStamina = this.maxStamina; 
        this.updateStaminaBar();
    }

    //Increases the players stamina by the given stamina points
    public void increaseStaminaByPoint(float staminaPoints){ 
        float st = this.currentStamina + staminaPoints; 
        if (st <= this.maxStamina){
            this.currentStamina = st;
        }
        else{
            this.resetStamina();
        }
    }

    //===== Decrease  the players stamina =====//
    /*  Decreases the players stamina by the given stamina points if stamina is not disregarded
        Returns true if the points is not more than the current stamina points and decreases the current points by the given stamina points
        Returns false if the points exceed the current stamina points, and decreases the current points by 0
    */
    public bool decreaseStaminaByPoint(float staminaPoints){
        //Checks that the staminaPoints isn't larger than the currentStamina because we can't have negative stamina
        if (this.currentStamina >= staminaPoints){
            if (!this.disregardStamina){ 
                this.takeStamina(this.currentStamina - staminaPoints); 
            }
            return true;
        }
        return false;
    }


    /*  Decreases the players stamina by the given stamina points regardless of if the stamina is not disregarded.
        Returns true if the points is not more than the current stamina points and decreases the current points by the given stamina points
        Returns false if the points exceed the current stamina points, and decreases the current points by 0
    */
    public bool decreaseStaminaByPointFORCE(float staminaPoints){
        if (this.currentStamina > staminaPoints){
            this.takeStamina(this.currentStamina - staminaPoints); 
            return true;
        }
        return false;
    }

    public void takeStamina(float st){
        if (st <=0){
            this.currentStamina = 0;
        }
        else{
            this.currentStamina = st;
        }
        this.updateStaminaBar();
    }

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


    /*========================== User Interface Methods ==========================*/
    public void updateHealthBar(){
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(currentHealth); 
    }

    public void updateStaminaBar(){
        staminaBar.setMaxStamina(maxStamina);
        staminaBar.setStamina(this.currentStamina);
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
        this.staminaRegenCountDown = this.staminaRegenTime;

        // setting the health bar
        healthBar.setMaxHealth(maxHealth);
        staminaBar.setMaxStamina(maxStamina);
    }

    void Update(){
        this.staminaRegenCountDown -= Time.deltaTime;
        if (this.staminaRegenCountDown <=0){
            this.increaseStaminaByPoint(3);
            this.updateStaminaBar();
            this.staminaRegenCountDown = this.staminaRegenTime;
        }
    }
}