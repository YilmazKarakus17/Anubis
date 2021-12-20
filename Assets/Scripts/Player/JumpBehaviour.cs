using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerActionManager player = PlayerActionManager.instance;
        PlayerMovement movement = PlayerMovement.instance;
        if (player.isAttacking) {
            movement.setInCombatStatus(true);
            player.animator.Play("PlayerAttackOne");
            player.ApplyDamage(0, 0, 0.2f);
        }
        if (player.isShockwaveActive) {
            movement.setInCombatStatus(true);
            player.ApplyShockwave(0, 0, 0.2f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerActionManager.instance.isAttacking = false;
        PlayerActionManager.instance.isShockwaveActive = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
