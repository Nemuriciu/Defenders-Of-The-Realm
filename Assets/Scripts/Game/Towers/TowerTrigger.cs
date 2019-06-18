using System.Collections;
using UnityEngine;

public class TowerTrigger : MonoBehaviour {
	public Tower tower;
	
	private GameObject _curTarget;
	private ArrayList _targets;

	private void Start() {
		_targets = new ArrayList();
	}

	private void OnTriggerEnter(Collider other) {
		if (!other.CompareTag("Enemy")) return;

		_targets.Add(other.gameObject);
	}

	private void Update() {
		if (_curTarget || _targets.Count <= 0) return;
		
		while (_targets.Count > 0 & _targets[0] == null)
			_targets.Remove(_targets[0]);

		if (_targets.Count <= 0) return;
		
		_curTarget = _targets[0] as GameObject;
		
		if (_curTarget != null) 
			tower.target = _curTarget.transform;
	}

	private void OnTriggerExit(Collider other) {
		if (!other.CompareTag("Enemy")) return;
		
		_targets.Remove(_targets[0]);

		if (other.gameObject != _curTarget) return;
		
		_curTarget = null;
		tower.target = null;
	}
}
