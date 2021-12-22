using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBoss : MonoBehaviour
{
    
    private Animator animator;
    private string currentState;

    //Variables for shooting/air attacking
    public Transform eye;
    public float timeBtwShots;
    private float countdownTimeBtwShots;

    //Variables for Melee Attacking
    private bool isMeleeAttacking;
    private float meleeAttackDuration;
    private float meleeAttackTimer;

    //GameObject Variables
    public GameObject player;
    public GameObject projectile;

    //Variable For looking at players direction
    private bool lookingLeft;

    //Animation Variables
    const string IDLE = "FlyingEyeIdle";
    const string ATTACK_MELEE = "FlyingEyeAttackMelee";

    ///================================================ Animation Methods ================================================//
    public void playIdleAnimation(){ this.animator.Play(IDLE); }

    public void playMeleeAnimation(){ this.animator.Play(ATTACK_MELEE); }
    //================================================ Instance Methods ================================================//
    //Performs a air attack and plays its animation
    void performAirAttack(){
        Instantiate(this.projectile, this.eye.position, Quaternion.identity);
        this.countdownTimeBtwShots = this.timeBtwShots;
    }

    //Performs a melee attack and plays its animation
    void performMeleeAttack(){
        this.playMeleeAnimation();

        //Finds the direction in which the knockback should push the player to
        float knockbackDirection = 0;
        if (gameObject.transform.position.x < this.player.transform.position.x) { knockbackDirection = -1; }
        else { knockbackDirection = 1; }

        //Damages player health by the melee attack damage set in the Enemy Script
        this.player.GetComponent<Player>().decreaseHealthByPoint(GetComponent<Enemy>().getMeleeAttackDamage());

        //Pushes the player back
        this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackDirection*100, 0), ForceMode2D.Impulse);
    }

    //Ensures the Flying Eye always looks at the direction of the player
    void lookTowardsPlayer(){
        Vector3 scaler = transform.localScale;

        if (this.lookingLeft && player.transform.position.x > transform.position.x){
            this.lookingLeft = false;
            this.flip();
        }
        else if (!this.lookingLeft && player.transform.position.x < transform.position.x){
            this.lookingLeft = true;
            this.flip();
        }
    }

    //Flips the Flying Eye horizontally
    void flip(){
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    //================================================ Unity Special Methods ================================================//

    //Collider checks if the player has made contact with the Flying Eye and sets meleeAttack to true
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.isMeleeAttacking = true;
            this.meleeAttackTimer = this.meleeAttackDuration;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.currentState = IDLE;
        
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.countdownTimeBtwShots = this.timeBtwShots;
        this.lookingLeft = true;

        this.isMeleeAttacking = false;
        this.meleeAttackDuration = 1f;
        this.meleeAttackTimer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        this.lookTowardsPlayer();

        this.meleeAttackTimer -= Time.deltaTime;

        if (this.meleeAttackTimer > 0){
            if (this.isMeleeAttacking){
                this.performMeleeAttack();
                this.isMeleeAttacking = false;
            }
        }
        else {
            if (this.countdownTimeBtwShots <=0){
                this.performAirAttack();
            }
            else{
                this.countdownTimeBtwShots -= Time.deltaTime;
                this.playIdleAnimation();
            }
        }
        
    }
}
