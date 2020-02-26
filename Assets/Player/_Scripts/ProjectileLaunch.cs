using UnityEngine;

public class ProjectileLaunch : StateMachineBehaviour {
    private GameObject _projectile;
    private bool _launched;
    private Transform _parent;
    private Transform _ts;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.name.Equals("Left_hand")) return;
        
        _parent = GameObject.Find("Bullets").GetComponent<Transform>();
        _ts = GameObject.Find("ProjectilePos").transform;
        _projectile = GameObject.Find("Player").GetComponent<PlayerInfo>().projectile;
        _launched = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.name.Equals("Left_hand")) return;
        
        if (stateInfo.normalizedTime > 0.65f && !_launched) {
            Camera cam = Camera.main;

            if (cam != null) {
                Instantiate(_projectile, _ts.position, cam.transform.rotation, _parent);
                _launched = true;
            }
        }
    }
}
