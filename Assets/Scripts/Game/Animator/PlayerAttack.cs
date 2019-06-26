using UnityEngine;

public class PlayerAttack : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
    }
}
