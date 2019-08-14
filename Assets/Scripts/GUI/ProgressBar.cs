using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public bool percent;
    
    private int _maxValue;
    private Image _slider;
    private TextMeshProUGUI _displayText;
    private int _currentVal;

    private void Start () {
        _slider = GetComponent<Image>();
        _displayText = transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();

        switch (transform.parent.name) {
            case "BuildBar":
                _maxValue = Stats.MaxGold;
                break;
            case "EnergyBar":
                _maxValue = Stats.MaxEnergy;
                Stats.PlayerEnergy = _currentVal = _maxValue;
                break;
            case "ArtefactBar":
                _maxValue = Stats.MaxArtefact;
                Stats.ArtefactHealth = _currentVal = _maxValue;
                break;
        }

        if (!percent)
            Display();
        else
            DisplayPercent();
    }
    
    private void Display() {
        _displayText.text = _currentVal + " / " + _maxValue;
        _slider.fillAmount = (float) _currentVal / _maxValue;
    }
    
    private void DisplayPercent() {
        _displayText.text = (float) _currentVal / _maxValue * 100.0f + " %";
        _slider.fillAmount = (float) _currentVal / _maxValue; 
    }
    
    public void ChangeValue(int value) {
        _currentVal += value;

        if (_currentVal <= 0)
            _currentVal = 0;
        else if (_currentVal >= _maxValue)
            _currentVal = _maxValue;

        if (!percent) 
            Display();
        else
            DisplayPercent();
    }
}
