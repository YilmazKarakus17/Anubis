using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{

    private bool levelCompleted = false;
    [SerializeField] private GameObject victoryScreen;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if play collides with the object then "completed" variable is set to "true" and the user can proceed to the next level.
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            if (victoryScreen!=null){
            victoryScreen.SetActive(true);
            }
            Invoke("CompleteLevel", 1.25f);
            // it loads the next level after 2 seconds.
        }
    }

    private void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level9")
        {
            SceneManager.LoadScene("MainMenu");
            // if on the final level, redirect to the appropriate screen.
        }


        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (SaveManager.instance.levelsComplete < 1) {
                SaveManager.instance.levelsComplete = 1;
            }

            SaveManager.instance.level1CheckpointX = 0;
            SaveManager.instance.level1CheckpointY = 0;
            SaveManager.instance.Save();
            
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (SaveManager.instance.levelsComplete < 2) {
                SaveManager.instance.levelsComplete = 2;
            }
            SaveManager.instance.level2CheckpointX = 0;
            SaveManager.instance.level2CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (SaveManager.instance.levelsComplete < 3) {
                SaveManager.instance.levelsComplete = 3;
            }
            SaveManager.instance.level3CheckpointX = 0;
            SaveManager.instance.level3CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            if (SaveManager.instance.levelsComplete < 4) {
                SaveManager.instance.levelsComplete = 4;
            }
            SaveManager.instance.level4CheckpointX = 0;
            SaveManager.instance.level4CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level4Cinematic")
        {
            if (SaveManager.instance.levelsComplete < 5) {
                SaveManager.instance.levelsComplete = 5;
            }
        }

        if (SceneManager.GetActiveScene().name == "Level5")
        {
            if (SaveManager.instance.levelsComplete < 6) {
                SaveManager.instance.levelsComplete = 6;
            }
            SaveManager.instance.level5CheckpointX = 0;
            SaveManager.instance.level5CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level6")
        {
            if (SaveManager.instance.levelsComplete < 7) {
                SaveManager.instance.levelsComplete = 7;
            }
            SaveManager.instance.level6CheckpointX = 0;
            SaveManager.instance.level6CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level7")
        {
            if (SaveManager.instance.levelsComplete < 8) {
                SaveManager.instance.levelsComplete = 8;
            }
            SaveManager.instance.level7CheckpointX = 0;
            SaveManager.instance.level7CheckpointY = 0;
            SaveManager.instance.Save();
        }

        if (SceneManager.GetActiveScene().name == "Level8")
        {
            if (SaveManager.instance.levelsComplete < 9) {
                SaveManager.instance.levelsComplete = 9;
            }
            SaveManager.instance.level8CheckpointX = 0;
            SaveManager.instance.level8CheckpointY = 0;
            SaveManager.instance.Save();
        }




        SceneManager.LoadScene("LevelSelect");


        
    }

}