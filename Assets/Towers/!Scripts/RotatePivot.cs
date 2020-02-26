using UnityEngine;

public class RotatePivot : MonoBehaviour {

    private void Update() {
        transform.Rotate(0, 12.5f * Time.deltaTime, 0);
    }
}
