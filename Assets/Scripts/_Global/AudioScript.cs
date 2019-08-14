using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip[] combatClips;
    
    private AudioSource _audio;
    private int _index;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!_audio.isPlaying && !_audio.loop) {
            _audio.clip = combatClips[_index];
            _audio.Play();
            _index = (_index + 1 == combatClips.Length) ? 1 : _index + 1;
        }
    }

    public void StartCombat() {
        _audio.clip = combatClips[0];
        _audio.loop = false;
        _audio.Play();
        _index++;
    }
}
