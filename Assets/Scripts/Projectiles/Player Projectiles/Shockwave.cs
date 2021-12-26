using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private PlayerActionManager player;
    private Enemy enemy;
    private float timeToLive;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerActionManager>();
        timeToLive = 1;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        // Layer 8 is the ground layer.
        if ((otherCollider.gameObject.layer == 8)) {
            Destroy(gameObject);
        }
        // Layer 10 is the enemy layer.
        else if (otherCollider.gameObject.layer == 10) {
            Destroy(gameObject);
            otherCollider.GetComponent<Enemy>().TakeDamage(player.attackDamage/5);
        }
        // Layer 12 is the destructible wall layer
        else if ((otherCollider.gameObject.layer == 12)) {
            Destroy(gameObject);
            otherCollider.GetComponent<DestructibleWall>().TakeDamage(player.attackDamage/5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timeToLive > 0) {
            this.timeToLive -= Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
    }
}

