using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour {
    private CanvasGroup _panel;

    private void Start() {
        _panel = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (gameObject.CompareTag("DoNotClose"))
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape) && _panel.interactable) {
            ClosePanel();
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ClosePanel() {
        _panel.alpha = 0;
        _panel.interactable = false;
        _panel.blocksRaycasts = false;
    }
}
