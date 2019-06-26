using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public int maxValue;
    public bool isFull;
    
    private Slider _slider;
    private Text _displayText;
    private int _currentVal;

    private void Start () {
        _slider = GetComponent<Slider>();
        _displayText = GetComponentInChildren<Text>();

        if (isFull)
            _currentVal = maxValue;
        
        Display();
    }
    
    private void Display() {
        _displayText.text = _currentVal + " / " + maxValue;
        _slider.value = (float) _currentVal / maxValue; 
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
