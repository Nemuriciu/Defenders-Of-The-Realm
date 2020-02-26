using UnityEngine;

public class ZoomEffect : MonoBehaviour {
    private RectTransform _rect;
    private float _scale = 1.0f;
    private bool _zoomIn;
    private const float Val = 0.1f;

    private void Start() {
        _rect = GetComponent<RectTransform>();
        _zoomIn = true;
    }

    private void Update() {
        if (_scale >= 1.1f && _zoomIn)
            _zoomIn = false;
        else if (_scale <= 1.0f && !_zoomIn)
            _zoomIn = true;
        
        if (_zoomIn) {
            _scale += Time.deltaTime * Val;
            _rect.localScale += new Vector3(Time.deltaTime * Val, Time.deltaTime * Val);
        }
        else {
            _scale -= Time.deltaTime * Val;
            _rect.localScale -= new Vector3(Time.deltaTime * Val, Time.deltaTime * Val);
        }
    }
}
