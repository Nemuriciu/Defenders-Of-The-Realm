using UnityEngine;
using System.Collections;

public class TowerTrigger : MonoBehaviour {
	public Tower tower;
	
	private GameObject _curTarget;
	private Damager _damager;
	//private ArrayList _targets;
	private Queue _targets;

	private void Start() {
		_targets = new Queue();
	}

	private void OnTriggerEnter(Collider other) {
		if (!other.CompareTag("Enemy")) return;
		
		_targets.Enqueue(other.gameObject);
	}

	private void Update() {
		if (_targets.Count <= 0) return;
		
		if (_curTarget) {
			if (_damager.isDead) {
				_targets.Dequeue();
				_curTarget = null;
				_damager = null;
				tower.target = null;
			}
			
			return;
		}
		
		while (_targets.Count > 0)
			if (_targets.Peek() == null)
				_targets.Dequeue();
			else break;

		if (_targets.Count <= 0) return;
		
		_curTarget = _targets.Peek() as GameObject;
		
		if (_curTarget != null) {
			tower.target = _curTarget;
			_damager = _curTarget.GetComponent<Damager>();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (!other.CompareTag("Enemy")) return;
		
		if (!_targets.Contains(other.gameObject)) return;
		
		_targets.Dequeue();

		if (other.gameObject != _curTarget) return;
		
		_curTarget = null;
		_damager = null;
		tower.target = null;
	}
}
