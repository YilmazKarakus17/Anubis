using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private int activeMessage = 0;

    public GameObject dialogueBox;

    public static bool isActive=false;

    public void openDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;

        isActive=true;

        dialogueBox.SetActive(true);

        // Debug.Log("Started conversation! Loaded messages: " +messages.Length);
        Time.timeScale = 0f;
        displayMessage();
        
    }

    void displayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImage.sprite =actorToDisplay.sprite;
    }

    public void nextMessage()
    {
        activeMessage++;
        if (activeMessage<currentMessages.Length)
        {
            displayMessage();
        }
        else{
            // Debug.Log("Conversation ended!");
            isActive=false;
            Time.timeScale = 1f;
            dialogueBox.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void continueToNextMsg(InputAction.CallbackContext value)
    {
        Debug.Log("you are pressing the button");
        Debug.Log("isActive status: " + isActive);

        if (value.performed && isActive && !(PauseMenu.isPaused))
        {
            nextMessage();
        }
        
    }
}
