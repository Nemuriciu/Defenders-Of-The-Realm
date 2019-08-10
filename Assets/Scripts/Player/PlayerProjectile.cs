using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
	public float speed;
	public GameObject hitPrefab;
	
	private float _distance;
	private Vector3 _playerForward;
	private Quaternion _playerRotation;
	private Transform _particlesT, _energyT, _trailT;
	private Transform _parent;

	private void Start () {
		_playerForward = GameObject.Find("Player").GetComponent<Transform>().forward;
		_playerRotation = GameObject.Find("Player").GetComponent<Transform>().localRotation;
		_parent = GameObject.Find("Bullets").GetComponent<Transform>();
		
		_energyT = transform.GetChild(0);
		_particlesT = transform.GetChild(1);
		_trailT = transform.GetChild(2);

		_particlesT.rotation = _energyT.rotation = _trailT.rotation = _playerRotation;
		_particlesT.Rotate(new Vector3(1, 0, 0), 180);
	}
	
	private void Update () {
		if (_distance < 20.0f) {
			Vector3 oldPos = transform.position;
			transform.Translate(Time.deltaTime * speed * _playerForward);
			_distance += Vector3.Distance(oldPos, transform.position);
		} else 
			Destroy(gameObject);
	}

	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("PathCollision") ||
		    other.CompareTag("IgnoreCol") ||
		    other.CompareTag("PlayerInteract")) return;
		
		Instantiate (hitPrefab, transform.position, Quaternion.identity, _parent);

		if (other.CompareTag("Enemy")) {
			Damager damager = other.GetComponent<Damager>();
			// TODO: Player Damage value
			if (!damager.isDead) 
				damager.Hit(Random.Range(1, 2));
		}
		
		Destroy(gameObject);
	}
}