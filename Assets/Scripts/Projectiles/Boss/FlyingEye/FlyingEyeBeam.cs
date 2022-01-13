using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBeam : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    //Particle Effect Variables
    public GameObject explosionEffect;

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().decreaseHealthByPoint(5);
            this.DestroyProjectile();
        }
    }

    public void DestroyProjectile(){
        Instantiate(this.explosionEffect, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        this.speed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, target, speed* Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y){
            this.DestroyProjectile();
        }
    }
}
