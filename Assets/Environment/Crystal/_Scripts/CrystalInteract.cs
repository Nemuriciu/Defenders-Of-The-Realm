using UnityEngine;

public class CrystalInteract : MonoBehaviour {
    public ProgressBar progressBar;

    public void Hit(float value) {
        progressBar.ChangeValue(value);
    }
}
