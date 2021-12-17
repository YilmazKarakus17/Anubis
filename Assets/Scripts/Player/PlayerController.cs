using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    public Player player;
    private Rigidbody2D rigidbody;

    //Horizontal Movement Variables
    private bool allowed_to_horizontally_move = true;
    private float horiztonal_movement_input;
    public float horizontal_movement_speed = 7;
    public float in_combat_horizontal_movement_speed = 2;
    public bool in_combat;

    //Player Jump Variables
    private bool jump_input;
    public float jumpForce = 15;
    private bool isGrounded;
    public int extra_jumps_allowed = 2;
    private int extra_jumps_remaining;
    
    //Variables for checking if Player's feet is touching a ground object
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    

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
            this.jump_input = true;
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

    /*========================== Auxiliary Methods   ==========================*/
    //Checks if the player is allowed to perform a jump
    public bool isAllowedToJump(){
        if ((this.isGrounded && this.jump_input) || (this.jump_input && this.extra_jumps_remaining > 0)){
            return true;
        }

        return false;
    }

    void flip() {
        // Flip the player's model, so that the player is facing in the correct direction.
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    /*========================== Special Unity Methods  ==========================*/
    void Start(){
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.player = gameObject.GetComponent<Player>();
        this.extra_jumps_remaining = this.extra_jumps_allowed;
    }

    void FixedUpdate(){
        if (this.getAllowedToHorizontallyMove()){
            this.move();  
        }

        //isGrounded is only set to true if the feetPost collides with a ground object
        this.isGrounded = Physics2D.OverlapCircle(this.feetPos.position, this.checkRadius, this.whatIsGround);
        if (this.isGrounded){
            this.extra_jumps_remaining = this.extra_jumps_allowed;
        }

        if (this.isAllowedToJump()){
            this.jump();
        }

        this.jump_input = false;
    }
}
