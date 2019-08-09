using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseScript : MonoBehaviour {
    public TrailSpawner[] trailSpawners;
    public List<AudioClip> sounds;
    public TextMeshProUGUI phaseBox;
    public EventMessage eventMsg;
    
    public enum Phase {
        Begin,
        Build,
        Wave,
        Loading
    };
    
    private Phase _phase;
    private bool _scaleDir, _startedWave;
    private RectTransform _boxTransform;
    private AudioSource _audioSource;
    private WaveSpawn _waveSpawn;
    
    

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _boxTransform = phaseBox.rectTransform;
        _waveSpawn = gameObject.GetComponent<WaveSpawn>();

        _phase = Phase.Begin;
        phaseBox.text = "Press [B] to start build phase";
    }

    private void Update() {
        if (_startedWave) return;
        
        switch (_phase) {
            case Phase.Loading:
                return;
            
            case Phase.Begin:
                AnimateBox();
                
                if (Input.GetKey(KeyCode.B)) {
                    _phase = Phase.Loading;
                    eventMsg.Show("Build Phase!");

                    foreach (var trailSpawner in trailSpawners) {
                        if (trailSpawner.gameObject.activeSelf)
                            trailSpawner.enabled = true;
                    }

                    StartCoroutine(ShowEvent(Phase.Build,
                        "Build defenses and press [B] when ready"));
                    
                    _audioSource.clip = sounds[0];
                    //_audioSource.Play();
                }
                break;
            
            case Phase.Build:
                AnimateBox();
                
                if (Input.GetKey(KeyCode.B)) {
                    _phase = Phase.Loading;
                    eventMsg.Show("Wave 1");
                    
                    foreach (var trailSpawner in trailSpawners) {
                        if (trailSpawner.gameObject.activeSelf)
                            trailSpawner.enabled = false;
                    }
                    
                    StartCoroutine(ShowEvent(Phase.Wave, string.Empty));
                    
                    // Audio.Play();
                }
                break;
            
            case Phase.Wave:
                _startedWave = true;
                _waveSpawn.Begin();
                break;
        }
    }

    private IEnumerator ShowEvent(Phase phase, string msg) {
        phaseBox.text = string.Empty;
        
        yield return new WaitUntil(() => !eventMsg.isActive);

        _phase = phase;
        phaseBox.text = msg;
    }

    private void AnimateBox() {
        if (_boxTransform.localScale.x >= 1.0f)
            _scaleDir = true;
        else if (_boxTransform.localScale.x < 0.8f)
            _scaleDir = false;

        if (!_scaleDir)
            _boxTransform.localScale = new Vector3(_boxTransform.localScale.x + 0.25f * Time.deltaTime,
                _boxTransform.localScale.y + 0.25f * Time.deltaTime,
                _boxTransform.localScale.z + 0.25f * Time.deltaTime);
        else
            _boxTransform.localScale = new Vector3(_boxTransform.localScale.x - 0.25f * Time.deltaTime,
                _boxTransform.localScale.y - 0.25f * Time.deltaTime,
                _boxTransform.localScale.z - 0.25f * Time.deltaTime);
    }

    public Phase GetPhase() {
        return _phase;
    }
}
