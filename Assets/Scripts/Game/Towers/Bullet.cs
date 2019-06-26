using UnityEngine;

public class Bullet : MonoBehaviour {
    private GameObject _target;
    private Damager _damager;

    private void Update () {
        if (!_target) return;
        
        Vector3 pos = _target.transform.position;
        pos.y += 1;
        
        transform.LookAt(_target.transform);
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 20);
    }
    
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject == _target) {
            if (!_damager.isDead)
                _damager.Hit(Random.Range(5, 10));
            
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject t) {
        _target = t;
        _damager = _target.GetComponent<Damager>();
    }
}
