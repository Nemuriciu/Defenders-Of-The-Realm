using System.Collections;
using UnityEngine;

public class PlayerTwrInteract : MonoBehaviour {
    public GameObject upgradeText;
    public GameObject sellText;
    
    private const float Angle = 20.0f;
    private ArrayList _towers;
    private GameObject _target;
    private Highlight _selection;
    private bool _isActive;
    

    private void Start() {
        _towers = new ArrayList();
    }

    private void Update() {
        if (_towers.Count <= 0) {
            if (_target) {
                _target = null;
                DisplayTooltip(false);
            }
            return;
        }

        // Check every frame if player still looking towards a tower
        _target = null;
        foreach (GameObject tower in _towers) {
            if (Vector3.Angle(transform.forward, 
                    tower.transform.position - transform.position) < Angle) {
                _target = tower;
                _selection = _target.GetComponent<Highlight>();
                break;
            }
        }
        
        // Not currently looking at a tower
        if (!_target) {
            // If tooltip is active, disable it
            if (_isActive)
                DisplayTooltip(false);
        } 
        // Looking at tower _target
        else {
            // If tooltip is not active, enable it
            if (!_isActive)
                DisplayTooltip(true);
            
            // Tooltip is active => Get Keyboard Interactions
            if (Input.GetKeyDown(KeyCode.E)) {
                // TODO: Upgrade
                Debug.Log("Upgrading " + _target.name);
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                // TODO: Sell
                Debug.Log("Selling " + _target.name);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerInteract"))
            _towers.Add(other.transform.parent.parent.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerInteract"))
            if (_towers.Contains(other.transform.parent.parent.gameObject))
                _towers.Remove(other.transform.parent.parent.gameObject);
    }

    private void DisplayTooltip(bool flag) {
        if (flag) {
            _isActive = true;
            _selection.enabled = true;
            upgradeText.SetActive(true);
            sellText.SetActive(true);
        }
        else {
            _isActive = false;
            _selection.enabled = false;
            upgradeText.SetActive(false);
            sellText.SetActive(false);
        }
    }
}
