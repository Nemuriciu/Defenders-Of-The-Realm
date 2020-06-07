using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CreatureInfo : MonoBehaviour {
    [Header("Stats")] 
    public string cName;
    public string affinity;
    public int baseHealth;
    [Space(10)]

    public GameObject slowFx;
    public bool IsAlive { get; private set; } = true;

    private int _health;
    private int _slowCounter;
    private float _baseSpeed;
    private float _baseSpeedModif = 1.0f, _speedModifier = 1.0f;
    private float _goldRatio;
    
    private Vector3[] _wp;
    private int _index;
    private float _dist;
    private NavMeshAgent _navAgent;
    
    private Healthbar _healthbar;
    private Camera _cam;
    private Animator _anim;
    private GameSystem _system;
    private GoldInfo _goldInfo;
    private bool _isDecaying;

    private void Start() {
        _anim = GetComponent<Animator>();
        _system = GameObject.Find("System").GetComponent<GameSystem>();
        _goldInfo = GameObject.Find("GoldGroup").GetComponent<GoldInfo>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        /* Set speed */
        _baseSpeed = _navAgent.speed;
        _speedModifier += Random.Range(-0.05f, 0.05f);
        _baseSpeedModif = _speedModifier;
        _anim.SetFloat("Speed", _speedModifier);
        
        /* Set health */
        //baseHealth = Mathf.RoundToInt(baseHealth * RNG.GetHealthModifier(cName));
        _health = baseHealth;
        
        /* Set gold ratio */
        _goldRatio = RNG.GoldRatio(cName);
        
        /* Randomize waypoints */
        for (int i = 0; i < _wp.Length; i++) {
            _wp[i] += new Vector3(
                Random.Range(-0.5f, 0.5f), 
                0, 
                Random.Range(-0.5f, 0.5f));
        }
        
        /* Set navAgent speed & destination */
        if (_navAgent.enabled) {
            _navAgent.updateRotation = false;
            _navAgent.speed = _baseSpeed * _speedModifier;
            _navAgent.SetDestination(_wp[_index]);
        }
    }
    
    private void Update() {
        if (!IsAlive) {
            if (_isDecaying) return;
            
            StartCoroutine(Decay());
            _isDecaying = true;
        }
        
        /* Check distance until next waypoint */
        _dist = Vector3.Distance(transform.position, _navAgent.destination);
        if (_dist < 0.5f) {
            _index++;
            
            _navAgent.SetDestination(_wp[_index]);
        }

        /* Smooth rotation */
        Vector3 pos = new Vector3(_wp[_index].x, transform.position.y, _wp[_index].z);
        Quaternion rot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 
            10.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Crystal")) {
            CrystalInteract crystal = other.gameObject.GetComponent<CrystalInteract>();
            crystal.Hit(Random.Range(15, 25));
            
            /* TODO: Crystal Damage based on mob type / wave nr / mob hp */
            Kill();
        }
    }

    private IEnumerator Decay() {                
        yield return new WaitUntil(() =>
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        
        yield return new WaitForSeconds(1);

        while (transform.position.y >= 1.7f) {
            transform.Translate(Vector3.down * (Time.deltaTime * 0.1f));
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        Destroy(gameObject);
    }
    
    private void Kill() {
        IsAlive = false;

        tag = "Dead";
        foreach (Transform t in transform)
            t.gameObject.tag = "Dead";
        
        _navAgent.enabled = false;
        _anim.SetBool("Death", true);
        _healthbar.RemoveHealthbar();

        if (slowFx.activeSelf)
            slowFx.SetActive(false);

        _system.creatureNr--;
        _system.UpdateMobCount();
    }
    
    public void Hit(int damage, string type) {
        /* Reduce damage by 75% if resistant to that type */
        if (type.Equals(affinity))
            damage = Mathf.RoundToInt(damage * 0.25f);
        
        _health = (_health - damage <= 0) ? 0 : _health - damage;
        
        /* Kill creature if health reaches zero */
        if (_health <= 0) {
            //TODO: Calculate new gold drop 
            int value = Mathf.RoundToInt(baseHealth * _goldRatio);
            _goldInfo.MobChangeValue(value);
            Debug.Log(value);
            Kill();
        }
    }

    public void SetWaypoint(Vector3[] wp) {
        _wp = new Vector3[wp.Length];

        for (int i = 0; i < _wp.Length; i++)
            _wp[i] = wp[i];
    }
    
    public void SetHealthbar(Healthbar healthbar) {
        _healthbar = healthbar;
    }

    public void EnableSlow() {
        _slowCounter++;

        if (_slowCounter == 1) {
            /* Slow creature by 25% */
            _speedModifier *= 0.75f;
            _navAgent.speed = _baseSpeed * _speedModifier;
            _anim.SetFloat("Speed", _speedModifier);
            slowFx.SetActive(true);
        }
    }

    public void DisableSlow() {
        _slowCounter = _slowCounter == 0 ? 0 : _slowCounter - 1;

        if (_slowCounter == 0) {
            /* Reset to base speed */
            _speedModifier = _baseSpeedModif;
            _navAgent.speed = _baseSpeed * _speedModifier;
            _anim.SetFloat("Speed", _speedModifier);
            slowFx.SetActive(false);
        }
    }
    
    public int GetHealth() {
        return _health;
    }
}
