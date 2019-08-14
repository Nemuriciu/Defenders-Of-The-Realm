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
    private AudioScript _audioScript;
    private AudioSource _audio;
    private WaveSpawn _waveSpawn;
    private ProgressBar _buildBar;

    private void Start() {
        _boxTransform = phaseBox.rectTransform;
        _audioScript = GetComponent<AudioScript>();
        _audio = GameObject.Find("Canvas").GetComponent<AudioSource>();
        _waveSpawn = GetComponent<WaveSpawn>();
        _buildBar = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();

        if (IntroMusic.Instance) 
            Destroy(IntroMusic.Instance.gameObject);
        
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
                    
                    _buildBar.ChangeValue(Stats.PlayerGold);

                    StartCoroutine(ShowEvent(Phase.Build,
                        "Build defenses and press [B] when ready"));

                    _audio.clip = sounds[0];
                    _audio.Play();
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
                    
                    _audio.clip = sounds[1];
                    _audio.Play();
                    _audioScript.StartCombat();
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
