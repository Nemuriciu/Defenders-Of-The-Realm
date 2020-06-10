using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoOptions : MonoBehaviour {
    public TMP_Dropdown resolution;
    public TMP_Dropdown display;
    public TextMeshProUGUI quality;

    private List<Resolution> _resolutions;
    private string[] _quality;
    private int _qualityIndex;

    private void Start() {
        /* Screen Resolution */
        _resolutions = new List<Resolution>(Screen.resolutions);
        _resolutions.RemoveAll(item => item.width < 1024);
        _resolutions.Reverse();

        for (int i = 0; i < _resolutions.Count; i++) {
            resolution.options.Add(new TMP_Dropdown.OptionData(
                _resolutions[i].width + " x " + _resolutions[i].height + 
                " (" + _resolutions[i].refreshRate + "Hz)"));

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height &&
                _resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                resolution.value = i;
        }

        /* Display Mode */
        display.options.Add(new TMP_Dropdown.OptionData("Fullscreen"));
        display.options.Add(new TMP_Dropdown.OptionData("Windowed"));
        display.value = Screen.fullScreen ? 0 : 1;
        
        /* Quality Level */
        _quality = new[] {"Very Low", "Low", "Medium", 
            "High", "Very High", "Ultra"};

        _qualityIndex = QualitySettings.GetQualityLevel();
        quality.text = _quality[_qualityIndex];
    }

    public void Refresh() {
        /* Refresh current resolution */
        for (int i = 0; i < _resolutions.Count; i++) {
            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height &&
                _resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                resolution.value = i;
        }
        
        /* Refresh Window Mode */
        display.value = Screen.fullScreen ? 0 : 1;
        
        /* Refresh Quality Level */
        _qualityIndex = QualitySettings.GetQualityLevel();
        quality.text = _quality[_qualityIndex];
    }

    public void NextQuality() {
        if (_qualityIndex < 5) {
            _qualityIndex++;
            quality.text = _quality[_qualityIndex];
        }
    }
    
    public void PreviousQuality() {
        if (_qualityIndex > 0) {
            _qualityIndex--;
            quality.text = _quality[_qualityIndex];
        }
    }

    public void Apply() {
        /* Change Resolution & Window Mode*/
        Resolution current = _resolutions[resolution.value];
        if (!Screen.currentResolution.Equals(current)) {
            FullScreenMode mode = display.value == 0 ? 
                FullScreenMode.FullScreenWindow : 
                FullScreenMode.Windowed;
            Screen.SetResolution(current.width, current.height, mode, current.refreshRate);
        }
        /* Change only Window Mode */
        else if (Screen.fullScreen && display.value == 1 ||
                 !Screen.fullScreen && display.value == 0) {
            Screen.fullScreenMode = display.value == 0 ? 
                FullScreenMode.FullScreenWindow : 
                FullScreenMode.Windowed;
        }
        
        /* Change Graphics Quality */
        if (!_qualityIndex.Equals(QualitySettings.GetQualityLevel())) {
            QualitySettings.SetQualityLevel(_qualityIndex);
        }
    }
}