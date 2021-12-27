using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaringMovingCeiling : MonoBehaviour
{
    [SerializeField] private GameObject CeilingTrap;
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.CeilingTrap.GetComponent<MovingCeiling>().startMovingCeiling();
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
