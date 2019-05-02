using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {
	public List<Transform> checkpoints;
	private NavMeshAgent navAgent;
	private int current = 0;

	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.SetDestination(checkpoints[current].position);
	}
	
	// Update is called once per frame
	void Update () {
		if (current < checkpoints.Count) {
			if (transform.position.x == checkpoints [current].position.x &&
			    transform.position.z == checkpoints [current].position.z) {
				current++;
				if (current < checkpoints.Count) {
					navAgent.SetDestination (checkpoints [current].position);
					transform.LookAt (checkpoints [current].position);
				}
			}
		}
	}
}
