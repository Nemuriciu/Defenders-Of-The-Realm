using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public float distanceAway, distanceUp;
	public float smooth;
	public Transform player;

	private Vector3 targetPos;

	void LateUpdate() {
		targetPos = player.position + player.up * distanceUp - player.forward * distanceAway;

		Debug.DrawRay (player.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay (player.position, -1f * player.forward * distanceAway, Color.blue);
		Debug.DrawRay (player.position, targetPos, Color.magenta);

		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * smooth);
		transform.LookAt (player);
	}
}

