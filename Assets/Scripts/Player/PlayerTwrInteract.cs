using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerTwrInteract : MonoBehaviour {
    public GameObject upgradeText;
    public GameObject sellText;
    public AudioClip interactSound;

    public GameObject[] crossbowUpgrades;
    public GameObject[] crystalUpgrades;
    public GameObject[] hourglassUpgrades;

    private TextMeshProUGUI _upgradeValue;
    private TextMeshProUGUI _sellValue;
    
    private const float Angle = 20.0f;
    private ArrayList _towers;
    private GameObject _target;
    private Highlight _selection;
    private Transform _twrGroup;
    private bool _isActive;

    private Tower _twr;
    private ProgressBar _slider;
    private ErrorMessage _err;
    private AudioSource _audio;
    

    private void Start() {
        _towers = new ArrayList();
        _slider = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();
        _audio = GetComponent<AudioSource>();

        _upgradeValue = upgradeText.GetComponentInChildren<TextMeshProUGUI>();
        _sellValue = sellText.GetComponentInChildren<TextMeshProUGUI>();
        _err = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        _twrGroup = GameObject.Find("Towers").transform;
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
                _twr = _target.GetComponent<Tower>();
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
            if (!_isActive) {
                _sellValue.text = _twr.sellValue.ToString();
                _upgradeValue.text = (_twr.upgradeValue == 0) ? "MAX" : _twr.upgradeValue.ToString();
                DisplayTooltip(true);
            }
            
            if (Input.GetKeyDown(KeyCode.E)) {                    /* Upgrade */
                if (_twr.upgradeValue == 0) return;                  
                    
                if (Stats.PlayerGold < _twr.upgradeValue)
                    _err.Show("Insufficient Gold.");
                else {
                    Stats.PlayerGold -= _twr.upgradeValue;
                    _slider.ChangeValue(-_twr.upgradeValue);

                    Vector3 pos = _twr.gameObject.transform.position;
                    GameObject instance = null;
                    BuildAnimation bA;
                    
                    switch (_twr.tier) {
                        case "Tier 1":
                            switch (_twr.type) {
                                case "Crossbow":
                                    instance = Instantiate(crossbowUpgrades[0], pos, Quaternion.identity, _twrGroup);
                                    break;
                                case "Crystal":
                                    instance = Instantiate(crystalUpgrades[0], pos, Quaternion.identity, _twrGroup);
                                    break;
                                case "Hourglass":
                                    instance = Instantiate(hourglassUpgrades[0], pos, Quaternion.identity, _twrGroup);
                                    break;
                            }

                            if (instance != null) {
                                bA = instance.GetComponent<BuildAnimation>();
                                bA.isActive = true; 
                            }
                            
                            break;
                        
                        case "Tier 2" :
                            switch (_twr.type) {
                                case "Crossbow":
                                    instance = Instantiate(crossbowUpgrades[1], pos, Quaternion.identity, _twrGroup);
                                    break;
                                case "Crystal":
                                    instance = Instantiate(crystalUpgrades[1], pos, Quaternion.identity, _twrGroup);
                                    break;
                                case "Hourglass":
                                    instance = Instantiate(hourglassUpgrades[1], pos, Quaternion.identity, _twrGroup);
                                    break;
                            }

                            if (instance != null) {
                                bA = instance.GetComponent<BuildAnimation>();
                                bA.isActive = true; 
                            }
                            
                            break;
                    }
                    
                    /* Disable tooltip & Destroy target object */
                    _towers.Remove(_target);
                    DisplayTooltip(false);
                    Destroy(_target);
                }
            } else if (Input.GetKeyDown(KeyCode.Q)) {            /* Sell */
                Stats.PlayerGold += _twr.sellValue;
                _slider.ChangeValue(_twr.sellValue);
                
                /* Disable tooltip & Destroy target object */
                _towers.Remove(_target);
                DisplayTooltip(false);
                Destroy(_target);

                _audio.clip = interactSound;
                _audio.Play();
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
