using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovingCeilingSpeed : MonoBehaviour
{
    [SerializeField] private float speed = 14;
    [SerializeField] private float exitSpeed = 3;
    [SerializeField] private GameObject tileMap;
    [SerializeField] private bool stopPlatformMoving = false;

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.tileMap.GetComponent<MovingCeiling>().changeSpeed(this.speed);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.tileMap.GetComponent<MovingCeiling>().changeSpeed(this.exitSpeed);
            if (this.stopPlatformMoving){
                this.tileMap.GetComponent<MovingCeiling>().finishMovingCeiling();
            }
        }
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
