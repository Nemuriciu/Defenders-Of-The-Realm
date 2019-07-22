using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {
	private NavMeshAgent _navAgent;
	private GameObject _checkpoint;
	private Damager _damager;
	private bool _stop;

	private void Start () {
		_navAgent = GetComponent<NavMeshAgent> ();
		_damager = GetComponent<Damager>();
		_checkpoint = GameObject.Find("Artefact");
		
		_navAgent.SetDestination(_checkpoint.transform.position);
	}

	private void Update() {
		if (_stop) return;

		if (_damager.isDead) {
			_navAgent.enabled = false;
			_stop = true;
		}
	}
}
