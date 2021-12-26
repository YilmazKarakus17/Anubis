using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    public float speed;
    public Transform end;

    private int i; // index of an array

    private GameObject player;
    public bool movingAllowed;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.movingAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player.GetComponent<Player>().isPlayerAlive() && this.movingAllowed){
            if (Vector2.Distance(transform.position, this.end.position) < 0.02f)
            {
                this.movingAllowed = false;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, this.end.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.movingAllowed = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Player>().killPlayerFORCE();
        }
    }
}
