using UnityEngine;

public class SimpleRotate : MonoBehaviour {
    private RectTransform _rect;
    private const int RotSpeed = 4;
    private float _currentVal;

    private void Start() {
        _rect = GetComponent<RectTransform>();
    }

    private void Update() {
        _currentVal += Time.deltaTime * RotSpeed;
        _rect.transform.rotation = Quaternion.Euler(0f, 0f, -72.0f * _currentVal);
    }
}