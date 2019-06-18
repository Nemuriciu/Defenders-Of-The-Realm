using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {
	private NavMeshAgent _navAgent;
	private GameObject _checkpoint;

	// Use this for initialization
	private void Start () {
		_navAgent = GetComponent<NavMeshAgent> ();
		_checkpoint = GameObject.Find("Checkpoint");
		
		_navAgent.SetDestination(_checkpoint.transform.position);
	}
}
