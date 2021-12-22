using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurDetection : MonoBehaviour
{
    public GameObject player;
    private MinatourActionManager actionManager;
    
    // Start is called before the first frame update
    void Start()
    {
        actionManager = transform.parent.gameObject.GetComponent<MinatourActionManager>();
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.tag == "Player" && !actionManager.getIsCharging()) {
            actionManager.setIsAttacking(true);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider) {
        
        if (otherCollider.gameObject.tag == "Player" && !actionManager.getIsCharging()) {
            actionManager.setIsAttacking(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
