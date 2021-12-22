using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private new Renderer renderer;
    // Combat variables
    public float health = 100;
    public float meleeAttackDamage = 25;

    public float getMeleeAttackDamage(){
        return this.meleeAttackDamage;
    }

    public void TakeDamage(float playerAttackDamage) {
        // The enemy takes damage and will die if its health reaches 0.
        health -= playerAttackDamage;
        StartCoroutine(ChangeColour());
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeColour() {
        renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.25f);
        renderer.material.SetColor("_Color", Color.white);
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        
    }
}