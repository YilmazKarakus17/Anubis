using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //GameObject Variables
    private GameObject player;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject projectile;
    
    [SerializeField] private Transform bombSpawn;
    [SerializeField] private float timeBtwShots;

    private float countdownTimeBtwShots;
    private bool playerInRange;

    void performAirAttack(){
        GameObject bomb = Instantiate(this.projectile, this.bombSpawn.position, Quaternion.identity);
        this.countdownTimeBtwShots = this.timeBtwShots;
        if (transform.position.x < this.player.transform.position.x){
            bomb.GetComponent<GoblinBomb>().throwBomb(this.player, 1);
        }
        else{
            bomb.GetComponent<GoblinBomb>().throwBomb(this.player, -1);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.playerInRange = true;
            this.player = other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.playerInRange = true;
            this.player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            this.playerInRange = false;
        }
    }

    //Coroutine for the knockback effect
    IEnumerator knockBack() {
        animator.Play("BombExplode");
        yield return new WaitForSeconds(0.8f);
        this.explode();
        StartCoroutine(destroyBomb());
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playerInRange = false;
        this.countdownTimeBtwShots = 0; //The goblin can immediately attack the player
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.countdownTimeBtwShots -= Time.deltaTime;
        if (this.countdownTimeBtwShots <=0 && this.player.GetComponent<Player>().isPlayerAlive() && this.playerInRange){
            this.performAirAttack();StartCoroutine(knockBack());
        }
    }
}
