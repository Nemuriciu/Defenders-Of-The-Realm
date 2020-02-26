using UnityEngine;

public class PortalTooltipCanvasGroup : MonoBehaviour {
    private CanvasGroup _canvasGroup;
    private bool _active;

    private void Start() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _active = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
            if (_active) {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
                _active = false;
            }
            else {
                _canvasGroup.alpha = 1;
                _canvasGroup.interactable = true;
                _active = true;
            }
    }
}
