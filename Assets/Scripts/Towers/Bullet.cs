using UnityEngine;

public class Bullet : MonoBehaviour {
    [HideInInspector] public Tower twr;
    public GameObject crossbowImpact, crystalImpact;

    private GameObject _target;
    private Damager _damager;
    private bool _alive;
    private float _speed;

    private void Start() {
        _speed = Random.Range(20.0f, 25.0f);
    }

    private void Update () {
        if (!_alive) return;

        Vector3 pos = transform.position;
        
        if (!_target || !twr)
            Destroy(gameObject);
        else {
            pos = _target.transform.position;
            pos.y += 1;
        }

        if (!_target || !twr)
            Destroy(gameObject);
        else
            transform.LookAt(_target.transform);
        
        if (!_target || !twr)
            Destroy(gameObject);
        else
            transform.position = Vector3.MoveTowards(transform.position,
            pos, Time.deltaTime * _speed);
    }
    
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject == _target) {
            if (_target) 
                if (!_damager.isDead && twr) {
                    int damage = Random.Range(twr.damage.min, twr.damage.max);
                    _damager.Hit(damage);
                    Stats.towerDamage += damage;

                    if (twr)
                        switch (twr.type) {
                            case "Crossbow":
                                Instantiate(crossbowImpact, transform.position, Quaternion.identity, transform.parent);
                                break;
                            case "Crystal":
                                Instantiate(crystalImpact, transform.position, Quaternion.identity, transform.parent);
                                break;
                        }
                }

            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject t) {
        _target = t;
        _damager = _target.GetComponent<Damager>();
        _alive = true;
    }
}



