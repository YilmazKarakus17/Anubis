using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //GameObject Variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject soul;

    //Soul Variables
    [SerializeField] private int numberOfSouls = 1;

    //Goblin Attack Variables
    [SerializeField] private GameObject projectile;
    [SerializeField] private float bombThrowRangeX;
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
    const string DEATH = "Death";
    ///================================================ Animation Methods ================================================//
    public void playIdleAnimation(){ this.animator.Play(IDLE); }
    public void playMeleeAnimation(){ this.animator.Play(ATTACK_MELEE); }
    public void playBombThrowAnimation(){ this.animator.Play(ATTACK_BOMB); }
    public void playDeathAnimation(){ this.animator.Play(DEATH); }
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

    float calculateAbsoluteXAxisDistance(){
        //If the Player is to the right hand side then get the difference in x and times by -1 to get absolute value
        if (((transform.position.x - this.player.transform.position.x) < 0)){
            return (transform.position.x - this.player.transform.position.x) *-1;
        }
        //else return the position of the player must be to the left (i.e. x is smaller) so simply return the difference
        return transform.position.x - this.player.transform.position.x;
    }

    //If the player's distance is less than the meleeAttackRange the performMeleeAttack variable is set to true
    void checkInBombThrowRange(){
        //Checks if the player is in distance in terms of the x axis
        float dst = this.calculateAbsoluteXAxisDistance();
        if (dst <= this.bombThrowRangeX){
            this.playerInRange = true;
        }
        else{
            this.playerInRange = false;
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

    //Method that gets called when goblin dies
    public void goblinDeath(){
        StartCoroutine(GoblinDies());
    }

    //Instantiates n number of souls
    void SpawnSouls(int range) {
        GameObject[] souls = new GameObject[range];
        for (int i = 0; i < souls.Length; i++) {
            souls[i] = Instantiate(soul, transform.position, Quaternion.Euler(0,0,0));
            souls[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(2.5f, 7.5f)), ForceMode2D.Impulse);
        }
    }
    //================================================ Unity Special Methods ================================================//
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerInRange = false;
        this.performMeleeAttack = false;
        this.countdownTimeBtwShots = 0; //The goblin can immediately throw a bomb at the player
        this.countdownTimeBtwMeleeAttacks = 0; //The goblin can immediately attack the player
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.checkInBombThrowRange();
        this.checkInMeleeAttackRange();
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

    //Coroutine for when the goblin dies
    IEnumerator GoblinDies() {
        this.playDeathAnimation();
        yield return new WaitForSeconds(1f);
        this.SpawnSouls(this.numberOfSouls);
        Destroy(gameObject);
    }
}
