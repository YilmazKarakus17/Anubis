using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    // Constants that represent the animations in the game
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_JUMP = "PlayerInitialJumpCharge";
    const string PLAYER_DEATH = "PlayerDeath";
    const string PLAYER_FALL = "PlayerFall";
    const string PLAYER_HANG = "PlayerHanging";
    const string PLAYER_HURT = "PlayerHurt";

    /*============ Animate Method ============*/
    public void playJumpAnimation(){ this.changeAnimationState(PLAYER_JUMP); }
    public void playDeathAnimation(){ this.changeAnimationState(PLAYER_DEATH); }
    public void playRunAnimation(){ this.changeAnimationState(PLAYER_RUN); }
    public void playFallAnimation(){ this.changeAnimationState(PLAYER_FALL); }
    public void playIdleAnimation(){ this.changeAnimationState(PLAYER_IDLE); }
    public void playHangAnimation(){ this.changeAnimationState(PLAYER_HANG); }
    public void playHurtAnimation(){ this.changeAnimationState(PLAYER_HURT); }

    //Changes the animation
    public void changeAnimationState(string newState){
        // return if there's no state change
        if (this.currentState==newState && newState != PLAYER_FALL) return;
        // return if the animator is told to play something other then hurt if the player is currently hurt
        if (this.gameObject.GetComponent<Player>().isHurt && newState != PLAYER_HURT) return;
        
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