using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    // Combat variables
    public float health = 100;
    public float attackDamage = 25;

    public void TakeDamage(float playerAttackDamage) {
        // The enemy takes damage and will die if its health reaches 0.
        health -= playerAttackDamage;
        if (health <= 0) {
            Destroy(gameObject);
            player.GetComponent<PlayerScript>().addOneSoul();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        
    }
}