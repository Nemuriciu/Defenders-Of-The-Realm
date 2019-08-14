using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Animator anim;
	public float speed;
	public float rotSpeed;
    
    private float _rot;
    private CharacterController _controller;
    private AudioSource _audio;
    private BuildingScript _building;

    private void Start() {
        _controller = GetComponent<CharacterController>();
        _building = GetComponent<BuildingScript>();
        _audio = transform.GetChild(1).GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate () {
        /* Character Movement */
        if (!anim.GetBool("Attack")) {
            if (Input.GetMouseButton(0) && !_building.IsBuilding) {
                SetAttack();
                _audio.Stop();
            } else if (Input.GetKey(KeyCode.W) && (anim.GetBool("MoveFwd") || anim.GetBool("Idle"))) {
                SetMoveFwd();
                _controller.Move(speed * Time.deltaTime * transform.forward);
                if (!_audio.isPlaying) _audio.Play();
            } else if (Input.GetKey(KeyCode.S) && (anim.GetBool("MoveBack") || anim.GetBool("Idle"))) {
                SetMoveBack();
                _controller.Move(speed * Time.deltaTime * -transform.forward);
                if (!_audio.isPlaying) _audio.Play();
            } else if (Input.GetKey(KeyCode.A) && (anim.GetBool("MoveLeft") || anim.GetBool("Idle"))) {
                SetMoveLeft();
                _controller.Move(speed * Time.deltaTime * -transform.right);
                if (!_audio.isPlaying) _audio.Play();
            } else if (Input.GetKey(KeyCode.D) && (anim.GetBool("MoveRight") || anim.GetBool("Idle"))) {
                SetMoveRight();
                _controller.Move(speed * Time.deltaTime * transform.right);
                if (!_audio.isPlaying) _audio.Play();
            } else {
                SetIdle();
                _audio.Stop();
            }
        }

		/* Character Rotation */
		_rot += Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, _rot, 0);
    }
    private void SetMoveFwd() {
        anim.SetBool("MoveFwd", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);        
        anim.SetBool("MoveBack", false);
    }
    private void SetMoveBack() {
        anim.SetBool("MoveBack", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
    }
    private void SetMoveLeft() {
        anim.SetBool("MoveLeft", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);
    }

    private void SetMoveRight() {
        anim.SetBool("MoveRight", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveBack", false);
    }
    private void SetIdle() {
        anim.SetBool("Idle", true);
        
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);

    }
    private void SetAttack() {
        anim.SetBool("Attack", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);
    }
}