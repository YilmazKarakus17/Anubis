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

        if (activeSceneName == "Level1")
        {
            transform.position = new Vector3 (SaveManager.instance.level1CheckpointX, SaveManager.instance.level1CheckpointY, 0);
        }

        // if (activeSceneName == "Level2")
        // {
        //     transform.position = new Vector3 (SaveManager.instance.testSceneCheckpointX, SaveManager.instance.testSceneCheckpointY, 0);
        // }

        if (activeSceneName == "Level3")
        {
            transform.position = new Vector3 (SaveManager.instance.level3CheckpointX, SaveManager.instance.level3CheckpointY, 0);
            Debug.Log(SaveManager.instance.level3CheckpointX);
            Debug.Log(SaveManager.instance.level3CheckpointY);
        }

        // if (activeSceneName == "Level4")
        // {
        //     transform.position = new Vector3 (SaveManager.instance.testSceneCheckpointX, SaveManager.instance.testSceneCheckpointY, 0);
        // }

        if (activeSceneName == "Level5")
        {
            if (SaveManager.instance.level5CheckpointX == 0 && SaveManager.instance.level5CheckpointX == 0){
                transform.position = new Vector3 (-57.2f, 74.2f, 0);
            }
            else{
                transform.position = new Vector3 (SaveManager.instance.level5CheckpointX, SaveManager.instance.level5CheckpointY, 0);
            }
        }

        // if (activeSceneName == "Level6")
        // {
        //     transform.position = new Vector3 (SaveManager.instance.testSceneCheckpointX, SaveManager.instance.testSceneCheckpointY, 0);
        // }

        if (activeSceneName == "Level7")
        {
            if (SaveManager.instance.level7CheckpointX == 0 && SaveManager.instance.level7CheckpointY == 0){
                transform.position = new Vector3 (-38.08f, -40.06f, 0);
            }
            else{
                transform.position = new Vector3 (SaveManager.instance.level7CheckpointX, SaveManager.instance.level7CheckpointY, 0);
            }
        }

        if (activeSceneName == "Level8")
        {
            // add an if statement that checks if the checkpoint values are zero and if they are, correct coordinates in.
            

            if (SaveManager.instance.level8CheckpointX == 0 && SaveManager.instance.level8CheckpointY == 0){
                transform.position = new Vector3 (0, -6f, 0);
            }
            else{
                transform.position = new Vector3 (SaveManager.instance.level8CheckpointX, SaveManager.instance.level8CheckpointY, 0);
            }
        }

        // if (activeSceneName == "Level9")
        // {
        //     transform.position = new Vector3 (SaveManager.instance.testSceneCheckpointX, SaveManager.instance.testSceneCheckpointY, 0);
        // }

        
    }


}
