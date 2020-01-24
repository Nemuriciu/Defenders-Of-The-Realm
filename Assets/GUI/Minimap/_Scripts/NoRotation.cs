using UnityEngine;

public class NoRotation : MonoBehaviour {
    private Quaternion _rotation;

    private void Awake() {
        _rotation = transform.rotation;
    }

    private void LateUpdate() {
        transform.rotation = _rotation;
    }
}