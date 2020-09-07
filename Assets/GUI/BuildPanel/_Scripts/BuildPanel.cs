using UnityEngine;

public class BuildPanel : MonoBehaviour {
    public Spot SpotInstance { get; set; }
    public bool IsActive { get; private set; }

    private CanvasGroup _panel;
    private Camera _cam;
    private Spot _pos;
    private GameObject _blockingPanel;
    private AudioSource _audio;
    
    private void Start() {
        _cam = Camera.main;
        _panel = GetComponent<CanvasGroup>();
        _audio = GetComponent<AudioSource>();
        _blockingPanel = transform.GetChild(5).gameObject;
    }

    private void Update() {
        if (!SpotInstance) return; 
        
        transform.position = _cam.WorldToScreenPoint(SpotInstance.SpawnPos);
    }

    public void OpenPanel() {
        _panel.alpha = 1;
        _panel.blocksRaycasts = true;
        IsActive = true;
        _audio.Play();
    }

    public void ClosePanel() {
        _blockingPanel.SetActive(true);
        _panel.alpha = 0;
        _panel.blocksRaycasts = false;
        IsActive = false;
    }

    public void CloseClickBlocker() {
        _blockingPanel.SetActive(false);
    }
}
