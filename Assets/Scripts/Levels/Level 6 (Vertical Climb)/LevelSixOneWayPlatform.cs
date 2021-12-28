using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSixOneWayPlatform : MonoBehaviour
{
    [SerializeField]
    private float surfaceArc;

    [SerializeField]
    private bool allowedToDrop;

    private bool collided;
    private bool drop;

    public void Drop(InputAction.CallbackContext value) {
        if (value.performed){
            this.drop = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.collided = true;
            other.gameObject.GetComponent<PlayerMovement>().setHorizontalSpeed(12);
            other.gameObject.GetComponent<PlayerMovement>().setJumpForce(35);
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.collided = false;
            other.gameObject.GetComponent<PlayerMovement>().setHorizontalSpeed(12);
            other.gameObject.GetComponent<PlayerMovement>().setJumpForce(35);
        }
    }

    void Start(){
        this.drop = false;
        GetComponent<PlatformEffector2D>().surfaceArc = this.surfaceArc;
    }

    public void Update(){
        if (this.collided && this.drop && this.allowedToDrop){
            GetComponent<PlatformEffector2D>().surfaceArc = 0f;
            StartCoroutine(Wait());
            this.drop = false;
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(0.3f);
        GetComponent<PlatformEffector2D>().surfaceArc = this.surfaceArc;
    }
}