using UnityEngine;

public class PauseMenu : MonoBehaviour {
    private GameSystem _gameSystem;
    private ConfirmPanel _confirm;
    private CanvasGroup _panel, _currentPanel;
    private CanvasGroup _main,
        _options,
        _videoPanel,
        _audioPanel;

    private void Start() {
        _gameSystem = GameObject.Find("System").GetComponent<GameSystem>();
        _confirm = GameObject.Find("ConfirmPanel").GetComponent<ConfirmPanel>();
        _panel = GetComponent<CanvasGroup>();
        _main = GameObject.Find("Main").GetComponent<CanvasGroup>();
        _options = GameObject.Find("Options").GetComponent<CanvasGroup>();
        _videoPanel = GameObject.Find("VideoPanel").GetComponent<CanvasGroup>();
        _audioPanel = GameObject.Find("AudioPanel").GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (_videoPanel.interactable || _audioPanel.interactable || _confirm.IsActive)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameSystem.isBuilding) {
            if (!_main.interactable && !_options.interactable) {
                OpenPanel("Main");
                GameTime.TimeScale = Time.timeScale;
                Time.timeScale = 0;
                GameTime.IsPaused = true;
            }
            else {
                ResumeGame(_options.interactable ? "Options" : "Main");
            }
        }
    }

    public void MainMenu() {
        _confirm.SetText("Are you sure you want to exit to main menu? " +
                         "Current game progress will be lost.");
        _confirm.Enable("MainMenu");
    }
    
    public void ExitButton() {
        _confirm.SetText("Are you sure you want to exit to desktop? " +
                         "Current game progress will be lost.");
        _confirm.Enable("Exit");
    }

    public void OpenPanel(string panel) {
        switch (panel) {
            case "Main":
                _panel.alpha = 1;
                _panel.interactable = true;
                _panel.blocksRaycasts = true;
                
                _main.alpha = 1;
                _main.interactable = true;
                _main.blocksRaycasts = true;
                break;
            case "Options":
                _main.alpha = 0;
                _main.interactable = false;
                _main.blocksRaycasts = false;
                
                _options.alpha = 1;
                _options.interactable = true;
                _options.blocksRaycasts = true;
                break;
            case "VideoPanel":
                _videoPanel.alpha = 1;
                _videoPanel.interactable = true;
                _videoPanel.blocksRaycasts = true;
                _videoPanel.GetComponent<VideoOptions>().Refresh();
                break;
            case "AudioPanel":
                _audioPanel.alpha = 1;
                _audioPanel.interactable = true;
                _audioPanel.blocksRaycasts = true;
                break;
        }
    }

    public void ClosePanel(string panel) {
        switch (panel) {
            case "Main":
                _main.alpha = 0;
                _main.interactable = false;
                _main.blocksRaycasts = false;
                
                _panel.alpha = 0;
                _panel.interactable = false;
                _panel.blocksRaycasts = false;
                Time.timeScale = GameTime.TimeScale;
                GameTime.IsPaused = false;
                break;
            case "Options":
                _options.alpha = 0;
                _options.interactable = false;
                _options.blocksRaycasts = false;

                _main.alpha = 1;
                _main.interactable = true;
                _main.blocksRaycasts = true;
                break;
            case "VideoPanel":
                _videoPanel.alpha = 0;
                _videoPanel.interactable = false;
                _videoPanel.blocksRaycasts = false;
                break;
            case "AudioPanel":
                _audioPanel.alpha = 0;
                _audioPanel.interactable = false;
                _audioPanel.blocksRaycasts = false;
                break;
        }
    }
    
    public void ResumeGame(string panel) {
        switch (panel) {
            case "Main":
                _main.alpha = 0;
                _main.interactable = false;
                _main.blocksRaycasts = false;
                
                _panel.alpha = 0;
                _panel.interactable = false;
                _panel.blocksRaycasts = false;
                break;
            case "Options":
                _options.alpha = 0;
                _options.interactable = false;
                _options.blocksRaycasts = false;
                
                _panel.alpha = 0;
                _panel.interactable = false;
                _panel.blocksRaycasts = false;
                break;
        }
        
        Time.timeScale = GameTime.TimeScale;
        GameTime.IsPaused = false;
    }
}
