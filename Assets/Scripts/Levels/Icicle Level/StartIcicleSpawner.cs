using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIcicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Spawner;
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.Spawner.GetComponent<IcicleSpawner>().startSpawning();
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
