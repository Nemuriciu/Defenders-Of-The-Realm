using UnityEngine;

public class CrystalInteract : MonoBehaviour {
    public ProgressBar progressBar;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            progressBar.ChangeValue(15);
        }
    }

    public void Hit(float value) {
        progressBar.ChangeValue(value);
    }
}
