using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{

    public Button easyBtn;
    public Button normalBtn;
    public Button hardBtn;

    public int currentDifficulty;

    // Start is called before the first frame update
    private void Start()
    {  
        currentDifficulty = SaveManager.instance.currentDifficulty;

        if (currentDifficulty==0)
        {
            easyBtn.interactable = false;
            normalBtn.interactable = true;
            hardBtn.interactable = true;
        }

        if (currentDifficulty==1)
        {
            easyBtn.interactable = true;
            normalBtn.interactable = false;
            hardBtn.interactable = true;
        }

        if (currentDifficulty==2)
        {
            easyBtn.interactable = true;
            normalBtn.interactable = true;
            hardBtn.interactable = false;
        }
        
        
    }


    public void easyMode()
    {
        SaveManager.instance.currentDifficulty = 0;
        SaveManager.instance.playerHealth = 200;
        SaveManager.instance.playerStamina = 100;
        SaveManager.instance.playerSouls = 0;

        SaveManager.instance.Save();

        if (SaveManager.instance.currentDifficulty==0)
        {
            easyBtn.interactable = false;
            normalBtn.interactable = true;
            hardBtn.interactable = true;
        }
    }
    public void normalMode()
    {
        SaveManager.instance.currentDifficulty = 1;
        SaveManager.instance.playerHealth = 150;
        SaveManager.instance.playerStamina = 75;
        SaveManager.instance.playerSouls = 0;
        SaveManager.instance.Save();
        if (SaveManager.instance.currentDifficulty==1)
        {
            easyBtn.interactable = true;
            normalBtn.interactable = false;
            hardBtn.interactable = true;
        }
    }
    public void hardMode()
    {
        SaveManager.instance.currentDifficulty = 2;
        SaveManager.instance.playerHealth = 100;
        SaveManager.instance.playerStamina = 50;
        SaveManager.instance.playerSouls = 0;
        SaveManager.instance.Save();
        if (SaveManager.instance.currentDifficulty==2)
        {
            easyBtn.interactable = true;
            normalBtn.interactable = true;
            hardBtn.interactable = false;
        }
    }
}
