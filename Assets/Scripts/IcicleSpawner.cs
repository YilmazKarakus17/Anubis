using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleSpawner : MonoBehaviour
{

    public GameObject icicle;
    public GameObject left;
    public GameObject right;
    private bool generate;

    public float delay;
    private float countdownTimer;

    public void start(){
        this.generate = true;
    }

    public void stop(){
        this.generate = false;
        this.countdownTimer = delay;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.generate = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 leftPos = left.transform.position; Vector2 rightPos = right.transform.position;

        float randomX = Random.Range(leftPos.x, rightPos.x);

        Vector2 position = new Vector2(randomX,leftPos.y);
        
        if (this.generate){
            if (this.countdownTimer <= 0){
                this.countdownTimer = delay;
                Instantiate(this.icicle, position, Quaternion.identity);
            }
            else{
                this.countdownTimer -= Time.deltaTime;
            }
        }

    }
}