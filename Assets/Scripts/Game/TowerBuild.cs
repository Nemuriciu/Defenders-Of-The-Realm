using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuild : MonoBehaviour {
	public GameObject player;
	public GameObject ctower_prefab;
	public GameObject mtower_prefab;
	public GameObject mtower;
	public Material mtower_mat;

	public bool building;
	private bool valid;
	private GameObject current;
	private Vector3 mouse_pos;
	private float dist;

	private GameBehaviour gb;

	void Start () {
		gb = GameObject.Find("ScriptHandler").GetComponent<GameBehaviour>();
	}

	void Update () {
		//TODO: Check currency before building
		if (Input.GetKeyDown(KeyCode.Alpha1) && !building) {
			building = true;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				mouse_pos = hit.point;
			}

			current = Instantiate (mtower_prefab, mouse_pos, 
				Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0), GameObject.Find ("Towers").transform);
		}

		if (Input.GetKeyUp(KeyCode.Escape) && building) {
			Destroy (current);
			building = false;
		}

		if (building) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if (hit.point != mouse_pos) {
					mouse_pos = hit.point;
					current.transform.position = mouse_pos;
					current.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
				}
			}

			dist = Vector3.Distance (player.transform.position,current.transform.position);

			if (dist > 4.0f || current.transform.position.y >= 0.1f) {
				mtower_mat.color = Color.red;
				valid = false;
			} else {
				mtower_mat.color = Color.green;
				valid = true;
			}
		}

		if (building && valid) {
			if (Input.GetMouseButtonDown (0) && gb.gold >= 100) {
				Instantiate (mtower, mouse_pos, Quaternion.Euler (Vector3.zero), GameObject.Find ("Towers").transform);
				Destroy (current);
				mouse_pos = Vector3.zero;
				valid = building = false;

				gb.gold -= 100;
			} else if (Input.GetMouseButtonDown (0) && gb.gold < 100) {
				gb.SendError ("Insufficient gold!");
				Destroy (current);
				mouse_pos = Vector3.zero;
				valid = building = false;
			}
		}
	}
}
