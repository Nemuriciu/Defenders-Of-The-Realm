﻿using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject hitEffect;
    public bool isAoe;
    
    private GameObject _target;
    private Vector3 _targetPos;
    private Tower _tower;
    private bool Go {  get; set;  }

    private const float Speed = 25f;
    
    private void Update() {
        if (Go) {
            if (_target) {
                _targetPos = _target.transform.position;

                transform.position = Vector3.MoveTowards(transform.position,
                    _targetPos, Time.deltaTime * Speed);
                transform.LookAt(_target.transform);
                
                return;
            }
            
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target, Tower t) {
        _target = target;
        _targetPos = _target.transform.position;
        _tower = t;
        
        Go = true;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (_target & ReferenceEquals(_target, other.gameObject)) {
            /* Hit all enemies in a radius near the target */
            if (isAoe) {
                var hitColliders = Physics.OverlapSphere(
                    transform.position, 3.0f, LayerMask.GetMask("Enemy"));

                foreach (var hitCollider in hitColliders) {
                    GameObject enemy = hitCollider.gameObject;
                    if (enemy.CompareTag("Dead") || enemy == _target) continue;

                    CreatureInfo c = enemy.GetComponentInParent<CreatureInfo>();
                    if (c.IsAlive && _tower) {
                        Instantiate(hitEffect, enemy.transform.position, 
                            Quaternion.identity, enemy.transform);
                        c.Hit(_tower.GetDamage(), _tower.GetDmgType());
                    }
                }
            }
            
            /* Hit the target */
            CreatureInfo creature = _target.GetComponentInParent<CreatureInfo>();
            if (creature.IsAlive && _tower) {
                Instantiate(hitEffect, _target.transform.position, 
                    Quaternion.identity, _target.transform);
                creature.Hit(_tower.GetDamage(), _tower.GetDmgType());
            }

            Destroy(gameObject);
        }
    }
}