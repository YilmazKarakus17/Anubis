using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopIcicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Spawner;
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.Spawner.GetComponent<IcicleSpawner>().stopSpawning();
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.Spawner.GetComponent<IcicleSpawner>().stopSpawning();
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
