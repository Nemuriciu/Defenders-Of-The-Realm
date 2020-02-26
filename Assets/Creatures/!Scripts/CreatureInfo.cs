using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CreatureInfo : MonoBehaviour {
    [Header("Stats")] 
    public string cName;
    public string affinity;
    public int baseHealth;
    public float goldRation;
    [Space(10)]

    public SkinnedMeshRenderer[] meshes;
    public Material[] replace;
    public GameObject slowFx;
    public bool IsAlive { get; private set; } = true;

    private int _health;
    private int _slowCounter;
    private float _baseSpeed;
    private float _baseSpeedModif = 1.0f, _speedModifier = 1.0f;
    
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
        _cam = Camera.main;
        _anim = GetComponent<Animator>();
        _system = GameObject.Find("System").GetComponent<GameSystem>();
        _goldInfo = GameObject.Find("GoldInfo").GetComponent<GoldInfo>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        /* Set speed */
        _baseSpeed = _navAgent.speed;
        //_speedModifier += Random.Range(-0.025f, 0.025f);
        _speedModifier = RNG.GetSpeedModifier(cName);
        _baseSpeedModif = _speedModifier;
        _anim.SetFloat("Speed", _speedModifier);
        
        /* Set health */
        _health = Mathf.RoundToInt(baseHealth * RNG.GetHealthModifier(cName));
        
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
            CrystalInteract script = other.gameObject.GetComponent<CrystalInteract>();
            script.Hit(Random.Range(15, 25));
            Kill();
        }
    }

    private IEnumerator Decay() {
        if (replace.Length > 0)
            for (int i = 0; i < replace.Length; i++)
                meshes[i].material = replace[i];
                
        yield return new WaitUntil(() =>
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        
        yield return new WaitForSeconds(2.5f);

        while (meshes[0].material.color.a > 0) {
            foreach (var mesh in meshes) {
                Color color = mesh.material.color;
                color.a -= Time.deltaTime * 1.0f;
                mesh.material.color = color;
            }
            
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
        _anim.SetInteger("Death", Random.Range(1, 3));
        _healthbar.RemoveHealthbar();

        if (slowFx.activeSelf)
            slowFx.SetActive(false);

        _system.creatureNr--;
    }
    
    public void Hit(int damage, string type) {
        /* Show healthbar when first hit */
        if (!_healthbar.isHit) {
            _healthbar.isHit = true;
            _healthbar.drawOffDistance = true;
        }
        
        /* Reduce damage by 90% if resistant to that type */
        if (type.Equals(affinity))
            damage = Mathf.RoundToInt(damage * 0.1f);

        _health = (_health - damage <= 0) ? 0 : _health - damage;
        
        /* Draw floating hit damage text */
        float dist = Vector3.Distance(transform.position, _cam.transform.position);
        if (dist < 20) {
            FloatingText.instance.InitializeScriptableText(type.Equals("Physical") ? 0 : 1,
                transform.position + new Vector3(
                    Random.Range(-0.25f, 0.25f),
                    Random.Range(_healthbar.yOffset + 0.1f, _healthbar.yOffset + 0.25f),
                    Random.Range(-0.25f, 0.25f)),
                damage.ToString());
        }

        /* Kill creature if health reaches zero */
        if (_health <= 0) {
            int value = Mathf.RoundToInt(baseHealth * goldRation);
            // Debug.Log("Gold: " + value);
            _goldInfo.ChangeValue(value);
            Kill();
        }
    }

    public void SetWaypoints(Vector3[] wp) {
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
            /* Slow creature by 30% */
            _speedModifier *= 0.7f;
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
