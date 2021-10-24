using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D r2d;
    // Player variables
    public float walkSpeed;
    public float jumpMagnitude;
    // Private fields
    private float horizontalValue;
    private bool isJumping;
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

    void OnTriggerEnter2D(Collider2D collision) {
        // When the player finally touches the ground, the player can jump once again.
        if (collision.gameObject.name == "Ground") {
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
        if (horizontalValue > 0f || horizontalValue < 0f) {
            r2d.AddForce(new Vector2(horizontalValue * walkSpeed, 0f), ForceMode2D.Force);
        }
        // Jump force is only applied once.
        if (isJumping && applyJump) {
            r2d.AddForce(new Vector2(0f, jumpMagnitude), ForceMode2D.Impulse);
            applyJump = false;
        }
    }
}
