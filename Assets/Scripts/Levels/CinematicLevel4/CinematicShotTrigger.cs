using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicShotTrigger : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            camera.GetComponent<LevelFourCinematicCamera>().startCinematicShot();
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider){
        if (otherCollider.gameObject.tag == "Player"){
            camera.GetComponent<LevelFourCinematicCamera>().startCinematicShot();
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
