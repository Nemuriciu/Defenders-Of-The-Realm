using UnityEngine;

public class HealingAura : MonoBehaviour {
    public ParticleSystem healingFx;

    private CreatureInfo _creatureInfo;
    private float _healTimer, _healTick;


    private void Start() {
        _creatureInfo = GetComponent<CreatureInfo>();
        _healTick = Random.Range(6.5f, 8.5f);
    }

    private void Update() {
        if (!_creatureInfo.IsAlive)
            return;

        _healTimer += Time.deltaTime;

        if (_healTimer >= _healTick) {
            healingFx.Play();
            
            int health = _creatureInfo.GetHealth();
            health += Mathf.RoundToInt(0.2f * _creatureInfo.baseHealth);
            _creatureInfo.SetHealth(health);
            
            _healTimer = 0;
            _healTick = Random.Range(6.5f, 8.5f);
        }
    }
}
