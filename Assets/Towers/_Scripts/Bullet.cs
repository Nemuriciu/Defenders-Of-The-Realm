using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject hitEffect;
    
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
            Instantiate(hitEffect, _target.transform.position, 
                Quaternion.identity, _target.transform);

            CreatureInfo creature = _target.GetComponentInParent<CreatureInfo>();
            if (creature.IsAlive && _tower)
                creature.Hit(_tower.GetDamage(), _tower.GetDmgType());

            Destroy(gameObject);
        }
    }
}
