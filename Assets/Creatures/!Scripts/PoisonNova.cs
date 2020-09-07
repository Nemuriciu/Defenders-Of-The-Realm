using UnityEngine;

public class PoisonNova : MonoBehaviour {
    public ParticleSystem poisonFx;
    public float radius;

    private CreatureInfo _creatureInfo;
    private float _tick;
    private Vector3 _lastPos;
    private float _distance;

    private void Start() {
        _creatureInfo = GetComponent<CreatureInfo>();
        _lastPos = transform.position;
        _tick = Random.Range(8.5f, 12.5f);
    }

    private void Update() {
        if (!_creatureInfo.IsAlive)
            return;
        
        _distance += Vector3.Distance(transform.position, _lastPos);
        _lastPos = transform.position;
        
        if (_distance >= _tick) {
            poisonFx.Play();

            HitTowers();
            _distance = 0;
        }
    }

    private void HitTowers() {
        foreach (Collider hit in Physics.OverlapSphere(transform.position, radius, 1 << 2)) {
            if (!hit.isTrigger) continue;
            
            Tower twr = hit.transform.GetComponent<Tower>();
            /* Hit for 2% of max hp */
            twr.Hit(twr.baseHealth * 0.02f);
        }
    }
}
