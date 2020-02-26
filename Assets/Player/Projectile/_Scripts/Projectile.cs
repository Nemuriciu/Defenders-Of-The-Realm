using UnityEngine;

public class Projectile : MonoBehaviour {
    public string type;
    private Vector3 _forward;
    private const float Speed = 35;
    private float _dist;

    private void Start() {
        Camera c = Camera.main;

        if (c != null)
            _forward = c.transform.forward;
    }
    
    private void Update() {
        _dist += Vector3.Distance(transform.position, 
            transform.position + _forward * (Speed * Time.deltaTime));
        
        transform.position += _forward * (Speed * Time.deltaTime);

        if (_dist > 30)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            var hitColliders = Physics.OverlapSphere(
                transform.position, 2.0f, LayerMask.GetMask("Enemy"));

            foreach (var hitCollider in hitColliders) {
                GameObject enemy = hitCollider.gameObject;
                if (enemy.CompareTag("Dead")) continue;

                CreatureInfo c = enemy.GetComponentInParent<CreatureInfo>();
                c.Hit(RNG.GetPlayerDamage(), type);
            }
            
            Destroy(gameObject);
        }
    }
}
