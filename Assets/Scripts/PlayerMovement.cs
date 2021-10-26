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

    // Private fields
    private float horizontalValue;

    //Booleans
    private bool isJumping;
    private bool allowDashing;
    private bool applyJump;

    // Start is called before the first frame update
    void Start() {
        r2d = gameObject.GetComponent<Rigidbody2D>();
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
        if (value.performed && !isJumping) {
            isJumping = true;
            applyJump = true;
        }
    }

    public void Dash(InputAction.CallbackContext value) {
        // The player can only jump, whilst touching the ground.
        if (value.performed && !allowDashing) {
            allowDashing = true;
        }
    }

    void OnCollisionEnter2D(Collision2D otherCollider){
        if (otherCollider.gameObject.tag == "Floor"){
            isJumping = false;
        }
    }

    void flip() {
        // Flip the player's model, so that the player is facing in the correct direction.
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        // Move the player either left or right.
        if (horizontalValue != 0f) {
            r2d.AddForce(new Vector2(horizontalValue * speed, 0f), ForceMode2D.Force);
        }

        // Jump force is only applied once.
        if (isJumping && applyJump) {
            r2d.AddForce(Vector2.up*jumpMagnitude, ForceMode2D.Impulse);
            applyJump = false;
        }

        // Dash only applied
        if (allowDashing) {
            r2d.AddForce(new Vector2((horizontalValue)*dashBoost,0f), ForceMode2D.Impulse);
            allowDashing = false;
        }
        
    }
}