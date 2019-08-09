using UnityEngine;
using UnityEngine.EventSystems;

public class ExitToMainMenu : MonoBehaviour, IPointerClickHandler {
    private GameObject _panel;

    private void Start() {
        _panel = transform.parent.gameObject;
    }

    public void OnPointerClick(PointerEventData eventData) {
        _panel.SetActive(false);
        Info.isPanelOpen = false;
        Info.selectedDifficulty = null;
    }
}
