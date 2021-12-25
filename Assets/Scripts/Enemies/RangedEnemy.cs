using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectile;
    private float horizontalDirection;

    public float getHorizontalDirection() {
        return this.horizontalDirection;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (transform.localScale.x < 0) {
            this.horizontalDirection = -1;
        }
        else {
            this.horizontalDirection = 1;
        }
    }
}
