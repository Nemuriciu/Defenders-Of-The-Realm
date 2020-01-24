using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CreatureInfo : MonoBehaviour {
    public SkinnedMeshRenderer[] meshes;
    public Material[] replace;
    public bool IsAlive { get; private set; } = true;
    
    private int _health;
    private float _speedModifier = 1.0f;

    private Vector3[] _wp;
    private int _index;
    private float _dist;
    private NavMeshAgent _navAgent;
    
    private Healthbar _healthbar;
    private Camera _cam;
    private Animator _anim;
    private bool _isDecaying;

    private void Start() {
        _cam = Camera.main;
        _anim = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        _health = 500;
        _speedModifier += Random.Range(-0.05f, 0.05f);
        _anim.SetFloat("Speed", _speedModifier);

        /* Randomize waypoints */
        for (int i = 0; i < _wp.Length; i++) {
            _wp[i] += new Vector3(
                Random.Range(-0.5f, 0.5f), 
                0, 
                Random.Range(-0.5f, 0.5f));
        }
        
        if (_navAgent.enabled) {
            _navAgent.updateRotation = false;
            _navAgent.speed *= _speedModifier;
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

            // if (_index >= _wp.Length) {
            //     Kill();
            //     return;
            // }
            
            _navAgent.SetDestination(_wp[_index]);
        }

        /* Smooth rotation */
        Vector3 pos = new Vector3(_wp[_index].x, transform.position.y, _wp[_index].z);
        Quaternion rot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 
            10.0f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.gameObject.name);
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
        _navAgent.enabled = false;
        _anim.SetInteger("Death", Random.Range(1, 3));
        _healthbar.RemoveHealthbar();
        
        // _waveScript.enemyCount--;
        // _waveScript.Display();
        // _audio.Play();
    }
    
    public void Hit(int damage, string type) {
        /* Show healthbar when first hit */
        if (!_healthbar.isHit) {
            _healthbar.isHit = true;
            _healthbar.drawOffDistance = true;
        }

        _health = (_health - damage <= 0) ? 0 : _health - damage;
        
        /* Draw floating hit damage text */
        float dist = Vector3.Distance(transform.position, _cam.transform.position);
        if (dist < 20) {
            FloatingText.instance.InitializeScriptableText(Random.Range(0, 2),
                transform.position + new Vector3(
                    Random.Range(-0.25f, 0.25f),
                    Random.Range(_healthbar.yOffset + 0.1f, _healthbar.yOffset + 0.25f),
                    Random.Range(-0.25f, 0.25f)),
                damage.ToString());
        }

        /* Kill creature if health reaches zero */
        if (_health <= 0) Kill();
    }

    public void SetWaypoints(Vector3[] wp) {
        _wp = new Vector3[wp.Length];

        for (int i = 0; i < _wp.Length; i++)
            _wp[i] = wp[i];
    }
    
    public void SetHealthbar(Healthbar healthbar) {
        _healthbar = healthbar;
    }
    
    public int GetHealth() {
        return _health;
    }
}
