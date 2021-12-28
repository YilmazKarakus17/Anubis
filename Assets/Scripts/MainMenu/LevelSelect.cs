using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public Button [] levelsBtns; 

    public int levelsDone;


    // Start is called before the first frame update
    void Start()
    {
        levelsDone = SaveManager.instance.levelsComplete;

        for (int i = 0; i< 10; i++){
            if (i>levelsDone){
                levelsBtns[i].interactable = false;
            }
            
        }
        
    }

    public void startLevel1()
    {

        if (SaveManager.instance.playerHealth == 0)
        {
            SaveManager.instance.playerHealth = 200;
        }
        if (SaveManager.instance.playerStamina == 0)
        {
            SaveManager.instance.playerStamina = 100;
        }
        
        SceneManager.LoadScene("Level1");
    }

    public void startLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void startLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void startLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void startLevel5()
    {
        SceneManager.LoadScene("Level4Cinematic");
    }

    public void startLevel6()
    {
        SceneManager.LoadScene("Level5");
    }

    public void startLevel7()
    {
        SceneManager.LoadScene("Level6");
    }

    public void startLevel8()
    {
        SceneManager.LoadScene("Level7");
    }

    public void startLevel9()
    {
        SceneManager.LoadScene("Level8");
    }

    public void startLevel10()
    {
        SceneManager.LoadScene("Level9");
    }

}
