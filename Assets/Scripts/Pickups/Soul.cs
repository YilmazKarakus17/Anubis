using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D otherCollider) {
        if (otherCollider.gameObject.tag == "Player") {
            Destroy(gameObject);
            otherCollider.gameObject.GetComponent<Player>().addSouls(1);
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
