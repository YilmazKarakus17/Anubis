using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Combat variables
    public float health = 100;
    public float attackDamage = 25;

    // Reference to player.
    public GameObject shootAt;
    
    // projectile --> the bullet being shot by the enemy (obtainable from prefabs in Unity).
    public GameObject projectile;
    public float fireRate = 2.5f;
    public float shotMagnitude = 10;
    private float shootNext = 0;

    public void TakeDamage(float playerAttackDamage) {
        // The enemy takes damage and will die if its health reaches 0.
        health -= playerAttackDamage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private float ProjectileDirection() {
        // The object that the enemy needs to shoot at is left of the enemy
        if (shootAt.gameObject.transform.position.x < gameObject.transform.position.x) {
            // Shoot to the left.
            return -1;
        }
        // Shoot to the right.
        return 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        shootAt = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(Time.time > shootNext) {
            shootNext = Time.time + fireRate;
            GameObject shot = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileDirection()*shotMagnitude, 0), ForceMode2D.Impulse);
        }
    }
}
