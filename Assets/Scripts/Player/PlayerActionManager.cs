using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public bool isAttacking = false;
    public static PlayerActionManager instance;
    private Rigidbody2D r2d;
    public bool applyAttackThrust = false;

    // Combat variables
    public float attackDamage = 25;
    private float horizontalDirection;
    public bool applyDoubleDamage;
    // Attack range/radius and enemy detection variables
    public Transform attackCentre;
    public float attackRange = 1.25f;
    public GameObject projectile;
    public bool isShockwaveActive;

    void Awake() {
        instance = this;
    }

    public void Attack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking) {
            isAttacking = true;
        }
    }

    public void Shockwave(InputAction.CallbackContext context) {
        if (context.performed && !isShockwaveActive) {
            isAttacking = true;
            isShockwaveActive = true;
        }
    }

    public void ApplyDamage(float positionIncrease, float rangeIncrease, float waitTime) {
        StartCoroutine(AddDelay(positionIncrease, rangeIncrease, waitTime));
    }

    public void ApplyShockwave(float positionIncrease, float rangeIncrease, float waitTime) {
        StartCoroutine(AddShockwaveDelay(positionIncrease, rangeIncrease, waitTime));
    }

    IEnumerator AddDelay(float positionIncrease, float rangeIncrease, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        // Enemy hit detection with the sword swings. All of the enemies that intersect with the sword's radius will take damage.
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackCentre.position+new Vector3(positionIncrease, 0, 0), attackRange+rangeIncrease, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemiesHit.Length; i++) {
            Enemy enemy = enemiesHit[i].GetComponent<Enemy>();
            // Dead enemies will not take any damage.
            if (!enemy.getIsDeaded()) {
                if (this.applyDoubleDamage) {
                    enemiesHit[i].GetComponent<Enemy>().TakeDamage(attackDamage*2);
                }
                else {
                    enemiesHit[i].GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
        }
    }

    IEnumerator AddShockwaveDelay(float positionIncrease, float rangeIncrease, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        GameObject shockwave;
        if (this.horizontalDirection < 0) {
            shockwave = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0,0,180));
        }
        else {
            shockwave = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0,0,0));
        }
        shockwave.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.horizontalDirection*20, 0), ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (transform.localScale.x < 0) {
            this.horizontalDirection = -1;
        }
        else {
            this.horizontalDirection = 1;
        }
        if (applyAttackThrust) {
            r2d.AddForce(new Vector2(this.horizontalDirection*10, 0), ForceMode2D.Impulse);
            applyAttackThrust = false;
        }
    }
}
