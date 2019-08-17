using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtefactScript : MonoBehaviour {
    private ProgressBar _artefactBar;
    private EventMessage _eventMessage;
    private AudioSource _audio;
    private bool _em1, _em2, _em3;

    private void Start() {
        _artefactBar = GameObject.Find("ArtefactBar").GetComponentInChildren<ProgressBar>();
        _eventMessage = GameObject.Find("EventBox").GetComponent<EventMessage>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update() {
        /* Defeat */
        if (Stats.ArtefactHealth <= 0) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("DefeatScene");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Damager damager = other.gameObject.GetComponent<Damager>();
            int damage = Mathf.RoundToInt(damager.maxHealth / 10.0f);

            Stats.ArtefactHealth -= damage;
            _artefactBar.ChangeValue(-damage);
            damager.Kill();

            float percent = (float) Stats.ArtefactHealth / Stats.MaxArtefact * 100.0f;

            if (percent <= 25.0f && !_em3) {
                _eventMessage.Show("Artefact's health is below 25% !");
                _audio.Play();
                _em3 = true;
            } else if(percent <= 50.0f && !_em2) {
                _eventMessage.Show("Artefact's health is below 50% !");
                _audio.Play();
                _em2 = true;
            } else if (percent <= 75.0f && !_em1) {
                _eventMessage.Show("Artefact's health is below 75% !");
                _audio.Play();
                _em1 = true;
            }
        }
    }
}
