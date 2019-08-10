using UnityEngine;

public class PlayerAttack : StateMachineBehaviour {
    public GameObject projectile;
    
    private Vector3 _startPos; 
    private Vector3 _point;
    private bool _launched;
    private Transform _parent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _parent = GameObject.Find("Bullets").GetComponent<Transform>();
        _launched = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > 0.65f && !_launched) {
            _startPos = GameObject.Find("Edge").transform.position;
            
            Instantiate(projectile, _startPos, Quaternion.identity, _parent);
            _launched = true;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
    }
}
