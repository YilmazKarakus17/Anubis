using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionThreeBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // USED TO IMPLEMENT THE FOURTH ATTACK OF THE COMBO CHAIN...
        // if (PlayerActionManager.instance.isAttacking) {
        //     PlayerActionManager.instance.animator.Play("PlayerAttackFour");
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerActionManager.instance.isAttacking = false;
        PlayerActionManager.instance.isShockwaveActive = false;
        PlayerActionManager.instance.applyDoubleDamage = false;
        if (PlayerMovement.instance.getAllowedToHorizontallyMove() && (PlayerMovement.instance.getHorizontalMovementInput() != 0) && !PlayerActionManager.instance.isAttacking) {
            PlayerActionManager.instance.animator.Play("PlayerRun");
        }
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
