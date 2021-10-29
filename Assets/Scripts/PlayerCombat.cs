using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // Combat variables
    public float attackDamage = 50;

    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public float attackRange = 7.5f;
    private Collider2D[] enemiesHit;
    public LayerMask enemyLayers;

    // Booleans
    private bool performAttack;

    // gets the animator object
    Animator animator;
    // to store the current animation state of the player
    private string currentState;
    // Constants that represent the animations in the game
    const string PLAYER_ATTACK1 = "playerAttack1";

    public void Attack(InputAction.CallbackContext value) {
        if (value.performed) {
            performAttack = true;
            // Run the attack animation and detect all enemies that intersect with the attack hit-circle. Then apply damage to all enemies that were hit.
            enemiesHit = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLayers);
            for (int i = 0; i < enemiesHit.Length; i++) {
                enemiesHit[i].GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    // Changes the animation
    void changeAnimationState(string newState){
        // Return if there's no state change
        if (currentState==newState) return;
        // Play the animation
        animator.Play(newState);
        // Update the current state
        currentState = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (performAttack) {
            changeAnimationState(PLAYER_ATTACK1);
        }
    }
}