using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private new Renderer renderer;
    public GameObject player;

    // Combat variables
    public float health = 100;
    public float meleeAttackDamage = 25;

    //Variable For looking at players direction
    public bool lookingLeft;

    public float getMeleeAttackDamage(){
        return this.meleeAttackDamage;
    }

    public void TakeDamage(float playerAttackDamage) {
        // The enemy takes damage and will die if its health reaches 0.
        health -= playerAttackDamage;
        StartCoroutine(ChangeColour());
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void performSinlgeMeleeAttack(GameObject Player, float x, float y){
        Player.GetComponent<Player>().decreaseHealthByPoint(this.meleeAttackDamage);
        // If the enemy is left of the player, knockback the player to the right and vice versa.
        if (transform.position.x < Player.transform.position.x) {
            Player.GetComponent<Player>().Knockback(x, y);
        }
        else {
            Player.GetComponent<Player>().Knockback(-x, y);
        }
    }

    IEnumerator ChangeColour() {
        renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.25f);
        renderer.material.SetColor("_Color", Color.white);
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

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.lookTowardsPlayer();
    }

    void FixedUpdate() {
        
    }
}