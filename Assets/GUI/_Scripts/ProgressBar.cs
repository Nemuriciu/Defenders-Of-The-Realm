using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public Sprite[] fillers;

    private float _currentVal, _maxVal;
    private int _percent;
    private Image _slider;
    private TextMeshProUGUI _text;
    private EventMessage _eventMessage;
    private GameSystem _system;
    private enum Color {
        Yellow,
        Orange,
        Red
    }

    private void Start () {
        _slider = GetComponent<Image>();
        _text = transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        _system = GameObject.Find("System").GetComponent<GameSystem>();
        
        _maxVal = _currentVal = 250;
        _percent = 100;
        Display();
    }

    private void CheckColor() {
        if (_percent <= 75 && _percent > 50) {
            if (_slider.sprite != fillers[(int) Color.Yellow])
                _slider.sprite = fillers[(int) Color.Yellow];
        }
        else if (_percent <= 50 && _percent > 25) {
            if (_slider.sprite != fillers[(int) Color.Orange])
                _slider.sprite = fillers[(int) Color.Orange];
        }
        else if (_percent <= 25) {
            if (_slider.sprite != fillers[(int) Color.Red])
                _slider.sprite = fillers[(int) Color.Red];
        }
    }
    
    private void Display() {
        _slider.fillAmount = _percent / 100.0f;
        _text.text = _percent + "%";
    }
    
    public void ChangeValue(float value) {
        _currentVal = (_currentVal - value <= 0) ? 0 : _currentVal - value;
        _percent = Mathf.RoundToInt(_currentVal / _maxVal * 100.0f);
        
        if (_currentVal <= 0) 
            _system.Defeat();
        
        CheckColor();
        Display();
    }
}
