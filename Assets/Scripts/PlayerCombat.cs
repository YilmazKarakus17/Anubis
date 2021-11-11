using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerScript player;
    private Rigidbody2D r2d;
    // Combat variables
    public float attackDamage = 50;
    private float attackRate = 0.25f;
    private float nextAttack = 0;

    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public float attackRange = 7.5f;
    private Collider2D[] enemiesHit;
    public LayerMask enemyLayers;

    // Knockback fields.
    private bool knockback;
    private float knockbackDirection;

    // Normal attack performed by pressing the attack button once.
    public void Attack(InputAction.CallbackContext value) {
        if (value.performed) {
            // Player has to wait for the animation finish, until it can attack again.
            if (Time.time > nextAttack) {
                nextAttack = Time.time + attackRate;
                player.isAttacking = true;
                // Run the attack animation and detect all enemies that intersect with the attack hit-circle. Then apply damage to all enemies that were hit.
                enemiesHit = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLayers);
                for (int i = 0; i < enemiesHit.Length; i++) {
                    enemiesHit[i].GetComponent<RangedEnemy>().TakeDamage(attackDamage);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D otherCollider){
        // Layer 6 is the enemy layer.
        if (otherCollider.gameObject.layer == 6){
            ReceiveDamage(otherCollider.gameObject.GetComponent<RangedEnemy>().attackDamage);
            // The player will be knockback away from the enemy. -1 = left, 1 = right.
            if (gameObject.transform.position.x < otherCollider.gameObject.transform.position.x) {
                knockbackDirection = -1;
            }
            else {
                knockbackDirection = 1;
            }
        }
    }

    public void ReceiveDamage(float damage) {
        player.minusHealth(damage);
        knockback = true;
        // If the players health drops to 0, the player will die.
        if (player.checkIfPlayerNeedsToDie()) {
            if (player.checkIfPlayerNeedsToDie() && !player.isInvulnerable()) {
                player.setPlayerIsDeadTrue();
            }   
        }
    }

    // A chain attack occurs when the attack button is pressed again subsequently right after the normal attack.
    // public void ChainAttack(InputAction.CallbackContext value) {
    //     if (value.performed) {
    //         Debug.Log("Attack 2 Performed!");
    //         // Run the attack animation and detect all enemies that intersect with the attack hit-circle. Then apply damage to all enemies that were hit.
    //         enemiesHit = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLayers);
    //         for (int i = 0; i < enemiesHit.Length; i++) {
    //             enemiesHit[i].GetComponent<Enemy>().TakeDamage(attackDamage);
    //         }
    //     }
    // }

    // A third attack variation occurs when the attack button is pressed again subsequently right after first chain attack.
    // public void LastChainAttack(InputAction.CallbackContext value) {
    //     if (value.performed) {
    //         Debug.Log("Attack 3 Performed!");
    //         // Run the attack animation and detect all enemies that intersect with the attack hit-circle. Then apply damage to all enemies that were hit.
    //         enemiesHit = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, enemyLayers);
    //         for (int i = 0; i < enemiesHit.Length; i++) {
    //             enemiesHit[i].GetComponent<Enemy>().TakeDamage(attackDamage);
    //         }
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerScript>();
        r2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (knockback) {
            // If the player is to the left of the enemy, the player gets knockback to the left.
            // If the player is to the right of the enemy, the player gets knockback to the right.
            r2d.AddForce(new Vector2(knockbackDirection*30, 0), ForceMode2D.Impulse);
            knockback = false;
        }
    }
}