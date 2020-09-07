using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public bool slowTower;
    public GameObject bulletPrefab;
    public Transform bulletT;
    public Transform pivot;
    public GameObject upgradePrefab;

    [Header("Stats")] 
    public string tName;
    public string type;
    public float atkSpeed;
    public float atkSpeedModif;
    public int damageMin, damageMax;
    public int buildVal, sellVal, repairVal;
    public float baseHealth, health;
    public string info;
    [Space(10)]

    private List<GameObject> _enemies;
    private GameObject _target;
    private bool _isAttacking;
    private Transform _bulletParent;
    private float _hitTimer;
    private CreatureInfo _targetInfo;
    private TowerHealth _towerHealthbar;
    private bool _destroyed;
    
    private AudioSource _audio;

    private void Start() {
        _enemies = new List<GameObject>();
        _bulletParent = GameObject.Find("Bullets").transform;
        _towerHealthbar = GetComponent<TowerHealth>();
        _audio = GetComponent<AudioSource>();
        
        /* Random AtkSpeedModif */
        atkSpeedModif += Random.Range(-0.05f, 0.05f);
        _hitTimer = atkSpeed * atkSpeedModif;
    }

    private void Update() {
        if (!slowTower) {
            _hitTimer += Time.deltaTime;
            
            /* Check if tower had a target */
            if (_target) {
                if (_targetInfo.IsAlive) {
                    if (pivot)
                        pivot.LookAt(_target.transform);

                    /* Shoot bullet */
                    if (_hitTimer >= atkSpeed * atkSpeedModif) {
                        GameObject bullet = Instantiate(bulletPrefab, bulletT.position,
                            Quaternion.identity, _bulletParent);
                        Bullet b = bullet.GetComponent<Bullet>();
                        b.SetTarget(_target, this);
                        _audio.Play();

                        _hitTimer = 0;
                    }
                    
                    _enemies.RemoveAll(obj => obj.CompareTag("Dead"));
                    return;
                }
                
                _enemies.Remove(_target);
                _target = null;
                _targetInfo = null;
                _enemies.RemoveAll(obj => obj.CompareTag("Dead"));

                if (_enemies.Count > 0) {
                    _target = _enemies[0];
                    _targetInfo = _target.GetComponentInParent<CreatureInfo>();
                }
            }
            else {
                _enemies.RemoveAll(obj => obj.CompareTag("Dead"));

                if (_enemies.Count > 0) {
                    _target = _enemies[0];
                    _targetInfo = _target.GetComponentInParent<CreatureInfo>();
                }
            }
        }
        else {
            _enemies.RemoveAll(obj => obj.CompareTag("Dead"));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            _enemies.Add(other.gameObject);

            if (slowTower) {
                CreatureInfo c = other.gameObject.GetComponentInParent<CreatureInfo>();
                c.EnableSlow();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (!slowTower) {
                if (_enemies.Contains(other.gameObject)) {
                    if (ReferenceEquals(other.gameObject, _target)) {
                        _target = null;
                        _targetInfo = null;
                    }

                    _enemies.Remove(other.gameObject);
                }
            }
            else {
                if (_enemies.Contains(other.gameObject))
                    _enemies.Remove(other.gameObject);
                
                CreatureInfo c = other.gameObject.GetComponentInParent<CreatureInfo>();
                c.DisableSlow();
            }
        }
    }
    
    private void DestroyTower() {
        Spot spot = GetComponentInParent<Spot>();
        spot.RemoveTower();
    }
    
    public void Hit(float damage) {
        if (_destroyed) return;
        
        if (health - damage <= 0) {
            _destroyed = true;
            DestroyTower();
            return;
        }

        health -= damage;
    }
    
    public void RemoveSlows() {
        foreach (var enemy in _enemies) {
            CreatureInfo c = enemy.GetComponentInParent<CreatureInfo>();
            c.DisableSlow();
        }
    }

    public void Repair() {
        health = baseHealth;
    }

    public void RemoveHealthbar() {
        _towerHealthbar.FreeHealthbar();
    }
    
    public int GetDamage() {
        return Random.Range(damageMin, damageMax);
    }

    public float GetHealth() {
        return health;
    }

    public string GetDmgType() {
        return type;
    }
}
