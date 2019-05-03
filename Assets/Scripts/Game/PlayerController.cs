using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator anim;
	public float speed;
	public float rotSpeed;
    private float x;
    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate () {

        /* Character Movement */
        if (!anim.GetBool("Attack")) {
            if (Input.GetMouseButton(0)) {
                SetAttack();
                //TODO: Attack projectile
            } else if(Input.GetKey(KeyCode.W) && (anim.GetBool("MoveFwd") || anim.GetBool("Idle"))) {
                SetMoveFwd();
                controller.Move(transform.forward * speed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.S) && (anim.GetBool("MoveBack") || anim.GetBool("Idle"))) {
                SetMoveBack();
                controller.Move(-transform.forward * speed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.A) && (anim.GetBool("MoveLeft") || anim.GetBool("Idle"))) {
                SetMoveLeft();
                controller.Move(-transform.right * speed * Time.deltaTime);
            } else if (Input.GetKey(KeyCode.D) && (anim.GetBool("MoveRight") || anim.GetBool("Idle"))) {
                SetMoveRight();
                controller.Move(transform.right * speed * Time.deltaTime);
            } else if (Input.GetMouseButton(0)) {
                SetAttack();
                //TODO: Attack projectile
            } else {
                SetIdle();
            }
        }

		/* Character Rotation */
		x += Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, x, 0);
	}

    void SetMoveFwd() {
        anim.SetBool("MoveFwd", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);        
        anim.SetBool("MoveBack", false);
    }
    void SetMoveBack() {
        anim.SetBool("MoveBack", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
    }
    void SetMoveLeft() {
        anim.SetBool("MoveLeft", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);
    }
    void SetMoveRight() {
        anim.SetBool("MoveRight", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveBack", false);
    }
    void SetIdle() {
        anim.SetBool("Idle", true);
        
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);

    }
    void SetAttack() {
        anim.SetBool("Attack", true);

        anim.SetBool("Idle", false);
        anim.SetBool("MoveFwd", false);
        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveBack", false);
    }
}