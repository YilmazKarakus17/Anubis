using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinatourActionManager : MonoBehaviour
{
    public static MinatourActionManager instance;
    public Animator animator;
    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public float attackRange = 2f;

    // Enemy combat variables
    public float attackDamage = 25;
    public bool isAttacking;
    public bool isCharging;


    /*========================== Enemy Attack Related Functions ==========================*/
    public void ApplyDamage(float pounceWaitTime, float attackWaitTime) {
        StartCoroutine(AddJumpDelay(pounceWaitTime));
        StartCoroutine(AddAttackDelay(attackWaitTime));
    }

    IEnumerator AddJumpDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (gameObject.GetComponent<Enemy>().lookingLeft) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3500, 1500), ForceMode2D.Impulse);
        }
        else {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3500, 1500), ForceMode2D.Impulse);
        }
    }

    IEnumerator AddAttackDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        // Enemy hit detection with the sword swings. All of the enemies that intersect with the sword's radius will take damage.
        Collider2D[] player = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, LayerMask.GetMask("Player"));
        for (int i = 0; i < player.Length; i++) {
            player[i].GetComponent<Player>().decreaseHealthByPoint(attackDamage);
            // If the enemy is left of the player, knockback the player to the right and vice versa.
            if (transform.position.x < player[i].transform.position.x) {
                player[i].GetComponent<Player>().Knockback(20, 5);
            }
            else {
                player[i].GetComponent<Player>().Knockback(-20, 5);
            }
        }
    }

    /*========================== Setter and Getter Functions ==========================*/
    public void setIsAttacking(bool isAttacking) {
        this.isAttacking = isAttacking;
    }

    public void setIsCharging(bool isCharging) {
        this.isCharging = isCharging;
    }

    public bool getIsAttacking() {
        return this.isAttacking;
    }

    public bool getIsCharging() {
        return this.isCharging;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
}
