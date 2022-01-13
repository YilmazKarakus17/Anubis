using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadLevel1());
    }

    //Coroutine for the waiting to play out the death of Flying Eye
    IEnumerator LoadLevel1() {
        yield return new WaitForSeconds(33.3f);
        SceneManager.LoadScene("Level1");
    }
}
