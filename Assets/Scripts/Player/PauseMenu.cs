using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    public void Pause(InputAction.CallbackContext value) {
        if (value.performed && !this.isPaused){
            this.isPaused = true;
            pauseGame();
        }
        else if (value.performed && this.isPaused){
            this.isPaused = false;
            resumeGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}
