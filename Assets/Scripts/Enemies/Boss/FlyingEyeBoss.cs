using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBoss : MonoBehaviour
{
    public Transform eye;

    public float timeBtwShots;
    private float countdownTimeBtwShots;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        this.countdownTimeBtwShots = this.timeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.countdownTimeBtwShots <=0){
            Instantiate(this.projectile, this.eye.position, Quaternion.identity);
            this.countdownTimeBtwShots = this.timeBtwShots;
        }else{
            this.countdownTimeBtwShots -= Time.deltaTime;
        }
    }
}
