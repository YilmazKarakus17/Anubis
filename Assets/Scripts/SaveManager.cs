using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager instance { get; private set; }

    public int currentDifficulty;
    public float playerHealth;
    public float playerStamina;
    public float playerSouls;

    public float testSceneCheckpointX;
    public float testSceneCheckpointY;

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

            currentDifficulty = data.currentDifficulty;
            playerHealth = data.playerHealth;
            playerStamina = data.playerStamina;
            playerSouls = data.playerSouls;
            testSceneCheckpointX = data.testSceneCheckpointX;
            testSceneCheckpointY = data.testSceneCheckpointY;

            file.Close();
        }
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.currentDifficulty = currentDifficulty;
        data.playerHealth = playerHealth;
        data.playerStamina = playerStamina;
        data.playerSouls = playerSouls;
        data.testSceneCheckpointX = testSceneCheckpointX;
        data.testSceneCheckpointY = testSceneCheckpointY;

        bf.Serialize(file, data);
        file.Close();
    }
    
}


[Serializable]
class PlayerData_Storage
{
    public int currentDifficulty;
    public float playerHealth;
    public float playerStamina;
    public float playerSouls;
    public float testSceneCheckpointX;
    public float testSceneCheckpointY;
}