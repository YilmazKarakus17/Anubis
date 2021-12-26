using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCeiling : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;

    private bool stopMoving;

    private int i; // index of an array

    //To Edit the speed
    public void changeSpeed(float Speed){
        this.speed = Speed;
    }

    public void stopMovingTrue(){
        this.stopMoving = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        this.stopMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i==points.Length)
            {
                i=0;
            }
        }
        
        if (!this.stopMoving){
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else{
            transform.position = Vector2.MoveTowards(transform.position, points[1].position, speed * Time.deltaTime);
        }
        
    }

    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Player>().killPlayerFORCE();
        }
    }
}
