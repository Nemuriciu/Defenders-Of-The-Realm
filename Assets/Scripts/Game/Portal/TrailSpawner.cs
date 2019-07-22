using UnityEngine;

public class TrailSpawner : MonoBehaviour {
    public GameObject trailPrefab;
    
    private Transform _parent;
    private float _timer;

    private void Start() {
        _parent = GameObject.Find("Trails").transform;
    }

    private void Update() {
        /* Spawn new trail particle */
        if (_timer <= 0) {
            Instantiate(trailPrefab,transform.position + transform.forward * 6,
                Quaternion.identity, _parent);
            _timer = 2;
        }
        
        _timer -= Time.deltaTime;
    }
}
