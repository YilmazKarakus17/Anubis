using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "LargeProjectileDespawner"){
            Destroy(gameObject);
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
