using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionManager : MonoBehaviour
{
    public static EnemyActionManager instance;
    private Player player;
    private Animator animator;
    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public GameObject soul;
    public GameObject attackEffect;
    public int numberOfSouls = 1;
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
        yield return new WaitForSeconds(waitTime);
        // A force should not be applied, if the enemy is about to attack but dies whilst charging their attack.
        // The Rigidbody2D component is destroyed, when the enemy's health reaches 0 (to allow the player to traverse through them).
        if (gameObject.GetComponent<Rigidbody2D>() != null) {
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
            // Because there is only one player
            if (i < 1) {
                player[i].GetComponent<Player>().decreaseHealthByPoint(this.attackDamage);
                // If the enemy is left of the player, knockback the player to the right and vice versa.
                if (transform.position.x < player[i].transform.position.x) {
                    Instantiate(this.attackEffect, this.transform.position, Quaternion.Euler(0,0,180));
                    player[i].GetComponent<Player>().Knockback(this.playerKnockbackMagnitude, 0);
                }
                else {
                    Instantiate(this.attackEffect, this.transform.position, Quaternion.identity);
                    player[i].GetComponent<Player>().Knockback(this.playerKnockbackMagnitude*-1, 0);
                }
            }
        }
        if (gameObject.tag == "Fire Worm") {
            RangedEnemy rangedEnemy = gameObject.GetComponent<RangedEnemy>();
            GameObject fireball;
            if (rangedEnemy.getHorizontalDirection() > 0) {
                fireball = Instantiate(rangedEnemy.projectile, gameObject.transform.position, Quaternion.Euler(0,0,0));    
            }
            else {
                fireball = Instantiate(rangedEnemy.projectile, gameObject.transform.position, Quaternion.Euler(0,0,180));
            }
            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(rangedEnemy.getHorizontalDirection()*20, 0), ForceMode2D.Impulse);
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
        else if (gameObject.tag == "Fire Worm") {
            playAnimation("FireWormDeath");
        }
        StartCoroutine(AddDeathDelay(2f));
    }

    IEnumerator AddDeathDelay(float waitTime) {
        SpawnSouls(this.numberOfSouls);
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    void SpawnSouls(int range) {
        GameObject[] souls = new GameObject[range];
        for (int i = 0; i < souls.Length; i++) {
            souls[i] = Instantiate(soul, transform.position, Quaternion.Euler(0,0,0));
            souls[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(2.5f, 7.5f)), ForceMode2D.Impulse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.player = GameObject.Find("Player").GetComponent<Player>();
        determineEnemy();
    }

    void Awake() {
        instance = this;
    }

    void determineEnemy() {
        if (gameObject.tag == "Minotaur") {
            xMagnitude = 3500f;
            yMagnitude = 1500f;
            playerKnockbackMagnitude = 20f;
        }
        else if (gameObject.tag == "Skeleton") {
            xMagnitude = 1500f;
            yMagnitude = 0;
            playerKnockbackMagnitude = 10f;
        }
        else if (gameObject.tag == "FireWorm") {
            xMagnitude = 0;
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
