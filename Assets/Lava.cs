using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D otherCollider) {
        // Layer 11 is the player layer. Lava will instantly kll player.
        if (otherCollider.gameObject.layer == 11) {
            float direction;
            if (otherCollider.gameObject.transform.localScale.y >= 0) {
                direction = 1;
            }
            else {
                direction = -1;
            }

            otherCollider.GetComponent<Player>().decreaseHealthByPoint(100);

            Vector3 velocity = otherCollider.GetComponent<Rigidbody2D>().velocity;
            velocity.x = 0;
            velocity.y = -1 * direction;
            otherCollider.GetComponent<Rigidbody2D>().velocity = velocity;

            
            otherCollider.GetComponent<Rigidbody2D>().gravityScale = -0.025f * direction;
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
