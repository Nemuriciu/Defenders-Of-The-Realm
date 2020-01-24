using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletT;
    public Transform pivot;

    [Header("Stats")] 
    public string type;
    public float atkSpeed = 1;
    public float atkSpeedModif = 1;
    public int damageMin, damageMax;
    [Space(10)]

    private List<GameObject> _enemies;
    private GameObject _target;
    private bool _isAttacking;
    private Transform _bulletParent;
    private float _hitTimer;
    private CreatureInfo _targetInfo;

    private Highlight _outline;

    private void Start() {
        _enemies = new List<GameObject>();
        _bulletParent = GameObject.Find("Bullets").transform;
        _outline = GetComponent<Highlight>();
        _hitTimer = atkSpeed * atkSpeedModif;
    }


    private void Update() {
        _hitTimer += Time.deltaTime;
        
        /* Check if tower had a target */
        if (_target) {
            if (_targetInfo.IsAlive) {
                pivot.LookAt(_target.transform);

                if (_hitTimer >= atkSpeed * atkSpeedModif) {
                    GameObject bullet = Instantiate(bulletPrefab, bulletT.position,
                        Quaternion.identity, _bulletParent);
                    Bullet b = bullet.GetComponent<Bullet>();
                    b.SetTarget(_target, this);

                    _hitTimer = 0;
                }

                return;
            }
            
            _enemies.Remove(_target);
            _target = null;
            _targetInfo = null;
            _enemies.RemoveAll(obj => obj is null);

            if (_enemies.Count > 0) {
                _target = _enemies[0];
                _targetInfo = _target.GetComponentInParent<CreatureInfo>();
                // Debug.Log(_target.tag);
            }
        }
        else {
            _enemies.RemoveAll(obj => obj is null);

            if (_enemies.Count > 0) {
                _target = _enemies[0];
                _targetInfo = _target.GetComponentInParent<CreatureInfo>();
                // Debug.Log(_target.tag);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
            _enemies.Add(other.gameObject);
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy"))
            if (_enemies.Contains(other.gameObject)) {
                if (ReferenceEquals(other.gameObject, _target)) {
                    _target = null;
                    _targetInfo = null;
                }

                _enemies.Remove(other.gameObject);
            }
    }

    public void SetOutline(bool b) {
        _outline.enabled = b;
    }
    
    public int GetDamage() {
        return Random.Range(damageMin, damageMax);
    }

    public string GetDmgType() {
        return type;
    }
}
