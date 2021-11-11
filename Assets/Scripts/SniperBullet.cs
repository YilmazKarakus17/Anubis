using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    private PlayerCombat player;
    private float damageAmount = 75;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.tag == "Player") {
            Destroy(gameObject);
            player.ReceiveDamage(damageAmount);
        }
        else if (otherCollider.gameObject.tag == "Floor") {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
