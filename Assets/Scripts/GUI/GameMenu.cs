using UnityEngine;

public class GameMenu : MonoBehaviour {
    public GameObject panel;
    
    private bool _isPanelOpen;
    private float _timeScaleAux;
    
    private void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPanelOpen) {
                CloseMenu(true);
            }
            else {
                panel.SetActive(true);
                _isPanelOpen = true;
                _timeScaleAux = Time.timeScale;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        */
    }

    public void CloseMenu(bool _lock) {
        panel.SetActive(false);
        _isPanelOpen = false;
        Time.timeScale = _timeScaleAux;

        if (_lock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
