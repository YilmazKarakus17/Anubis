using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
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
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.tag == "Player"){
            this.collided = false;
        }
    }

    void Start(){
        this.drop = false;
        GetComponent<PlatformEffector2D>().surfaceArc = this.surfaceArc;
    }

    public void Update(){
        if (this.collided && this.drop && this.allowedToDrop){
            GetComponent<PlatformEffector2D>().surfaceArc = 0f;
            this.drop = false;
            StartCoroutine(Wait());
        }
        else{
            if (this.drop){this.drop=false;}
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(0.3f);
        GetComponent<PlatformEffector2D>().surfaceArc = this.surfaceArc;
    }
}
