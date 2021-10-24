using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    private Rigidbody2D r2d;
    // Horizontal movement fields.
    public float walkSpeed;
    public float dashSpeed;
    private float horizontalValue;
    private float lastPressedTime;
    private bool dashing;
    private bool walking;
    private float lastDashTime;
    // Vertical movement fields (jumping).
    private float verticalValue;
    public float jumpMagnitude;
    private bool isJumping;
    private bool isDoubleJumping;
    private bool apply;
    private float lastJumpTime;
    // Player orientation fields.
    private bool facingRight;
    
    void Start() {
        r2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext value) {
        if (value.performed) {
            // timeSinceLastPress shows the time that the button was last pressed; used to check whether the player has commanded a dash.
            // timeSinceLastDash shows the time that the previous dash was commanded; used to prevent the player from repeatedly dashing.
            float timeSinceLastPress = Time.time - lastPressedTime;
            float timeSinceLastDash = Time.time - lastDashTime;
            if (timeSinceLastPress < 0.2f && timeSinceLastDash > 1f && !dashing) {
                dashing = true;
                lastDashTime = Time.time;
            }
            else {
                walking = true;
            }
            horizontalValue = value.ReadValue<Vector2>().x;
            lastPressedTime = Time.time;   
        }
        else {
            walking = false;
        }
    }

    public void Jump(InputAction.CallbackContext value) {
        if (value.performed) {
            // Single jump - the player can jump, as long as they are touching the ground.
            float timeSinceLastJump = Time.time - lastJumpTime;
            if (!isJumping) {
                verticalValue = 1f;
                isJumping = true;
                apply = true;
                lastJumpTime = Time.time;
            }
            // Double jump - the player can double jump, as long as they have already jumped once.
            else if (isJumping && !isDoubleJumping && timeSinceLastJump > 0.15f) {
                verticalValue = 2f;
                isDoubleJumping = true;
                apply = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // NOTE: can use .tag instead of .name, but make sure that the object to be collided with has the appropriate tag.
        if (collision.gameObject.name == "Ground") {
            isJumping = false;
            isDoubleJumping = false;
        }
    }

    void flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Update() {

    }

    void FixedUpdate() {
        // Move the player either left or right.
        if ((horizontalValue > 0f || horizontalValue < 0f) && walking) {
            r2d.AddForce(new Vector2(horizontalValue * walkSpeed, 0f), ForceMode2D.Impulse);
        }
        else if ((horizontalValue > 0f || horizontalValue < 0f) && dashing) {
            r2d.AddForce(new Vector2(horizontalValue * dashSpeed, 0f), ForceMode2D.Impulse);
            dashing = false;
            walking = true;
        }
        // The players model's horizontal orientation (x-value) flip, to ensure that the player is facing in the direction that the player moves in.
        if (horizontalValue > 0f && !facingRight) {
            //flip();
        }
        else if (horizontalValue < 0f || facingRight) {
            //flip();
        }
        // Move the player vertically via jumps (either single jump or double jump), the force is only applied once per jump.
        if (verticalValue > 0 && isJumping && apply) {
            r2d.AddForce(new Vector2(0f, jumpMagnitude), ForceMode2D.Impulse);
            apply = false;
        }
    }
}
