using System.Collections;
using UnityEngine;

public class TowerBuild : MonoBehaviour {
    public Material redMat, greenMat;
    public Renderer[] coloredParts;

    private ArrayList _colliders;
    private bool _isValid = true;

    private void Start() {
        _colliders = new ArrayList();
    }

    private void OnTriggerEnter(Collider other) {
        if (!enabled) return;
        
        if (other.CompareTag("IgnoreCol") || other.CompareTag("PlayerInteract")) return;
        
        _colliders.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (!enabled) return;
        
        if (other.CompareTag("IgnoreCol") || other.CompareTag("PlayerInteract")) return;
        
        _colliders.Remove(other.gameObject);
    }

    private void Update() {
        if (_colliders.Count > 0 && _isValid) {
            _isValid = false;

            foreach (Renderer part in coloredParts)
                part.material = redMat;
        } else if (_colliders.Count == 0 && !_isValid) {
            _isValid = true;
            
            foreach (Renderer part in coloredParts)
                part.material = greenMat;
        }
    }

    public bool IsValid() {
        return _isValid;
    }
}