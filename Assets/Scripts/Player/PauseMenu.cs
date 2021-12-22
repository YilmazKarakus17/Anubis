using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    public void Pause(InputAction.CallbackContext value) {
        Debug.Log("Boogaloo");
        Debug.Log(isPaused);
        if (value.performed && (isPaused==false)){
            pauseGame();
        }
        else if (value.performed && (isPaused==true)){
            resumeGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    
    public void resumeGame()
    {
        isPaused = false;
        if (!DialogueManager.isActive)
        {
            Time.timeScale = 1f;
        }
        pauseMenu.SetActive(false);

        
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

}
