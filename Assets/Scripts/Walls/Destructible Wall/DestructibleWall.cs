using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public float health;

    public void TakeDamage(float playerAttackDamage) {
        // The wall takes damage and will get destroyed if its health reaches 0.
        health -= playerAttackDamage;
        StartCoroutine(ChangeColour());
        if (this.health <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeColour() {
        GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        yield return new WaitForSeconds(0.25f);
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
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
