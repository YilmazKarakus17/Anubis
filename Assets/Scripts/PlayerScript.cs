using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Variables containing Objects
    private Rigidbody2D r2d;
    Animator animator;

    // Player variables (set to public, so we can change the values in the unity editor)
    public float health = 100;
    public float speed = 125;
    public float jumpMagnitude = 40;
    public float jumpPowerUpMagnitude;
    public float dashBoost = 20;
    public float numberOfDashs;
    public float slideBoost = 5;
    public float soulCount = 0;

    // Private fields
    private float horizontalValue;

    //Booleans
    private bool isGrounded;
    private bool performJump;
    private bool allowBoostedJump;
    private bool performDash;
    private bool performSlide;
    private bool playerIsDead;
    private bool invulnerable;

    // to store the current animation state of the player
    private string currentState;

    // Constants that represent the animations in the game
    const string PLAYER_IDLE = "playerIdle";
    const string PLAYER_RUN = "playerRun";
    const string PLAYER_JUMP = "playerJump";
    const string PLAYER_SLIDE = "playerSlide";
    const string PLAYER_DASH ="playerDash";
    const string PLAYER_DEATH = "playerDeath";
    const string PLAYER_JUMP_DASH = "playerJumpDash";

    // Floats used to wait for the animation to finish before destroying player object 
    public float deathAnimationWaitTime = 1f;
    private float timeOfDeath;

    // Floats used to countdown the amount of time the player object is invulnerabile
    public float invulnerabilityCountDownTime;
    private float timeOfInvulnerability;
    
    //UI variables
    public Text hpText;
    public Text dashText;
    public Text soulText;

    //=================================================== User Action Bindings =============================================//
    public void Move(InputAction.CallbackContext value) {
        horizontalValue = value.ReadValue<float>();
        // The player model looks in the same direction that the player is moving in.
        if ((horizontalValue > 0 && transform.localScale.x < 0) || (horizontalValue < 0 && transform.localScale.x > 0)) {
            if (!playerIsDead){
                flip();
            }        
        }
    }

    public void Jump(InputAction.CallbackContext value) {
        // The player can only jump, whilst touching the ground.
        if (value.performed && isGrounded) {
            performJump = true;
        }
    }

    public void Dash(InputAction.CallbackContext value) {
        // The player can only dash if they are not already dashing
        if (value.performed && !performDash && isAllowedDash()) {
            performDash = true;
        }
    }

    public void Slide(InputAction.CallbackContext value) {
        // The player can only slide, whilst touching the ground.
        if (value.performed && performSlide==false && isGrounded) {
            performSlide = true;
        }
    } 

    /*============================================== Collider and Trigger Methods =============================================*/

    void OnCollisionEnter2D(Collision2D otherCollider){
        if (otherCollider.gameObject.tag == "Floor"){
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "SpikeTrap")
        {
            if (!isInvulnerable()){
                setPlayerIsDeadTrue();
                timeOfDeath = 0;
            }
        }
        if (otherCollider.gameObject.tag == "FireTrap")
        {
            minusOneHealth();
            if (checkIfPlayerNeedsToDie() && !isInvulnerable()){
                setPlayerIsDeadTrue();
            }
        }
        if (otherCollider.gameObject.tag == "JumpPowerUps")
        {
            JumpBoost jumpBoost = otherCollider.gameObject.GetComponent<JumpBoost>();
            setJumpPowerUpMagnitude(jumpBoost.getBoostMagnitude());
            Destroy(otherCollider.gameObject);
            allowJumpBoost();
        }
        if (otherCollider.gameObject.tag == "Dash")
        {
            addOneDash();
            Destroy(otherCollider.gameObject);
        }
        if (otherCollider.gameObject.tag == "InvulnerabilityPowerUp")
        {
            Invulnerability invulnerability = otherCollider.gameObject.GetComponent<Invulnerability>();
            startInvulnerabilityTimer(invulnerability.getInvulnerabilityTime());
            allowInvulnerability();
            Destroy(otherCollider.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "FireTrap")
        {
            minusOneHealth();
            if (checkIfPlayerNeedsToDie() && !isInvulnerable()){
                setPlayerIsDeadTrue();
            }
        }
    }

    /*============================================== Getter and Setter Methods =============================================*/
    //sets the playerIsDead instance variable to true
    public void setPlayerIsDeadTrue(){
        playerIsDead = true;
    }

    //removes 1 HP from the player object
    public void minusOneHealth(){
        this.health = this.health - 1;
    }

    //Sets the jumpPowerUpMagnitude to be that of the given value
    public void setJumpPowerUpMagnitude(float magnitude){
        this.jumpPowerUpMagnitude = magnitude;
    }

    //Sets allowBoostedJump variable to true
    public void allowJumpBoost(){
        this.allowBoostedJump = true;
    }

    //Sets in invulnerable variable to true
    public void allowInvulnerability(){
        this.invulnerable = true;
    }

    //Sets in invulnerable variable to false
    public void disallowInvulnerability(){
        this.invulnerable = false;
    }

    //Sets allowBoostedJump variable to false
    public void disallowJumpBoost(){
        this.allowBoostedJump = false;
    }

    //Adds 1 to the overall number of dashses
    public void addOneDash(){
        this.numberOfDashs += 1;
    }

    //removes 1 from the overall number of dashses
    public void removeOneDash(){
        this.numberOfDashs -= 1;
    }

    //Returns the value of player health
    public float getHealth(){
        return this.health;
    }

    //Returns the value of the amount of dashes a player currently has
    public float getDashCount(){
        return this.numberOfDashs;
    }

    //Returns the value of the amount of souls ashes a player currently has
    public float getSoulCount(){
        return this.soulCount;
    }

    //Returns true if player is dead
    public bool isPlayerDead(){
        return playerIsDead;
    }

    //Returns true if player is allowed to perform a boosted jump
    public bool isAllowedBoostedJump(){
        return this.allowBoostedJump;
    }
    
    //Returns true if player is invulnerable
    public bool isInvulnerable(){
        return this.invulnerable;
    }

    //Returns true if player is allowed to dash
    public bool isAllowedDash(){
        if (this.numberOfDashs > 0){
            return true;
        }
        return false;
    }

    //Returns true if the player health drops to 0 or below
    public bool checkIfPlayerNeedsToDie(){
        if (this.health <= 0){
            return true;
        }
        return false;
    }

    /*==================================================== Auxiliary Methods =============================================*/

    //starts a count down for the time the player is allowed to be invulnerable
    public void startInvulnerabilityTimer(float timeForInvulnerability){
        this.invulnerabilityCountDownTime = timeForInvulnerability;
        this.timeOfInvulnerability = 0f;
    }

    void flip() {
        // Flip the player's model, so that the player is facing in the correct direction.
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    // changes the animation
    void changeAnimationState(string newState){
        // return if there's no state change
        if (currentState==newState) return;

        // play the animation
        animator.Play(newState);

        // update the current state
        currentState = newState;
    }

    /*==================================================== User Interface Methods =============================================*/
    void updateTexts(){
        updateHPText();
        updateDashText();
        updateSoulsText();
    }

    private void updateHPText()
    {
        this.hpText.text = "HP: " + getHealth().ToString();
    }

    private void updateDashText()
    {
        this.dashText.text = "Dash Count: " + getDashCount().ToString();
    }

    private void updateSoulsText()
    {
        this.soulText.text = "Souls: " + getSoulCount().ToString();
    }

    /*==================================================== Player Actions ====================================================*/
    //Applies necessary forces and animations to make the player object perform a jump action
    void playerJump(){
        float magnitude = this.jumpMagnitude;
        if (isAllowedBoostedJump()){
            magnitude = magnitude + this.jumpPowerUpMagnitude;
            disallowJumpBoost();
        }
        r2d.AddForce(new Vector2(0, magnitude), ForceMode2D.Impulse);
        isGrounded = false;
        performJump = false;
        changeAnimationState(PLAYER_JUMP);
    }

    //Applies necessary forces and animations to make the player object perform a dash action
    void playerDash(){
        r2d.AddForce(new Vector2((horizontalValue)*dashBoost,0f), ForceMode2D.Impulse);
        performDash = false;
        removeOneDash();
        //changeAnimationState(PLAYER_DASH);

        if (!isGrounded && horizontalValue!=0f){
            changeAnimationState(PLAYER_JUMP_DASH);
        }
    }

    //Applies necessary forces and animations to make the player object to die
    void playerDeath(){
        changeAnimationState(PLAYER_DEATH);
        timeOfDeath += Time.deltaTime;
        if (timeOfDeath > deathAnimationWaitTime){
            Destroy(gameObject);
        }
    }

    //Checks if player is still allowed to be invulerable and if it's not it calls a method to disallow invulerability
    void checkInvulnerabilityIsAllowed(){
        timeOfInvulnerability += Time.deltaTime;
        if (timeOfInvulnerability >= invulnerabilityCountDownTime){
            disallowInvulnerability();
        }
    }

    /*==================================================== Special Unity Methods ====================================================*/
    // Start is called before the first frame update
    void Start() {
        r2d = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.numberOfDashs = 0;
        this.playerIsDead = false;
        this.invulnerable = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        checkInvulnerabilityIsAllowed();
        updateTexts();

        if (playerIsDead){
            playerDeath();
        }
        else{
            // Move the player either left or right.
            if (horizontalValue != 0f) {
                // The player is unable to go beyond the given maximum speed.
                Vector2 movement = new Vector2(horizontalValue, 0);
                r2d.velocity = Vector2.ClampMagnitude(r2d.velocity, speed);
                r2d.AddForce(movement * speed, ForceMode2D.Force);
                if (isGrounded) {
                    changeAnimationState(PLAYER_RUN);
                }
            }

            // Jump force is only applied once.
            if (performJump) {
                playerJump();
            }

            // Applies a dash on the player object and changes the animation to be that of dashing
            if (performDash) {
                playerDash();
            }

            // Applies a slide on the player object and changes the animation to be that of sliding
            if (performSlide) {
                //r2d.AddForce(new Vector2((horizontalValue)*slideBoost,0f), ForceMode2D.Impulse);
                //changeAnimationState(PLAYER_SLIDE);
                performSlide = false;
            }

            if (horizontalValue==0 && isGrounded){
                changeAnimationState(PLAYER_IDLE);
            }
        }
    }
}
