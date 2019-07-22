using UnityEngine;
using UnityEngine.AI;

public class TrailNavigation : MonoBehaviour {
    private NavMeshAgent _navAgent;
    private GameObject _checkpoint;
    private float _timer = 3;
    
    private void Start () {
        _navAgent = GetComponent<NavMeshAgent> ();
        _checkpoint = GameObject.Find("Artefact");
		
        _navAgent.SetDestination(_checkpoint.transform.position);
    }

    private void Update() {
        if (_timer <= 0) {
            Destroy(gameObject);
            return;
        }

        if (_navAgent.isStopped) {
            _timer -= Time.deltaTime;
            return;
        }

        float dist = Vector3.Distance(_checkpoint.transform.position, transform.position);

        if (dist <= 4) {
            _navAgent.speed = 0;
            _navAgent.velocity = Vector3.zero;
            _navAgent.isStopped = true;
        }
    }
}
