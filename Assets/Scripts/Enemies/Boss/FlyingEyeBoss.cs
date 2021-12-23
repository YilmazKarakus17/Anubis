using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBoss : MonoBehaviour
{
    //GameObject Variables
    public GameObject player;
    public GameObject projectile;

    //Variables for Boss Movement
    public float movementDelay;
    private float movementDelayTimer;
    public float speed;
    public Transform[] locations;
    private int indexLocations;

    //Variables for shooting/air attacking
    public Transform eye;
    public float timeBtwShots;
    private float countdownTimeBtwShots;

    //Variables for Melee Attacking
    private bool isMeleeAttacking;
    private float meleeAttackDuration;
    private float meleeAttackTimer;

    //Animation Variables
    private Animator animator;
    private string currentState;
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
        GetComponent<Enemy>().performSinlgeMeleeAttack(this.player, 20, 10);
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

        this.isMeleeAttacking = false;
        this.meleeAttackDuration = 1f;
        this.meleeAttackTimer = 0;

        this.movementDelayTimer = this.movementDelay;
    }


    // Update is called once per frame
    void Update()
    {
        this.meleeAttackTimer -= Time.deltaTime;

        if (this.meleeAttackTimer > 0){
            if (this.isMeleeAttacking){
                this.performMeleeAttack();
                this.isMeleeAttacking = false;
            }
        }
        else {
            if (this.countdownTimeBtwShots <=0 && player.GetComponent<Player>().isPlayerAlive()){
                this.performAirAttack();
            }
            else{
                this.countdownTimeBtwShots -= Time.deltaTime;
                this.playIdleAnimation();
            }
        }

        this.movementDelayTimer -= Time.deltaTime;
        if (this.movementDelayTimer <= 0 && player.GetComponent<Player>().isPlayerAlive()){
            if (Vector2.Distance(transform.position, this.locations[this.indexLocations].position) < 0.02f)
            {
                this.indexLocations += 1;
                if (this.indexLocations==locations.Length-1){ this.indexLocations=0; }
                this.movementDelayTimer = this.movementDelay;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, locations[this.indexLocations].position, speed * Time.deltaTime);
        }
    }
}
