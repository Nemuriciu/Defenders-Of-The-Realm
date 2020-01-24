using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    private Animator _leftAnim, _rightAnim;
    
    private void Start() {
        _leftAnim = GameObject.Find("Left_hand").GetComponent<Animator>();
        _rightAnim = GameObject.Find("Right_hand").GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _leftAnim.SetTrigger("Shot");
            _rightAnim.SetTrigger("Shot");
        }
    }
}
