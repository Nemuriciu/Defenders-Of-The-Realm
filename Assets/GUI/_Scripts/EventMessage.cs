using TMPro;
using UnityEngine;

public class EventMessage : MonoBehaviour {
    [HideInInspector] public bool isActive;
    
    private TextMeshProUGUI _text;
    private float _timer = 3.5f;

    private void Start() {
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    private void Update() {
        if (isActive) {
            if (_timer <= 0) {
                _text.text = string.Empty;
                isActive = false;
                return;
            }
            
            _timer -= Time.deltaTime;
        }
    }

    public void Show(string msg) {
        isActive = true;
        _text.text = msg;
        _timer = 3.5f;
    }
}