using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTraps : MonoBehaviour
{
    private GameObject player;

    public bool instantKills;
    
    private bool initialDamage;
    private bool stayDamage;
    private bool currentlyDamagingForStaying;
    private bool exitDamage;

    //If a player is hits the spike initially a lot of hp is taken away
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.initialDamage = true;
        }
    }

    //When the player leaves the spikes a final critical hit is performed
    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.stayDamage = true;
        }
    }

    //As the player stays in the spike more damage occurs gradually
    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            this.player = other.gameObject;
            this.exitDamage = true;
        }
    }

    //Damages the player if the player is still alive
    void damagePlayer(float Amount){
        if (this.player.GetComponent<Player>().isPlayerAlive()){
            this.player.GetComponent<Player>().decreaseHealthByPoint(Amount);
            this.initialDamage = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.initialDamage = false;
        this.stayDamage = false;
        this.exitDamage = false;
        this.currentlyDamagingForStaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.instantKills){
            if (this.initialDamage){
                this.damagePlayer(10);
                this.initialDamage = false;
            }
            else if (this.stayDamage && !this.currentlyDamagingForStaying){
                this.stayDamage = false;
                StartCoroutine(onStayDamage());
            }
            else if (this.exitDamage){
                this.damagePlayer(25);
                this.exitDamage = false;
            }
        }
        else{
            if ((this.initialDamage || this.stayDamage || this.exitDamage) && this.player.GetComponent<Player>().isPlayerAlive()){
                this.player.GetComponent<Player>().killPlayer();
            }
        }
    }

    
    IEnumerator onStayDamage() {
        this.currentlyDamagingForStaying = true;
        this.damagePlayer(5);
        yield return new WaitForSeconds(1f);
        this.currentlyDamagingForStaying = false;
    }
}
