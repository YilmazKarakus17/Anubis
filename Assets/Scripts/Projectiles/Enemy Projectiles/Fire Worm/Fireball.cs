using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private EnemyActionManager enemy;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Fire Worm").GetComponent<EnemyActionManager>();
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        // Layer 11 is the player layer.
        if (otherCollider.gameObject.layer == 11) {
            Destroy(gameObject);
            otherCollider.GetComponent<Player>().decreaseHealthByPoint(enemy.attackDamage);
            // If the enemy is left of the player, knockback the player to the right and vice versa.
            if (transform.position.x < otherCollider.transform.position.x) {
                otherCollider.GetComponent<Player>().Knockback(10, 0);
            }
            else {
                otherCollider.GetComponent<Player>().Knockback(-10, 0);
            }
        }
        else if ((otherCollider.gameObject.layer == 12)) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

