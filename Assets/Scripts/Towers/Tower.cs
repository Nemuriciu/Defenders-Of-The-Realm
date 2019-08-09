using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	public string type;
	public string tier;
	public Damage damage;
	public int sellValue;
	public int upgradeValue;
	public float attackSpeed;
	
	[Space(25)]
	public Transform shootElement;
	public Transform pivot;
	public GameObject bullet;
	[HideInInspector] public GameObject target;
	
	
	private bool _shooting;
	private AudioSource _audioSource;
	private Transform _parent;

	private void Start() {
		Collider playerCol = GameObject.FindWithTag("Player").GetComponent<Collider>();
		Collider myCol = GetComponent<Collider>();
		Physics.IgnoreCollision(playerCol, myCol);
		
		_audioSource = GetComponent<AudioSource>();
		_parent = GameObject.Find("Bullets").transform;

		attackSpeed -= Random.Range(0, attackSpeed * 0.1f);
	}

	private void Update () {
		if (!target) return;
		
		if (pivot != null)
			pivot.transform.LookAt(target.transform);

		if (_shooting) return;
		
		_shooting = true;
		StartCoroutine(Shoot());
	}

	private IEnumerator Shoot() {
		while (target) {
			GameObject newBullet = Instantiate(bullet, shootElement.position, Quaternion.identity, _parent);
			Bullet bt = newBullet.GetComponent<Bullet>();
			
			bt.SetTarget(target);
			bt.twr = this;
			//_audioSource.Play();
			yield return new WaitForSeconds(attackSpeed);
		}
		
		_shooting = false;
	}
	
	[System.Serializable]
	public class Damage {
		public int min;
		public int max;
	}
}
