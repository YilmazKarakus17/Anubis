using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    // Combat variables
    public float health = 100;
    public float attackDamage = 25;

    // Reference to player.
    public GameObject playerObject;
    
    // projectile --> the bullet being shot by the enemy (obtainable from prefabs in Unity).
    public GameObject projectile;
    public float fireRate;
    public float shotMagnitude;
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
        if (playerObject.gameObject.transform.position.x < gameObject.transform.position.x) {
            // Shoot to the left.
            return -1;
        }
        // Shoot to the right.
        return 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(Time.time > shootNext) {
            shootNext = Time.time + fireRate;
            GameObject shot = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0,0,90));
            shot.GetComponent<Rigidbody2D>().AddForce(new Vector2(ProjectileDirection()*shotMagnitude, 0), ForceMode2D.Impulse);
        }
    }
}
