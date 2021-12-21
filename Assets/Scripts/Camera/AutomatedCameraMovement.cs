using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedCameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool focusPlayerOnDeath;
    public Transform[] points;
    private int i; // index of an array

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = transform.position;
        
        //Only keep moving to points whilst the player is alive
        if (this.player.GetComponent<Player>().isPlayerAlive()){
            target.x = points[i].position.x; target.y = points[i].position.y; 
            if (Vector2.Distance(transform.position, target) < 0.02f)
            {
                if (i<points.Length-1){ i++; }
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else{
            //Only move the camera to centre on the player if the level wants that to happen
            if (focusPlayerOnDeath){
                target.x = player.transform.position.x; target.y = player.transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
    }

}
