using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    /*============ Animate Method ============*/
    public void playJumpAnimation(){ this.changeAnimationState("PlayerJump"); }
    public void playDeathAnimation(){ this.changeAnimationState("PlayerDeath"); }
    public void playRunAnimation(){ this.changeAnimationState("PlayerRun"); }
    public void playFallAnimation(){ this.changeAnimationState("PlayerFall"); }
    public void playIdleAnimation(){ this.changeAnimationState("PlayerIdle"); }


    //Changes the animation
    public void changeAnimationState(string newState){
        // return if there's no state change
        if (this.currentState==newState) return;

        // play the animation
        animator.Play(newState);

        // update the current state
        this.currentState = newState;
    }
    /*============ Special Unity Methods ============*/
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.currentState = "PlayerIdle";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}