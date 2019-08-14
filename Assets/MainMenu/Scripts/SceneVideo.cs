using UnityEngine;
using UnityEngine.Video;

public class SceneVideo : MonoBehaviour {
    public GameObject[] gui;
    public AudioSource music;

    private VideoPlayer _videoPlayer;
    private bool _playing;

    private void Start() {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update() {
        if (_playing) return;

        if (_videoPlayer.isPlaying) {
            foreach (GameObject o in gui)
                o.SetActive(true);
        
            music.Play();
            _playing = true;
        }
    }
}
