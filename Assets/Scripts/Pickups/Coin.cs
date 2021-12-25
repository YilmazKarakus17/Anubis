using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    //If a player is hits the spike initially a lot of hp is taken away
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Player>().incrementCoinCount();
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
