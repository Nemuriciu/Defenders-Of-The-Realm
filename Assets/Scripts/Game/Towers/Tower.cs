using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	public float delay;
	public Transform shootElement;
	public Transform lookAtObj;
	public GameObject bullet;
	public Transform target;
	
	
	private bool _shooting;
	private AudioSource _audioSource;

	private void Start() {
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update () {
		if (!target) return;
		
		lookAtObj.transform.LookAt(target);

		if (_shooting) return;
		
		_shooting = true;
		StartCoroutine(Shoot());
	}

	private IEnumerator Shoot() {
		while (target) {
			var newBullet = Instantiate(bullet, shootElement.position, Quaternion.identity);
			var bt = newBullet.GetComponent<Bullet>();
			
			bt.SetTarget(target);
			_audioSource.Play();
			yield return new WaitForSeconds(delay);
		}
		
		_shooting = false;
	}
}
