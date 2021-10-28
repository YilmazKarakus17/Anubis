using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D r2d;
    // Player variables
    private float speed = 35;
    private float jumpMagnitude = 15;
    private float dashBoost = 5;
    private float slideBoost = 5;

    // Private fields
    private float horizontalValue;

    //Booleans
    private bool isGrounded;
    private bool performJump;
    private bool performDash;
    private bool performSlide;

    // gets the animator object
    Animator animator;
    // to store the current animation state of the player
    private string currentState;
    // Constants that represent the animations in the game
    const string PLAYER_IDLE = "playerIdle";
    const string PLAYER_RUN = "playerRun";
    const string PLAYER_JUMP = "playerJump";
    const string PLAYER_SLIDE = "playerSlide";
    const string PLAYER_DASH ="playerDash";


    // Start is called before the first frame update
    void Start() {
        r2d = gameObject.GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext value) {
        horizontalValue = value.ReadValue<float>();
        // The player model looks in the same direction that the player is moving in.
        if ((horizontalValue > 0 && transform.localScale.x < 0) || (horizontalValue < 0 && transform.localScale.x > 0)) {
            flip();
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
        if (value.performed && !performSlide && isGrounded) {
            performSlide = true;
        }
    } 

    void OnCollisionEnter2D(Collision2D otherCollider){
        if (otherCollider.gameObject.tag == "Floor"){
            isGrounded = true;
        }
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


    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        // Move the player either left or right.
        if (horizontalValue != 0f) {
            r2d.AddForce(new Vector2(horizontalValue * speed, 0f), ForceMode2D.Force);

            if (isGrounded){
                changeAnimationState(PLAYER_RUN);
            }
        }

        // Jump force is only applied once.
        if (performJump && isGrounded) {
            r2d.AddForce(Vector2.up*jumpMagnitude, ForceMode2D.Impulse);
            isGrounded = false;
            performJump = false;
            changeAnimationState(PLAYER_JUMP);
        }

        // Dash only applied
        if (performDash) {
            r2d.AddForce(new Vector2((horizontalValue)*dashBoost,0f), ForceMode2D.Impulse);
            performDash = false;
        }

        if (horizontalValue==0 && isGrounded){
            changeAnimationState(PLAYER_IDLE);
        }
        
    }
}