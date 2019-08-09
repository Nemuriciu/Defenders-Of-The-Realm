using UnityEngine;

public class HourglassTrigger : MonoBehaviour {
	public GameObject slowPrefab;
	
	private void OnTriggerEnter(Collider other) {
		if (!other.CompareTag("Enemy")) return;
		
		Damager damager = other.gameObject.GetComponent<Damager>();
		damager.ActivateSlow(slowPrefab, 0.33f);
	}
	
	private void OnTriggerExit(Collider other) {
		if (!other.CompareTag("Enemy")) return;
		
		Damager damager = other.gameObject.GetComponent<Damager>();
		damager.DeactivateSlow();
	}
}
