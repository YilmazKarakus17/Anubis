using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Sound variables
    public AudioSource coinSound;
    private bool allowIncrement;

    //If a player is hits the spike initially a lot of hp is taken away
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player" && this.allowIncrement){
            other.gameObject.GetComponent<Player>().incrementCoinCount();
            StartCoroutine(destroyCoin());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.allowIncrement = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Coroutine for the waiting to play out the death of Flying Eye
    IEnumerator destroyCoin() {
        this.allowIncrement = false;
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        this.coinSound.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
