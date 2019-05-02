using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
	public GameObject[] mobs;
	public Vector3 portal1_pos;
	public Vector3 portal2_pos;
	public Vector3[] portal1_path;
	public Vector3[] portal2_path;

	public int wave_nr = 1;
	public int monster_count;

	void Start () {
		
	}
	void Update () {
	}

	IEnumerator SpawnMobs() {
		while (monster_count > 0) {
			Instantiate (mobs [1], portal1_pos, Quaternion.Euler (0, 180, 0), GameObject.Find ("Mobs").transform);
			monster_count--;
			yield return new WaitForSeconds (Random.Range(2.0f, 5.0f));
		}
	}

	public void SpawnWave(int mobs_count) {
		monster_count = mobs_count;
		StartCoroutine ("SpawnMobs");
	}
}
