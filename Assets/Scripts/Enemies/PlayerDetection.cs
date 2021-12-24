using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public GameObject player;
    private EnemyActionManager actionManager;
    public float attackRate;
    private float timeBetweenAttacks;
    private bool detectable;
    
    // Start is called before the first frame update
    void Start()
    {
        actionManager = transform.parent.gameObject.GetComponent<EnemyActionManager>();
        player = GameObject.Find("Player");
        this.timeBetweenAttacks = 0f;
        this.detectable = true;
    }

    void OnTriggerStay2D(Collider2D otherCollider) { // If the gameObject has a RigidBody2D component, set the RigidBody2D's Sleeping Mode to "Never Sleep".
        if (this.detectable && otherCollider.gameObject.tag == "Player" && !actionManager.getIsCharging()) {
            actionManager.setIsAttacking(true);
            this.detectable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.detectable && this.timeBetweenAttacks > 0) {
            timeBetweenAttacks -= Time.deltaTime;
        }
        else if (this.timeBetweenAttacks <= 0) {
            this.detectable = true;
            this.timeBetweenAttacks = this.attackRate;
        }
    }
}
