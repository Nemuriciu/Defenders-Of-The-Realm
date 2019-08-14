using UnityEngine;
using UnityEngine.AI;

public class Damager : MonoBehaviour {
	public int health, maxHealth;
	public float speed;
	public bool isDead;

	private Camera _cam;
	private AudioSource _audio;
	private Animator _anim;
	private Healthbar _healthbar;
	private BoxCollider _collider;
	private const float FallSpeed = 0.5f;
	private WaveCount _waveScript;
	private ProgressBar _buildBar;

	private NavMeshAgent _nav;
	private float _initSpeed;
	private GameObject _slowInstance;
	private int _slowCount;

	private void Start() {
		_anim = GetComponent<Animator>();
		_cam = Camera.main;
		_audio = GetComponent<AudioSource>();
		_waveScript = GameObject.Find("WaveCount").GetComponent<WaveCount>();
		_buildBar = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();
		_collider = GetComponent<BoxCollider>();
		_nav = GetComponent<NavMeshAgent>();
	}

	private void Update () {
		if (isDead) {
			if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
				if (transform.position.y < 0)
					Destroy(gameObject);
				else
					transform.Translate(Time.deltaTime * FallSpeed * Vector3.down);
			}

			if (_slowCount > 0)
				Destroy(_slowInstance);
			
		} else if (health <= 0) {
	        _anim.SetTrigger("Dead");
			_healthbar.RemoveHealthbar();
			_collider.enabled = false;
	        isDead = true;
	        tag = "Untagged";
	        Stats.monstersKilled++;
	        _waveScript.enemyCount--;
	        _waveScript.Display();
	        _audio.Play();
	        
	        /* Drop Gold */
	        int value = Mathf.RoundToInt(maxHealth / 30.0f);
	        Debug.Log(value);
	        Stats.PlayerGold += value;
	        Stats.goldReceived += value;
	        _buildBar.ChangeValue(value);
		} else if (speed > 0 && _initSpeed <= 0)
			_initSpeed = _nav.speed = speed;
	}

	public void Kill() {
		_anim.SetTrigger("Dead");
		_healthbar.RemoveHealthbar();
		_collider.enabled = false;
		isDead = true;
		_waveScript.enemyCount--;
		_waveScript.Display();
		_audio.Play();
	}
	
	public void Hit(int damage) {
		health -= damage;

		float dist = Vector3.Distance(transform.position, _cam.transform.position);
		
		if (dist < 50)
			FloatingText.instance.InitializeScriptableText(0, 
				transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 2, 0), damage.ToString());
	}

	public void ActivateSlow(GameObject particle, float slowPercent) {
		if (_slowCount == 0) {
			_slowInstance = Instantiate(particle, transform);
			_nav.speed *= 1 - slowPercent;
		}

		_slowCount++;
	}
	
	public void DeactivateSlow() {
		_slowCount--;
		
		if (_slowCount == 0) {
			Destroy(_slowInstance);
			_nav.speed = _initSpeed;
		}
	}

	

	public void SetHealthbar(Healthbar hb) {
		_healthbar = hb;
	}
}