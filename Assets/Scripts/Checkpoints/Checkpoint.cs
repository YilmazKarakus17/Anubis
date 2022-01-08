using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int difficultyLevel = 2;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "TestScene")
            {
                SaveManager.instance.testSceneCheckpointX = transform.position.x;
                SaveManager.instance.testSceneCheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SaveManager.instance.level1CheckpointX = transform.position.x;
                SaveManager.instance.level1CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level2")
            {
                SaveManager.instance.level2CheckpointX = transform.position.x;
                SaveManager.instance.level2CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level3")
            {
                SaveManager.instance.level3CheckpointX = transform.position.x;
                SaveManager.instance.level3CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level4")
            {
                SaveManager.instance.level4CheckpointX = transform.position.x;
                SaveManager.instance.level4CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level5")
            {
                SaveManager.instance.level5CheckpointX = transform.position.x;
                SaveManager.instance.level5CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level6")
            {
                SaveManager.instance.level6CheckpointX = transform.position.x;
                SaveManager.instance.level6CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level7")
            {
                SaveManager.instance.level7CheckpointX = transform.position.x;
                SaveManager.instance.level7CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }

            if (SceneManager.GetActiveScene().name == "Level8")
            {
                SaveManager.instance.level8CheckpointX = transform.position.x;
                SaveManager.instance.level8CheckpointY = transform.position.y;
                SaveManager.instance.Save();
            }
        

        }
    }

    void possibleDestoryGameObject(float x, float y){
        int currentDifficulty = SaveManager.instance.currentDifficulty;

        //Is this checkpoint the same checkpoint that the player most recently visited
        if (this.gameObject.transform.position.x != x && this.gameObject.transform.position.y != y){
            //Is the current checkpoint for a easier difficulty than the current difficulty
            if (this.difficultyLevel < currentDifficulty){
                Destroy(gameObject);
            }
        }
    }

    private void Start(){
        // sets the spawn location
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "Level1")
        {
            this.possibleDestoryGameObject(SaveManager.instance.level1CheckpointX, SaveManager.instance.level1CheckpointY);
        }
        else if (activeSceneName == "Level3")
        {
            this.possibleDestoryGameObject(SaveManager.instance.level3CheckpointX, SaveManager.instance.level3CheckpointY);
        }
        else if (activeSceneName == "Level5")
        {
            this.possibleDestoryGameObject(SaveManager.instance.level5CheckpointX, SaveManager.instance.level5CheckpointY);
        }
        else if (activeSceneName == "Level7")
        {
            this.possibleDestoryGameObject(SaveManager.instance.level7CheckpointX, SaveManager.instance.level7CheckpointY);
        }
        else if (activeSceneName == "Level8")
        {
            this.possibleDestoryGameObject(SaveManager.instance.level8CheckpointX, SaveManager.instance.level8CheckpointY);
        }

    }
}

