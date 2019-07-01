using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public int maxValue;
    public bool isFull;
    
    private Image _slider;
    private TextMeshProUGUI _displayText;
    private int _currentVal;

    private void Start () {
        _slider = GetComponent<Image>();
        _displayText = transform.parent.GetComponentInChildren<TextMeshProUGUI>();

        if (isFull)
            _currentVal = maxValue;
        
        Display();
    }
    
    private void Display() {
        _displayText.text = _currentVal + " / " + maxValue;
        _slider.fillAmount = (float) _currentVal / maxValue; 
    }
    
    public void ChangeValue(int value) {
        _currentVal += value;

        if (_currentVal <= 0)
            _currentVal = 0;
        else if (_currentVal >= maxValue)
            _currentVal = maxValue;
        
        Display(); 
    }
}
