using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBomb : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private float speedX;
    
    [SerializeField] private float speedY;
    
    [SerializeField] private float range;
    
    
    private GameObject player;

    [SerializeField] private float damage;
    
    [SerializeField] private float knockBackX;
    
    [SerializeField] private float knockBacky;
    

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    private void explode(){
        if (Physics2D.OverlapCircle(transform.position, this.range, LayerMask.GetMask("Player"))){
            this.player.GetComponent<Player>().decreaseHealthByPoint(this.damage);
            if (transform.position.x < this.player.transform.position.x) {
                this.player.GetComponent<Player>().Knockback(this.knockBackX, this.knockBacky);
            }
            else {
                this.player.GetComponent<Player>().Knockback(-this.knockBackX, this.knockBacky);
            }
        }
    }

    public void throwBomb(GameObject Player, float direction) {
        this.player = Player;
        StartCoroutine(knockBack());
        GetComponent<Rigidbody2D>().AddForce(new Vector2(direction*speedX, speedY), ForceMode2D.Impulse);
    }

    //Coroutine for the knockback effect
    IEnumerator knockBack() {
        animator.Play("BombExplode");
        yield return new WaitForSeconds(0.8f);
        this.explode();
        StartCoroutine(destroyBomb());
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator destroyBomb() {
        yield return new WaitForSeconds(0.22f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, this.range);
    }
}
