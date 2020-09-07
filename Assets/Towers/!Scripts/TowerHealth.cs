using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour {
    public float yOffset;
    
    private TowerHealthRoot _healthRoot;
    private GameObject _healthObj;
    private Camera _cam;
    private CanvasGroup _canvasGr;
    private Tower _twr;
    private Image _healthValue;

    private void Start() {
        _healthRoot = GameObject.Find("TowerHealthRoot").GetComponent<TowerHealthRoot>();
        _cam = Camera.main;
        
        foreach (var obj in _healthRoot.pool) {
            if (!obj.activeSelf) {
                _healthObj = obj;
                _healthObj.SetActive(true);
                break;
            }
        }

        _canvasGr = _healthObj.GetComponent<CanvasGroup>();
        _twr = GetComponent<Tower>();
        _healthValue = _healthObj.transform.GetChild(1).GetComponent<Image>();
    }

    private void Update() {
        _healthObj.transform.position = _cam.WorldToScreenPoint(
            transform.position + new Vector3(0, yOffset, 0));

        _healthValue.fillAmount = (float)_twr.GetHealth() / _twr.baseHealth;

        /* Enable Healthbar when first hit */
        if (_healthValue.fillAmount < 1.0f && _canvasGr.alpha < 1.0f) {
            _canvasGr.alpha = 1;
            return;
        }
        
        /* Disable Healthbar when full health */
        if (_healthValue.fillAmount >= 1.0f && _canvasGr.alpha <= 0.0f)
            _canvasGr.alpha = 0;
    }

    public void FreeHealthbar() {
        _canvasGr.alpha = 0;
        _healthObj.transform.position = Vector3.zero;
        _healthObj.SetActive(false);
    }
}
