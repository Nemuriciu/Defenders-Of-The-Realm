using System.Collections;
using UnityEngine;

public class HourglassTrigger : MonoBehaviour {
	public GameObject slowPrefab;

	private ArrayList _enemies;

	private void Start() {
		_enemies = new ArrayList();
	}

	// private void OnTriggerEnter(Collider other) {
	// 	if (!other.CompareTag("Enemy")) return;
	// 	
	// 	Damager damager = other.gameObject.GetComponent<Damager>();
	// 	damager.ActivateSlow(slowPrefab, 0.4f);
	// 	_enemies.Add(damager);
	// }
	//
	// private void OnTriggerExit(Collider other) {
	// 	if (!other.CompareTag("Enemy")) return;
	// 	
	// 	Damager damager = other.gameObject.GetComponent<Damager>();
	// 	damager.DeactivateSlow();
	//
	// 	if (_enemies.Contains(damager))
	// 		_enemies.Remove(damager);
	// }
	//
	// public void DeactivateAll() {
	// 	for (int i = _enemies.Count - 1; i >= 0; i--) {
	// 		Damager d = _enemies[i] as Damager;
	// 		
	// 		if (d == null)
	// 			_enemies.RemoveAt(i);
	// 		else if (!d.isDead)
	// 			d.DeactivateSlow();
	// 	}
	// }
}
