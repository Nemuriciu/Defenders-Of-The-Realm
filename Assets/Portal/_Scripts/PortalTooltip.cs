using UnityEngine;

public class PortalTooltip : MonoBehaviour {
    public GameObject portal;
    private Camera _cam;
    private CanvasGroup _canvasGroup;
    private Vector3 _offsetY;
    private bool _active;
    
    

    private void Start() {
        _cam = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
        _offsetY = new Vector3(0, 6.5f, 0);

        _active = true;
    }

    private void LateUpdate() {
        Vector3 targetDir = portal.transform.position - _cam.transform.position;
        float angle = Vector3.Angle(targetDir, _cam.transform.forward);
            

        if (angle >= _cam.fieldOfView && _active) {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _active = false;
        } else if (angle < _cam.fieldOfView) {
            if (!_active) {
                _canvasGroup.alpha = 1;
                _canvasGroup.interactable = true;
                _active = true;
            }

            transform.position = _cam.WorldToScreenPoint(portal.transform.position + _offsetY);
        }
    }
}
