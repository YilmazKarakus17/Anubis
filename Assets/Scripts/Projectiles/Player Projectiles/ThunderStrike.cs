using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    private PlayerActionManager player;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player").GetComponent<PlayerActionManager>();
    }

    public void ApplyDamage(float waitTime) {
        StartCoroutine(AddDelay(waitTime));
    }

    IEnumerator AddDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, 2.5f, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemiesHit.Length; i++) {
            Enemy enemy = enemiesHit[i].GetComponent<Enemy>();
            // Dead enemies will not take any damage.
            if (!enemy.getIsDeaded()) {
                enemy.TakeDamage(this.player.attackDamage*2);
            }
        }
    }

    public void DestroyThunder() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

