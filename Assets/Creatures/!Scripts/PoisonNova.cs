using UnityEngine;

public class PoisonNova : MonoBehaviour {
    public ParticleSystem poisonFx;

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
            
            //TODO: Deal damage to towers
            _distance = 0;
        }
    }
}
