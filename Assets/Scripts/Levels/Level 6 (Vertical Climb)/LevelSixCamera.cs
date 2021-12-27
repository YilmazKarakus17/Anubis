using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSixCamera : MonoBehaviour
{
    /*
        Special Camera for level 6 that initially chases the player vertically (i.e. upwards), then does a slow motion cinematic shot and finally centres on the player
    */
    
    //Level Vertical Movement variables
    public float levelSpeed;
    public Transform levelEnd;
    private bool verticalCameraMovement;

    //Cinematic Camera Variables
    public Transform cinematicPoint;
    public float cinematicSpeed = 5;
    public float cinematicReturnSpeed = 14;
    private bool cinematicCameraMovement;
    private bool cinematicCameraReturnMovement;

    //Player Centering Variables
    public GameObject player;
    public float yOffset;
    private bool playerCenteredCameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        //First starts with vertical (upwards movement) camera movement
        this.verticalCameraMovement = true;
        this.cinematicCameraMovement = false;
        this.cinematicCameraReturnMovement= false;
        this.playerCenteredCameraMovement = false;

    }

    void focusOnPlayer(){
        this.player.GetComponent<PlayerMovement>().setAllowedToMove(true);
        Vector2 targetV2 = this.player.transform.position; // Gets the 2D coordinates of the 2D object that represents the end of the level.
        Vector3 targetV3 = transform.position; // Gets the current 3D position of the GameObject it's assigned to, in this case it's the camera
        //Only changes the x and y so that the cameras original z position isn't affected.
        targetV3.x = targetV2.x; 
        targetV3.y = targetV2.y + yOffset; //Offsets the camera's y to not have the player object directly in the middle
        transform.position = targetV3;
    }

    void cinematicReturnShot(){
        Vector3 target = transform.position;
        target.x = this.player.transform.position.x; 
        target.y = this.player.transform.position.y;
        if (Vector2.Distance(transform.position, target) < 0.02f)
        {
            this.cinematicCameraReturnMovement= false;
            this.playerCenteredCameraMovement = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, this.cinematicReturnSpeed * Time.deltaTime);
    }

    void cinematicShot(){
        Vector3 target = transform.position;
        this.player.GetComponent<PlayerMovement>().setAllowedToMove(false);
        target.x = cinematicPoint.position.x; target.y = cinematicPoint.position.y;
        if (Vector2.Distance(transform.position, target) < 0.02f)
        {
            this.cinematicCameraMovement = false;
            this.cinematicCameraReturnMovement= true;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, this.cinematicSpeed * Time.deltaTime);
    }

    void chasePlayerVertically(){
        Vector3 target = transform.position;
        
        //Only keep moving to points whilst the player is alive
        if (this.player.GetComponent<Player>().isPlayerAlive()){
            target.x = levelEnd.position.x; target.y = levelEnd.position.y; 
            if (Vector2.Distance(transform.position, target) < 0.02f)
            {
                this.verticalCameraMovement = false;
                this.cinematicCameraMovement = true;;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, this.levelSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.verticalCameraMovement){
            this.chasePlayerVertically();
        }
        else if (this.cinematicCameraMovement){
            this.cinematicShot();
        }
        else if (this.cinematicCameraReturnMovement){
            this.cinematicReturnShot();
        }
        else if (this.playerCenteredCameraMovement){
            this.focusOnPlayer();
        }
    }
}
