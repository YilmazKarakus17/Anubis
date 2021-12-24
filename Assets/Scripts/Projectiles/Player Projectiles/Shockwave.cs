using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private PlayerActionManager player;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerActionManager>();
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        // Layer 10 is the enemy layer.
        if (otherCollider.gameObject.layer == 10) {
            Destroy(gameObject);
            otherCollider.GetComponent<Enemy>().TakeDamage(player.attackDamage/5);
        }
        else if ((otherCollider.gameObject.layer == 12)) {
            Destroy(gameObject);
            otherCollider.GetComponent<DestructibleWall>().TakeDamage(player.attackDamage/5);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

