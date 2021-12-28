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
        else 
        {
            // otherwise, load the next scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}