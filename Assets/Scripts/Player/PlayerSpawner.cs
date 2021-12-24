using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // sets the spawn location
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "TestScene")
        {
            transform.position = new Vector3 (SaveManager.instance.testSceneCheckpointX, SaveManager.instance.testSceneCheckpointY, 0);
        }

        
    }


}
