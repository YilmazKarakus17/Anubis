using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    public static PlayerMovement instance; // Used in IdleBehaviour to check if player is in combat.
    private Player player;
    private PlayerAnimator animator;
    private Rigidbody2D rigidbody;

    //Particle Effect Variables
    public GameObject dashEffect;

    //Horizontal Movement Variables
    private bool allowed_to_horizontally_move = true;
    private float horizontal_movement_input;
    public float horizontal_movement_speed = 7;
    public float in_combat_horizontal_movement_speed = 2;
    public bool inCombat;

    //Player Jump Variables
    private bool jumpInput;
    public float jumpForce = 15;
    private bool isGrounded;
    public int extra_jumps_allowed = 2;
    private int extra_jumps_remaining;
    private float prevY;
    private float crntY;
    
    //Variables for checking if Player's feet is touching a ground object
    public Transform feetPos;
    public float feetCheckRadius;
    public LayerMask whatIsGround;

    //Variables for checking if Player's hand is touching a WallJump object
    private bool isHanging;
    public Transform handPos;
    public float handCheckRadius;
    public LayerMask whatIsWallJump;

    //Player Dash Variables
    private bool dashInput;
    public float dashSpeed;
    private float dashTimeCounter; //How long the dash should last
    public float dashTime; //Used to as a count down timer

    /*========================== Getter and Setter Methods ==========================*/
    public void setInCombatStatus(bool inCombat){
        this.inCombat = inCombat;
    }

    public void setInCombatHorizontalSpeed(float speed){
        this.in_combat_horizontal_movement_speed = speed;
    }

    public void setAllowedToHorizontallyMove(bool allowed){
        this.allowed_to_horizontally_move = allowed;
    }

    public float getHorizontalMovementInput() {
        return this.horizontal_movement_input;
    }

    public bool getInCombatStatus(){
        return this.inCombat;
    }

    public bool getAllowedToHorizontallyMove(){
        return this.allowed_to_horizontally_move;
    }

    /*========================== User Action Bindings ==========================*/
    public void Move(InputAction.CallbackContext value) {
        this.horizontal_movement_input = value.ReadValue<float>();
        // The player model looks in the same direction that the player is moving in.
        if ((this.horizontal_movement_input > 0 && transform.localScale.x < 0) || (this.horizontal_movement_input < 0 && transform.localScale.x > 0)) {
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
            this.player.decreaseStaminaByPoint(5); // BILAL CHANGED HERE!
        }
    }

    /*========================== Player Actions   ==========================*/
    //Applies horiztonal force to the player in the direction inputted by the user
    private void move(){
        this.rigidbody.velocity = new Vector2(
            this.horizontal_movement_input*this.horizontal_movement_speed,
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
            if (this.horizontal_movement_input == -1){
                this.rigidbody.velocity = Vector2.left * this.dashSpeed;
            }
            else if (this.horizontal_movement_input == 1){
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
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.player = gameObject.GetComponent<Player>();
        this.animator = gameObject.GetComponent<PlayerAnimator>();
        this.extra_jumps_remaining = this.extra_jumps_allowed;
        this.dashTimeCounter = this.dashTime;
        this.inCombat = false;
        this.prevY = this.crntY = this.transform.position.y;
    }

    void Awake() {
        instance = this;
    }

    void FixedUpdate(){
        this.crntY = this.transform.position.y;

        if (player.isPlayerAlive() && !player.isKockedBack()){
            //Regardless of whether the player is grounded or in the air, they must be allowed to move
            if (this.horizontal_movement_input != 0 && this.getAllowedToHorizontallyMove()){
                this.move();
            }

            //isGrounded is only set to true if the feetPost collides with a ground object
            this.isGrounded = Physics2D.OverlapCircle(this.feetPos.position, this.feetCheckRadius, this.whatIsGround);

            //isHanging is only set to true if the feetPost collides with a ground object
            this.isHanging = Physics2D.OverlapCircle(this.handPos.position, this.handCheckRadius, this.whatIsWallJump);

            //If the player is NOT touching the ground they must either be hanging, or falling
            if (!this.isGrounded){
                if (this.isHanging){
                    this.animator.playHangAnimation();
                }
                else if ((this.crntY - this.prevY) < 0 && !this.inCombat){
                    this.animator.playFallAnimation();
                }
            }
            else {
                //if the player is grounded and doesn't wish to horizontally and is not in combat they must be IDLE
                if (this.horizontal_movement_input==0 && !this.inCombat){
                    this.animator.playIdleAnimation();
                }
                //If the payer is grounded and is allowed to move horizontally and wish to move horizontally then they must be RUNNING 
                else if (this.horizontal_movement_input != 0 && this.getAllowedToHorizontallyMove()){
                    if (this.isGrounded && !this.inCombat) {
                        this.animator.playRunAnimation();
                    }
                }
            }
            
            if (this.isGrounded || this.isHanging){
                this.extra_jumps_remaining = this.extra_jumps_allowed;
            }

            if (this.isAllowedToJump() && !this.inCombat){
                this.animator.playJumpAnimation();
                this.jump();
            }

            if (this.dashInput && this.dashTimeCounter > 0){
                this.dash();
            }
        }

        this.jumpInput = false;
        this.dashInput = false;
        this.prevY = this.crntY;
    }
}
