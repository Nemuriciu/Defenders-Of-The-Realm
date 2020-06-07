using UnityEngine;
using UnityEngine.Video;

public class SceneVideo : MonoBehaviour {
    public GameObject title;
    public CanvasGroup mainPanel;
    public AudioSource music;

    private VideoPlayer _videoPlayer;
    private bool _playing;

    private void Start() {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update() {
        if (_playing) return;

        if (_videoPlayer.isPlaying) {
            title.SetActive(true);
            mainPanel.alpha = 1;
            mainPanel.interactable = true;
            mainPanel.blocksRaycasts = true;
        
            music.Play();
            _playing = true;
        }
    }
}
