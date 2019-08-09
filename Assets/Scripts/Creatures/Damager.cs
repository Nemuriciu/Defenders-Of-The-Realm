using UnityEngine;
using UnityEngine.AI;

public class Damager : MonoBehaviour {
	public int health, maxHealth;
	public bool isDead;
	public GameObject soulPrefab;

	private Camera _cam;
	private Animator _anim;
	private Healthbar _healthbar;
	private const float FallSpeed = 0.5f;
	private WaveCount _waveScript;
	
	private NavMeshAgent _nav;
	private float _initSpeed;
	private GameObject _slowInstance;
	public int SlowCount { get; private set; }

	private void Start() {
		_anim = GetComponent<Animator>();
		_cam = Camera.main;
		_waveScript = GameObject.Find("WaveCount").GetComponent<WaveCount>();
		_nav = GetComponent<NavMeshAgent>();
		_initSpeed = _nav.speed;
	}

	private void Update () {
		if (isDead) {
			if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
				if (transform.position.y < 0)
					Destroy(gameObject);
				else
					transform.Translate(Time.deltaTime * FallSpeed * Vector3.down);
			}

			if (SlowCount > 0)
				Destroy(_slowInstance);
			
		} else if (health <= 0) {
	        _anim.SetTrigger("Dead");
			_healthbar.RemoveHealthbar();
	        isDead = true;
	        tag = "Untagged";
	        _waveScript.enemyCount--;
	        _waveScript.Display();

	        GameObject soul = Instantiate(soulPrefab, 
		        transform.position + new Vector3(0, Random.Range(0.25f, 0.4f), 0),
		        Quaternion.identity, GameObject.Find("Drops").transform);
	        SoulScript soulScript = soul.GetComponent<SoulScript>();
	        
	        int value = Mathf.RoundToInt(maxHealth / 55.0f);
	        Debug.Log(value);
	        soulScript.SetValue(value);
		}
	}

	public void Kill() {
		_anim.SetTrigger("Dead");
		_healthbar.RemoveHealthbar();
		isDead = true;
		_waveScript.enemyCount--;
		_waveScript.Display();
	}
	
	public void Hit(int damage) {
		health -= damage;

		float dist = Vector3.Distance(transform.position, _cam.transform.position);
		
		if (dist < 50)
			FloatingText.instance.InitializeScriptableText(0, 
				transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 2, 0), damage.ToString());
	}

	public void ActivateSlow(GameObject particle, float slowPercent) {
		if (SlowCount == 0) {
			_slowInstance = Instantiate(particle, transform);
			_nav.speed *= 1 - slowPercent;
		}

		SlowCount++;
	}
	
	public void DeactivateSlow() {
		SlowCount--;
		
		if (SlowCount == 0) {
			Destroy(_slowInstance);
			_nav.speed = _initSpeed;
		}
	}

	

	public void SetHealthbar(Healthbar hb) {
		_healthbar = hb;
	}
}