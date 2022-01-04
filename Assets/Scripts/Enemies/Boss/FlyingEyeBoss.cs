using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBoss : MonoBehaviour
{
    //GameObject Variables
    private GameObject player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject exitDoor;

    //Variables for Boss Deaths
    private float numberOfDeaths;
    private bool isResurrecting;
    [SerializeField] private float resurrectedHealth = 1000;
    
    //Variables for Air Attacks
    [SerializeField] private Transform eye;
    [SerializeField] private float timeBtwShots;
    private float countdownTimeBtwShots;

    //Variables for Melee Attacks
    private bool performMeleeAttack;
    [SerializeField] private float timeBtwMeleeAttacks;
    private float countdownTimeBtwMeleeAttacks;

    //Variables for Boss Movement
    [SerializeField] private float movementDelay;
    private float movementDelayTimer;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] locations;
    private int indexLocations;

    //Animation Variables
    [SerializeField] private Animator animator;
    const string IDLE = "FlyingEyeIdle";
    const string ATTACK_MELEE = "FlyingEyeAttackMelee";
    const string DEATH1 = "FlyingEyeResurrect";
    const string DEATH2 = "FlyingEyeDeath";
    ///================================================ Animation Methods ================================================//
    public void playIdleAnimation(){ this.animator.Play(IDLE); }
    public void playMeleeAnimation(){ this.animator.Play(ATTACK_MELEE); }
    public void playDeath1Animation(){ this.animator.Play(DEATH1); }
    public void playDeath2Animation(){ this.animator.Play(DEATH2); }

    //================================================ Instance Methods ================================================//
    //Performs a air attack and plays its animation
    void performAirAttack(){
        Instantiate(this.projectile, this.eye.position, Quaternion.identity);
        this.countdownTimeBtwShots = this.timeBtwShots;
    }
    //================================================ Unity Special Methods ================================================//
    //Collider checks if the player has made contact with the Flying Eye and sets meleeAttack to true
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.performMeleeAttack = true;
        }
    }

    //Collider checks if the player has made contact with the Flying Eye and sets meleeAttack to true
    void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.performMeleeAttack = true;
        }
    }

    //If player exits the collider of the Flying Eye it prevents any more melee attacks from occurring
    void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.performMeleeAttack = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.countdownTimeBtwShots = this.timeBtwShots;
        this.countdownTimeBtwMeleeAttacks = 0; //The goblin can immediately attack the player
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.movementDelayTimer = this.movementDelay;
        GetComponent<Enemy>().setIsBoss();
        this.numberOfDeaths = 0;
        this.isResurrecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool dead = GetComponent<Enemy>().getisDead();

        //If Flying Eye is currently alive and has died less than 2 times
        if (!dead && this.numberOfDeaths < 2){
            //=========== Code for Attacks ===========//
            this.countdownTimeBtwShots -= Time.deltaTime;
            this.countdownTimeBtwMeleeAttacks -= Time.deltaTime;
            if (this.player.GetComponent<Player>().isPlayerAlive()){
                if (this.countdownTimeBtwMeleeAttacks <=0 && this.performMeleeAttack){
                    this.countdownTimeBtwMeleeAttacks = this.timeBtwMeleeAttacks;
                    StartCoroutine(MeleeAttack());
                    this.performMeleeAttack = false;
                }
                else if (this.countdownTimeBtwShots <=0){
                    this.performAirAttack();
                }
            }

            //=========== Code for Movement ===========//
            this.movementDelayTimer -= Time.deltaTime;

            //The Flying Eye can only move if the player is still alive and if it's not currently performing a melee attack on the player and it has waited at the spot it's at for long enough
            if (this.movementDelayTimer <= 0 && player.GetComponent<Player>().isPlayerAlive() && !this.performMeleeAttack){
                if (Vector2.Distance(transform.position, this.locations[this.indexLocations].position) < 0.02f)
                {
                    this.indexLocations += 1;
                    if (this.indexLocations==locations.Length-1){ this.indexLocations=0; }
                    this.movementDelayTimer = this.movementDelay;
                }
                
                transform.position = Vector2.MoveTowards(transform.position, locations[this.indexLocations].position, speed * Time.deltaTime);
            }

        }
        //If Flying eye is not alive but has 0 deaths, then resurrect and increcment the number of deaths to 1
        else if (this.numberOfDeaths == 0 && dead){
            this.numberOfDeaths += 1;
           StartCoroutine(ResurrectFlyingEye());
        }
        //If Flying Eye is not alive and doesn't have any prior deaths and isn't in the process of being resurrected (i.e. has died once and was resurrected)
        else if (this.numberOfDeaths == 1 && dead && !this.isResurrecting){
            this.numberOfDeaths += 1;
        }
        //If the Flying Eye is on it's second death meaning it has now fully perished
        else if (this.numberOfDeaths == 2){
            this.numberOfDeaths += 1;
            StartCoroutine(DeathOfFlyingEye());
        }
    }

    //================================================ Coroutines ================================================//
    //Coroutine for the Melee Attack - plays the melee attack animation after n amount of time
    IEnumerator MeleeAttack() {
        this.playMeleeAnimation();
        yield return new WaitForSeconds(0.49f);
        //By the time the bite animation happens they player could have moved away from the Flying Eye
        if (this.performMeleeAttack){
            //Melee attacks should only happen if the player is still next to the Flying Eye
            GetComponent<Enemy>().performSinlgeMeleeAttack(this.player, 20, 10);
        }
        StartCoroutine(MeleeAttackReturnIdle());
    }

    //Coroutine for the waiting to play the idle animation
    IEnumerator MeleeAttackReturnIdle() {
        yield return new WaitForSeconds(0.08f);
        this.playIdleAnimation();
    }

    //Coroutine for the waiting to resurrect the Flying Eye
    IEnumerator ResurrectFlyingEye() {
        this.isResurrecting = true;
        this.playDeath1Animation();
        yield return new WaitForSeconds(5f);
        this.playIdleAnimation();
        GetComponent<Enemy>().setHealth(this.resurrectedHealth);
        GetComponent<Enemy>().setIsDead(false);
        this.isResurrecting = false;
        this.timeBtwShots = 0.4f;
    }

    //Coroutine for the waiting to play out the death of Flying Eye
    IEnumerator DeathOfFlyingEye() {
        this.playDeath2Animation();
        yield return new WaitForSeconds(1f);
        this.exitDoor.SetActive(true);
        Destroy(gameObject);
    }
}
