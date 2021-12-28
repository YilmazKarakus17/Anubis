using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private bool alreadyIncremented;

    void OnCollisionEnter2D(Collision2D otherCollider) {
        if (otherCollider.gameObject.tag == "Player") {
            if (!this.alreadyIncremented) {
                this.alreadyIncremented = true;
                otherCollider.gameObject.GetComponent<Player>().incrementSouls();
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.alreadyIncremented = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
