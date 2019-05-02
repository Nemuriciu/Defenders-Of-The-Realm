using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator anim;

	public float speed;
	public float rotSpeed = 60;
	private float x;

	void Update () {
		/* Character Movement */
		//TODO: Left & Right
		if (Input.GetKey(KeyCode.W)) {
			anim.SetBool ("Moving", true);
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.S)) {
			anim.SetBool ("Moving", true);
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		} else {
			anim.SetBool ("Moving", false);
		}

		/* Character Rotation */
		x += Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, x, 0);
	}
}