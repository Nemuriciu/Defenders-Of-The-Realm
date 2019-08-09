using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultySelection : MonoBehaviour, IPointerClickHandler {
    private Image _image;
    private TextMeshProUGUI _text;
    private AudioSource _audio;
    
    private string _difficulty;
    private bool _flag;

    private void Start() {
        _audio = GetComponent<AudioSource>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        
        _difficulty = _text.text.Trim();

        Info.selectedDifficulty = null;
        Info.isPanelOpen = false;
    }

    private void Update() {
        if (_flag)
            if (Info.selectedDifficulty != _difficulty) {
                _image.color = new Color32(50, 50, 50, 250);
                _flag = false;
                return;
            }

        if (!_flag)
            if (Info.selectedDifficulty == _difficulty) {
                _image.color = new Color32(0, 150, 250, 250);
                _flag = true;
            }
    }


    public void OnPointerClick(PointerEventData eventData) {
        Info.selectedDifficulty = _difficulty;
        _audio.Play();
    }
}
public static class Info {
    public static string selectedDifficulty;
    public static bool isPanelOpen;
}
