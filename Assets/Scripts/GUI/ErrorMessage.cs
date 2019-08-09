using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour {
    private TextMeshProUGUI _text;
    private float _timer = 2.5f;
    private bool _isActive;

    private void Start() {
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    private void Update() {
        if (_isActive) {
            if (_timer <= 0) {
                _text.text = string.Empty;
                _isActive = false;
                return;
            }
            
            _timer -= Time.deltaTime;
        }
    }

    public void Show(string msg) {
        _text.text = msg;
        _timer = 2.5f;
        _isActive = true;
    }
}