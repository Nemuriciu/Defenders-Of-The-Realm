using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour {
    public bool IsActive { get; private set; }
    
    private CanvasGroup _canvasGr;
    private TextMeshProUGUI _text;
    private string _type;

    private void Start() {
        _canvasGr = GetComponent<CanvasGroup>();
        _text = gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && IsActive) {
            No();
        }
    }

    public void SetText(string text) {
        _text.text = text;
    }

    public void Enable(string type) {
        _canvasGr.alpha = 1;
        _canvasGr.interactable = true;
        _canvasGr.blocksRaycasts = true;

        _type = type;
        IsActive = true;
    }

    public void No() {
        _canvasGr.alpha = 0;
        _canvasGr.interactable = false;
        _canvasGr.blocksRaycasts = false;
        IsActive = false;
    }

    public void Yes() {
        _canvasGr.alpha = 0;
        _canvasGr.interactable = false;
        _canvasGr.blocksRaycasts = false;
        IsActive = false;

        switch (_type) {
            case "Exit":
                Time.timeScale = GameTime.OriginalTimeScale;
                GameTime.IsPaused = false;
                Application.Quit();
                break;
            case "MainMenu":
                Time.timeScale = GameTime.OriginalTimeScale;
                GameTime.IsPaused = false;
                SceneManager.LoadScene("StartMenu");
                break;
        }
    }
}
