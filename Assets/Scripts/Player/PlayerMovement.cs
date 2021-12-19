using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    private Player player;
    private Animator animator;
    private Rigidbody2D rigidbody;

    //Particle Effects
    public GameObject dashEffect;

    //Horizontal Movement Variables
    private bool allowed_to_horizontally_move = true;
    private float horiztonal_movement_input;
    public float horizontal_movement_speed = 7;
    public float in_combat_horizontal_movement_speed = 2;
    public bool in_combat;

    //Player Jump Variables
    private bool jumpInput;
    public float jumpForce = 15;
    private bool isGrounded;
    public int extra_jumps_allowed = 2;
    private int extra_jumps_remaining;
    
    //Variables for checking if Player's feet is touching a ground object
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Player Dash Variables
    private bool dashInput;
    public float dashSpeed;
    private float dashTimeCounter; //How long the dash should last
    public float dashTime; //Used to as a count down timer

    /*========================== Getter and Setter Methods ==========================*/
    public void setInCombatStatus(bool in_combat){
        this.in_combat = in_combat;
    }

    public void setInCombatHorizontalSpeed(float speed){
        this.in_combat_horizontal_movement_speed = speed;
    }

    public void setAllowedToHorizontallyMove(bool allowed){
        this.allowed_to_horizontally_move = allowed;
    }

    public float getHorizontalMovementInput() {
        return this.horiztonal_movement_input;
    }

    public bool getInCombatStatus(){
        return this.in_combat;
    }

    public bool getAllowedToHorizontallyMove(){
        return this.allowed_to_horizontally_move;
    }

    /*========================== User Action Bindings ==========================*/
    public void Move(InputAction.CallbackContext value) {
        this.horiztonal_movement_input = value.ReadValue<float>();
        // The player model looks in the same direction that the player is moving in.
        if ((this.horiztonal_movement_input > 0 && transform.localScale.x < 0) || (this.horiztonal_movement_input < 0 && transform.localScale.x > 0)) {
            if (this.player.isPlayerAlive()){
                this.flip();
            }        
        }
    }

    public void Jump(InputAction.CallbackContext value) {
        if (value.performed){
            this.jumpInput = true;
        }
    }

    public void Dash(InputAction.CallbackContext value) {
        if (value.performed){
            this.dashInput = true;
        }
    }

    /*========================== Player Actions   ==========================*/
    //Applies horiztonal force to the player in the direction inputted by the user
    private void move(){
        this.rigidbody.velocity = new Vector2(
            this.horiztonal_movement_input*this.horizontal_movement_speed,
            this.rigidbody.velocity.y
        );
    }

    //Applies vertical upwards force
    private void jump(){
        this.rigidbody.velocity = Vector2.up*this.jumpForce;
        this.extra_jumps_remaining--;
    }

    //Applies horiztonal force equivalent to a dash in the direction direction inputted by the user
    private void dash(){
        if(this.dashTimeCounter <= 0){
            this.dashTimeCounter = this.dashTime;
            this.rigidbody.velocity = Vector2.zero;
        }
        else{
            Instantiate(this.dashEffect, this.transform.position, Quaternion.identity);
            this.dashTimeCounter -= Time.deltaTime;
            if (this.horiztonal_movement_input == -1){
                this.rigidbody.velocity = Vector2.left * this.dashSpeed;
            }
            else if (this.horiztonal_movement_input == 1){
                this.rigidbody.velocity = Vector2.right * this.dashSpeed;
            }
        }
    }
    /*========================== Player Movement Instance Methods  (Auxiliary Methods) ==========================*/
    //Checks if the player is allowed to perform a jump
    public bool isAllowedToJump(){
        if ((this.isGrounded && this.jumpInput) || (this.jumpInput && this.extra_jumps_remaining >= 0)){
            return true;
        }

        return false;
    }

    //Checks if the player is allowed to perform a dash
    public bool isAllowedToDash(){
        if (this.dashInput){
            return true;
        }
        return false;
    }

    // Flip the player's model, so that the player
    void flip() {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    /*========================== Special Unity Methods  ==========================*/
    void Start(){
        this.animator = GetComponent<Animator>();
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.player = gameObject.GetComponent<Player>();
        this.extra_jumps_remaining = this.extra_jumps_allowed;
        this.dashTimeCounter = this.dashTime; 
    }

    void FixedUpdate(){
        if (this.getAllowedToHorizontallyMove()){
            this.move();  
        }

        //isGrounded is only set to true if the feetPost collides with a ground object
        this.isGrounded = Physics2D.OverlapCircle(this.feetPos.position, this.checkRadius, this.whatIsGround);
        if (this.isGrounded){
            this.animator.Play("PlayerIdle");
            this.extra_jumps_remaining = this.extra_jumps_allowed;
        }

        if (this.isAllowedToJump()){
            this.animator.Play("PlayerJump");
            this.jump();
        }

        if (this.dashInput && this.dashTimeCounter > 0){
            this.dash();
        }


        this.jumpInput = false;
        this.dashInput = false;
    }
}
