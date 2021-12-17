using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*========================== Instance Variables ==========================*/
    private Rigidbody2D rigidbody;

    //Player Stats Variables
    public float healthPoints = 100;
    public float soulCount = 0;
    public float dashCount = 0;

    /*========================== Getter and Setter Methods ==========================*/
    public bool isPlayerAlive()
    {
        if (this.healthPoints > 0){
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
