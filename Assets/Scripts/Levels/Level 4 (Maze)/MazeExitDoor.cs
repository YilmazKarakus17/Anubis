using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float keysRequired = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player.GetComponent<CollectingKeys>().collectedAllKeys(this.keysRequired)){
            Destroy(gameObject);
        }
    }
}
