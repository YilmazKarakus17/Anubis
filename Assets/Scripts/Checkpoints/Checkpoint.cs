using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SaveManager.instance.testSceneCheckpointX = transform.position.x;
            SaveManager.instance.testSceneCheckpointY = transform.position.y;
            SaveManager.instance.Save();
        }
    }


}