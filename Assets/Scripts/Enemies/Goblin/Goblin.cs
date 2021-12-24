using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //GameObject Variables
    private GameObject player;

    [SerializeField] private GameObject projectile;
    
    [SerializeField] private Transform bombSpawn;
    [SerializeField] private float timeBtwShots;
    [SerializeField] private float timeBtwMeleeAttacks;

    private float countdownTimeBtwShots;
    private float countdownTimeBtwMeleeAttacks;
    private bool playerInRange;
    private bool performMeleeAttack;
    [SerializeField] private float meleeAttackRange;

    //Animation Variables
    private Animator animator;
    const string IDLE = "Idle";
    const string ATTACK_MELEE = "MeleeAttack";
    const string ATTACK_BOMB = "ThrowAttack";
    ///================================================ Animation Methods ================================================//
    public void playIdleAnimation(){ this.animator.Play(IDLE); }
    public void playMeleeAnimation(){ this.animator.Play(ATTACK_MELEE); }
    public void playBombThrowAnimation(){ this.animator.Play(ATTACK_BOMB); }

    //================================================ Instance Methods ================================================//
    //Creates a Globlin Bomb game object and throws it towards the players direction
    void performBombThrow(){
        GameObject bomb = Instantiate(this.projectile, this.bombSpawn.position, Quaternion.identity);
        if (transform.position.x < this.player.transform.position.x){
            bomb.GetComponent<GoblinBomb>().throwBomb(this.player, 1);
        }
        else{
            bomb.GetComponent<GoblinBomb>().throwBomb(this.player, -1);
        }
    }

    //If the player's distance is less than the meleeAttackRange the performMeleeAttack variable is set to true
    void checkInMeleeAttackRange(){
        if (Vector2.Distance(transform.position, this.player.transform.position) < this.meleeAttackRange){
            this.performMeleeAttack = true;
        }
        else{
            this.performMeleeAttack = false;
        }
    }
    //================================================ Unity Special Methods ================================================//
    //If player is in the trigger collider of the goblin it records the player game object and that it's in range for an attack
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.playerInRange = true;
            this.player = other.gameObject;
            this.checkInMeleeAttackRange();
        }
    }

    //While the player is in the trigger collider of the goblin it records the player game object and that it's in range for an attack
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.playerInRange = true;
            this.player = other.gameObject;
            this.checkInMeleeAttackRange();
        }
    }

    //If player exits the trigger collider of the goblin it records that the player is no longer in the range for throws
    void OnTriggerExit2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            this.playerInRange = false;
            this.checkInMeleeAttackRange();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playerInRange = false;
        this.countdownTimeBtwShots = 0; //The goblin can immediately throw a bomb at the player
        this.countdownTimeBtwMeleeAttacks = 0; //The goblin can immediately attack the player
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.countdownTimeBtwShots -= Time.deltaTime;
        this.countdownTimeBtwMeleeAttacks -= Time.deltaTime;
        if (this.countdownTimeBtwMeleeAttacks <=0 && this.player.GetComponent<Player>().isPlayerAlive() && this.performMeleeAttack){
            this.countdownTimeBtwMeleeAttacks = this.timeBtwMeleeAttacks;
            StartCoroutine(MeleeAttack());
            this.performMeleeAttack = false;
        }
        else if (this.countdownTimeBtwShots <=0 && this.player.GetComponent<Player>().isPlayerAlive() && this.playerInRange){
            this.countdownTimeBtwShots = this.timeBtwShots;
            StartCoroutine(ThrowBomb());
        }
    }

    //================================================ Coroutines ================================================//
    //Coroutine for the throw bomb effect - plays the bomb throw animation and performs a bomb throw after n amount of time
    IEnumerator ThrowBomb() {
        this.playBombThrowAnimation();
        yield return new WaitForSeconds(0.83f);
        this.performBombThrow();
        this.playIdleAnimation();
    }

    //Coroutine for the Melee Attack - plays the melee attack animation after n amount of time
    IEnumerator MeleeAttack() {
        this.playMeleeAnimation();
        yield return new WaitForSeconds(0.36f);
        GetComponent<Enemy>().performSinlgeMeleeAttack(this.player,20,10);
        StartCoroutine(MeleeAttackReturnIdle());
    }

    //Coroutine for the waiting to play the idle animation
    IEnumerator MeleeAttackReturnIdle() {
        yield return new WaitForSeconds(0.11f);
        this.playIdleAnimation();
    }
}
