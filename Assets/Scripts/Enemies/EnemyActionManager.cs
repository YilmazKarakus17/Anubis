using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    public static EnemyActionManager instance;
    private Animator animator;
    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public float attackRange;

    // Enemy combat variables
    public float attackDamage;
    private bool isAttacking;
    private bool isCharging;

    // Enemy movement variables (caused by their attacks)
    private float xMagnitude;
    private float yMagnitude;

    // Player Knockback magnitude
    private float playerKnockbackMagnitude;

    /*========================== Enemy Attack Related Functions ==========================*/
    public void ApplyDamage(float movementWaitTime, float attackWaitTime) {
        StartCoroutine(AddMovementDelay(movementWaitTime));
        StartCoroutine(AddAttackDelay(attackWaitTime));
    }

    IEnumerator AddMovementDelay(float waitTime) {
        // A force should not be applied, if the enemy is about to attack but dies whilst charging their attack.
        // The Rigidbody2D component is destroyed, when the enemy's health reaches 0 (to allow the player to traverse through them).
        if (gameObject.GetComponent<Rigidbody2D>() != null) {
            yield return new WaitForSeconds(waitTime);
            if (gameObject.GetComponent<Enemy>().lookingLeft) {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xMagnitude*-1, yMagnitude), ForceMode2D.Impulse);
            }
            else {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xMagnitude, yMagnitude), ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator AddAttackDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        // Enemy hit detection with the sword swings. All of the enemies that intersect with the sword's radius will take damage.
        Collider2D[] player = Physics2D.OverlapCircleAll(attackCentre.position, attackRange, LayerMask.GetMask("Player"));
        for (int i = 0; i < player.Length; i++) {
            player[i].GetComponent<Player>().decreaseHealthByPoint(this.attackDamage);
            // If the enemy is left of the player, knockback the player to the right and vice versa.
            if (transform.position.x < player[i].transform.position.x) {
                player[i].GetComponent<Player>().Knockback(this.playerKnockbackMagnitude, 0);
            }
            else {
                player[i].GetComponent<Player>().Knockback(this.playerKnockbackMagnitude*-1, 0);
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

    public void playAnimation(string animation) {
        this.animator.Play(animation);
    }

    public void playDeathAnimation() {
        if (gameObject.tag == "Minotaur") {
            playAnimation("MinatourDeath");
        }
        else if (gameObject.tag == "Skeleton") {
            playAnimation("SkeletonDeath");
        }
        StartCoroutine(AddDeathDelay(2f));
    }

    IEnumerator AddDeathDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        determineEnemy();
    }

    void Awake() {
        instance = this;
    }

    void determineEnemy() {
        if (gameObject.name == "Minotaur") {
            xMagnitude = 3500f;
            yMagnitude = 1500f;
            playerKnockbackMagnitude = 20f;
        }
        else if (gameObject.name == "Skeleton") {
            xMagnitude = 1500;
            yMagnitude = 0;
            playerKnockbackMagnitude = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackCentre.position, attackRange);
    }
}
