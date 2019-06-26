using UnityEngine;

public class CameraScript : MonoBehaviour {
	public float distanceAway, distanceUp;
	public float smooth;
	public Transform player;

	private Vector3 _targetPos;

	private void FixedUpdate() {
		_targetPos = player.position + player.up * distanceUp - player.forward * distanceAway;

		Debug.DrawRay (player.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay (player.position, -1f * distanceAway * player.forward, Color.blue);
		Debug.DrawRay (player.position, _targetPos, Color.magenta);

		transform.position = Vector3.Lerp (transform.position, _targetPos, Time.deltaTime * smooth);
		transform.LookAt (player);
	}
}

