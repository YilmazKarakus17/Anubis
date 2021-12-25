using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKeys : MonoBehaviour
{

    [SerializeField] private float keysCollected;
     
    //Increments the players key count
    public void incrementKeyCount(){ 
        this.keysCollected += 1;
        //UPDATE Key UI
    }

    //returns true if the number of keys the player posses is enough based on the argument passed
    public bool collectedAllKeys(float amount){
        if (this.keysCollected >= amount){
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.keysCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
