using UnityEngine;
using Random = UnityEngine.Random;

public class ArtefactScript : MonoBehaviour {
    private ProgressBar _artefactBar;
    private EventMessage _eventMessage;
    private bool _em1, _em2, _em3;

    private void Start() {
        _artefactBar = GameObject.Find("ArtefactBar").GetComponentInChildren<ProgressBar>();
        _eventMessage = GameObject.Find("EventBox").GetComponent<EventMessage>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            /* TODO: dmg calculated based on enemy type */
            int damage = Random.Range(200, 250);

            Stats.ArtefactHealth -= damage;
            _artefactBar.ChangeValue(-damage);
            
            Damager damager = other.gameObject.GetComponent<Damager>();
            damager.Kill();

            float percent = (float) Stats.ArtefactHealth / Stats.MaxArtefact * 100.0f;

            if (percent <= 25.0f && !_em3) {
                _eventMessage.Show("Artefact's health is below 25% !");
                _em3 = true;
            } else if(percent <= 50.0f && !_em2) {
                _eventMessage.Show("Artefact's health is below 50% !");
                _em2 = true;
            } else if (percent <= 75.0f && !_em1) {
                _eventMessage.Show("Artefact's health is below 75% !");
                _em1 = true;
            }
        }
    }
}
