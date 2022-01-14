using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume",1);

        }
        else{
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ResetProgress()
    {
        SaveManager.instance.watchedIntro = 0;

        SaveManager.instance.currentDifficulty = 0;
        SaveManager.instance.playerHealth = 0;
        SaveManager.instance.playerStamina = 0;

        SaveManager.instance.playerCoins = 0;
        SaveManager.instance.playerSouls = 0;

        SaveManager.instance.levelsComplete = 0;

        SaveManager.instance.testSceneCheckpointX = 0;
        SaveManager.instance.testSceneCheckpointY = 0;

        SaveManager.instance.level1CheckpointX = 0;
        SaveManager.instance.level1CheckpointY = 0;

        SaveManager.instance.level2CheckpointX = 0;
        SaveManager.instance.level2CheckpointY = 0;
        
        SaveManager.instance.level3CheckpointX = 0;
        SaveManager.instance.level3CheckpointY = 0;
        
        SaveManager.instance.level4CheckpointX = 0;
        SaveManager.instance.level4CheckpointY = 0;

        SaveManager.instance.level5CheckpointX = 0;
        SaveManager.instance.level5CheckpointY = 0;
        
        SaveManager.instance.level6CheckpointX = 0;
        SaveManager.instance.level6CheckpointY = 0;

        SaveManager.instance.level7CheckpointX = 0;
        SaveManager.instance.level7CheckpointY = 0;

        SaveManager.instance.level8CheckpointX = 0;
        SaveManager.instance.level8CheckpointY = 0;

        SaveManager.instance.level9CheckpointX = 0;
        SaveManager.instance.level9CheckpointY = 0;

        SaveManager.instance.Save();
    }

}
