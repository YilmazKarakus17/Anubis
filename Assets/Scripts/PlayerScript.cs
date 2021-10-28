using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Variables containing Objects
    private Rigidbody2D r2d;
    Animator animator;

    // Player variables (set to public, so we can change the values in the unity editor)
    public float speed = 30;
    public float jumpMagnitude = 17;
    public float dashBoost = 5;
    public float slideBoost = 5;

    // Private fields
    private float horizontalValue;

    //Booleans
    private bool isGrounded;
    private bool performJump;
    private bool performDash;
    private bool performSlide;
    private bool playerIsDead = false;

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

    // Start is called before the first frame update
    void Start() {
        r2d = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

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
        if (value.performed && !performDash) {
            performDash = true;
        }
    }

    public void Slide(InputAction.CallbackContext value) {
        // The player can only slide, whilst touching the ground.
        if (value.performed && performSlide==false && isGrounded) {
            performSlide = true;
        }
    } 

    void OnCollisionEnter2D(Collision2D otherCollider){
        if (otherCollider.gameObject.tag == "Floor"){
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "SpikeTrap")
        {
            setPlayerIsDeadTrue();
            timeOfDeath = 0;
        }
    }

    //sets the playerIsDead instance variable to true
    public void setPlayerIsDeadTrue(){
        playerIsDead = true;
    }

    //Returns true if player is dead
    public bool isPlayerDead(){
        return playerIsDead;
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

    //Method called when player dies
    void playerDeath(){
        changeAnimationState(PLAYER_DEATH);
        timeOfDeath += Time.deltaTime;
        if (timeOfDeath > deathAnimationWaitTime){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
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
                r2d.AddForce(new Vector2(0, jumpMagnitude), ForceMode2D.Impulse);
                isGrounded = false;
                performJump = false;
                changeAnimationState(PLAYER_JUMP);

            }

            // Applies a dash on the player object and changes the animation to be that of dashing
            if (performDash) {
                r2d.AddForce(new Vector2((horizontalValue)*dashBoost,0f), ForceMode2D.Impulse);
                performDash = false;
                //changeAnimationState(PLAYER_DASH);

                if (!isGrounded){
                    changeAnimationState(PLAYER_JUMP_DASH);
                }
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
