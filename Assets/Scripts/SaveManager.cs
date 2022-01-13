using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance { get; private set; }

    public int watchedIntro;

    public int currentDifficulty;
    public float playerHealth;
    public float playerStamina;

    public float playerCoins;
    public float playerSouls;

    public int levelsComplete;

    public float testSceneCheckpointX;
    public float testSceneCheckpointY;

    public float level1CheckpointX;
    public float level1CheckpointY;

    public float level2CheckpointX;
    public float level2CheckpointY;
    
    public float level3CheckpointX;
    public float level3CheckpointY;
    
    public float level4CheckpointX;
    public float level4CheckpointY;

    public float level5CheckpointX;
    public float level5CheckpointY;
    
    public float level6CheckpointX;
    public float level6CheckpointY;

    public float level7CheckpointX;
    public float level7CheckpointY;

    public float level8CheckpointX;
    public float level8CheckpointY;

    public float level9CheckpointX;
    public float level9CheckpointY;



    

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            watchedIntro = data.watchedIntro;

            currentDifficulty = data.currentDifficulty;
            playerHealth = data.playerHealth;
            playerStamina = data.playerStamina;

            playerCoins = data.playerCoins;
            playerSouls = data.playerSouls;

            levelsComplete = data.levelsComplete;

            testSceneCheckpointX = data.testSceneCheckpointX;
            testSceneCheckpointY = data.testSceneCheckpointY;

            level1CheckpointX = data.level1CheckpointX;
            level1CheckpointY = data.level1CheckpointY;

            level2CheckpointX = data.level2CheckpointX;
            level2CheckpointY = data.level2CheckpointY;
            
            level3CheckpointX = data.level3CheckpointX;
            level3CheckpointY = data.level3CheckpointY;
            
            level4CheckpointX = data.level4CheckpointX;
            level4CheckpointY = data.level4CheckpointY;

            level5CheckpointX = data.level5CheckpointX;
            level5CheckpointY = data.level5CheckpointY;
            
            level6CheckpointX = data.level6CheckpointX;
            level6CheckpointY = data.level6CheckpointY;

            level7CheckpointX = data.level7CheckpointX;
            level7CheckpointY = data.level7CheckpointY;

            level8CheckpointX = data.level8CheckpointX;
            level8CheckpointY = data.level8CheckpointY;

            level9CheckpointX = data.level9CheckpointX;
            level9CheckpointY = data.level9CheckpointY;

            file.Close();
        }
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.watchedIntro = watchedIntro;

        data.currentDifficulty = currentDifficulty;
        data.playerHealth = playerHealth;
        data.playerStamina = playerStamina;

        data.playerCoins = playerCoins;
        data.playerSouls = playerSouls;

        data.levelsComplete = levelsComplete;

        data.testSceneCheckpointX = testSceneCheckpointX;
        data.testSceneCheckpointY = testSceneCheckpointY;

        data.level1CheckpointX = level1CheckpointX;
        data.level1CheckpointY = level1CheckpointY;

        data.level2CheckpointX = level2CheckpointX;
        data.level2CheckpointY = level2CheckpointY;
        
        data.level3CheckpointX = level3CheckpointX;
        data.level3CheckpointY = level3CheckpointY;
        
        data.level4CheckpointX = level4CheckpointX;
        data.level4CheckpointY = level4CheckpointY;

        data.level5CheckpointX = level5CheckpointX;
        data.level5CheckpointY = level5CheckpointY;

        data.level6CheckpointX = level6CheckpointX;
        data.level6CheckpointY = level6CheckpointY;

        data.level7CheckpointX = level7CheckpointX;
        data.level7CheckpointY = level7CheckpointY;

        data.level8CheckpointX = level8CheckpointX;
        data.level8CheckpointY = level8CheckpointY;

        data.level9CheckpointX = level9CheckpointX;
        data.level9CheckpointY = level9CheckpointY;

        

        bf.Serialize(file, data);
        file.Close();
    }
    
}


[Serializable]
class PlayerData_Storage
{
    public int watchedIntro;

    public int currentDifficulty;
    public float playerHealth;
    public float playerStamina;

    public float playerCoins;
    public float playerSouls;

    public int levelsComplete;

    public float testSceneCheckpointX;
    public float testSceneCheckpointY;

    public float level1CheckpointX;
    public float level1CheckpointY;

    public float level2CheckpointX;
    public float level2CheckpointY;
    
    public float level3CheckpointX;
    public float level3CheckpointY;
    
    public float level4CheckpointX;
    public float level4CheckpointY;

    public float level5CheckpointX;
    public float level5CheckpointY;
    
    public float level6CheckpointX;
    public float level6CheckpointY;

    public float level7CheckpointX;
    public float level7CheckpointY;

    public float level8CheckpointX;
    public float level8CheckpointY;

    public float level9CheckpointX;
    public float level9CheckpointY;
    
}