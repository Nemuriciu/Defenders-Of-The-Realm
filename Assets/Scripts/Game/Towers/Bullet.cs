using UnityEngine;

public class Bullet : MonoBehaviour {
    private Transform _target;
    
    private void Update () {
        if (!_target) return;
        
        var pos = _target.position;
        pos.y += 1;
        
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 20);
    }
    
    private void OnTriggerEnter (Collider other) {
        if(other.gameObject.transform == _target)
            Destroy(gameObject);
    }

    public void SetTarget(Transform t) {
        _target = t;
    }
}
