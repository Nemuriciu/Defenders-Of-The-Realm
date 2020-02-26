using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {
    public GameObject[] blueprints;
    public GameObject ballistaTower;
    public GameObject crystalTower;
    public GameObject pyroTower;
    public GameObject darkTower;
    public GameObject hourglassTower;
    // TODO: Add all towers
    
    public CanvasGroup interactCanvas;
    public CanvasGroup buildCanvas;
    public CanvasGroup upgradeCanvas;
    public bool CanBuild { get; private set; }

    private Camera _cam;
    private GameSystem _system;
    private Spot _spot;
    private ArrayList _spots;
    private GameObject _activeSpot;
    private GameObject _currentBlueprint;
    private Tower _tower;
    private BuildTooltip _buildTooltip;
    private UpgradeTooltip _upgradeTooltip;
    private GoldInfo _goldInfo;
    private ErrorMessage _error;
    private bool _interacting;
    
    private const float Angle = 44.0f;

    private void Start() {
        _cam = Camera.main;
        _spots = new ArrayList();
        _system = GameObject.Find("System").GetComponent<GameSystem>();
        _buildTooltip = buildCanvas.gameObject.GetComponent<BuildTooltip>();
        _upgradeTooltip = upgradeCanvas.gameObject.GetComponent<UpgradeTooltip>();
        _goldInfo = GameObject.Find("GoldInfo").GetComponent<GoldInfo>();
        _error = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        CanBuild = false;
    }
    
    private void Update() {
        if (_system.phase == Phase.Start) return;
        
        /* Check if still facing the Tower Spot from last frame */
        if (_activeSpot) {
            if(!_spots.Contains(_activeSpot) ||
               Vector3.Angle(transform.forward, 
                   _activeSpot.transform.position - transform.position) > Angle ||
               _cam.transform.eulerAngles.x < -10 || _cam.transform.eulerAngles.x > 75) {
                if (CanBuild) {
                    _interacting = CanBuild = false;
                    
                    if (_tower) {
                        _tower.SetOutline(false);
                        _tower = null;
                        TurnOff("upgrade");
                    }
                    else {
                        Destroy(_currentBlueprint);
                        TurnOff("build");
                    }
                }
                else {
                    _interacting = false;

                    if (_tower) {
                        _tower.SetOutline(false);
                        _tower = null;
                    }
                    TurnOff("interact");
                }
                
                _activeSpot = null;
                _spot = null;
            }
        }
        /* Check if facing any Tower Spot */
        else {
            foreach (GameObject spot in _spots) {
                if(Vector3.Angle(transform.forward, 
                       spot.transform.position - transform.position) < Angle &&
                   _cam.transform.eulerAngles.x > 12 && _cam.transform.eulerAngles.x < 75) {
                    _activeSpot = spot;
                    _spot = _activeSpot.GetComponent<Spot>();
                    _interacting = true;
                    
                    if (_spot.HasTower()) {
                        _tower = _spot.GetTower();
                        _tower.SetOutline(true);
                    }
                    
                    // TODO: Turn Interact Tooltip On
                    TurnOn("interact");
                    
                    break;
                }
            }
        }

        /* User Input */
        if (CanBuild) {

            /* Ballista Blueprint */
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (_spot != null && !_spot.HasTower())
                    if (!_currentBlueprint.CompareTag("Blueprint_1")) {
                        Destroy(_currentBlueprint);
                        _currentBlueprint = _spot.AddBlueprint(blueprints[0]);

                        Tower t = ballistaTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                    }
            }
            
            /* Crystal Blueprint */
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                if (_spot != null && !_spot.HasTower())
                    if (!_currentBlueprint.CompareTag("Blueprint_2")) {
                        Destroy(_currentBlueprint);
                        _currentBlueprint = _spot.AddBlueprint(blueprints[1]);

                        Tower t = crystalTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                    }
            }
            
            /* Pyro Blueprint */
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                if (_spot != null && !_spot.HasTower())
                    if (!_currentBlueprint.CompareTag("Blueprint_3")) {
                        Destroy(_currentBlueprint);
                        _currentBlueprint = _spot.AddBlueprint(blueprints[2]);

                        Tower t = pyroTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                    }
            }
            
            /* Dark Blueprint */
            else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                if (_spot != null && !_spot.HasTower())
                    if (!_currentBlueprint.CompareTag("Blueprint_4")) {
                        Destroy(_currentBlueprint);
                        _currentBlueprint = _spot.AddBlueprint(blueprints[3]);

                        Tower t = darkTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                    }
            }
            
            /* Hourglass Blueprint */
            else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                if (_spot != null && !_spot.HasTower())
                    if (!_currentBlueprint.CompareTag("Blueprint_5")) {
                        Destroy(_currentBlueprint);
                        _currentBlueprint = _spot.AddBlueprint(blueprints[4]);

                        Tower t = hourglassTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                    }
            }
            
            /* LBM - Confirm */
            else if (Input.GetMouseButtonDown(0)) {
                if (_spot != null && !_spot.HasTower()) {
                    Tower t;
                    
                    switch (_currentBlueprint.tag) {
                        case "Blueprint_1":
                            t = ballistaTower.GetComponent<Tower>();
                            
                            if (_goldInfo.gold < t.buildVal) {
                                _error.Show("Insufficient gold.");
                                return;
                            }

                            Destroy(_currentBlueprint);
                            _spot.AddTower(ballistaTower);
                            _goldInfo.ChangeValue(-t.buildVal);
                            break;
                        case "Blueprint_2":
                            t = crystalTower.GetComponent<Tower>();

                            if (_goldInfo.gold < t.buildVal) {
                                _error.Show("Insufficient gold.");
                                return;
                            }
                            
                            Destroy(_currentBlueprint);
                            _spot.AddTower(crystalTower);
                            _goldInfo.ChangeValue(-t.buildVal);
                            break;
                        case "Blueprint_3":
                            t = pyroTower.GetComponent<Tower>();

                            if (_goldInfo.gold < t.buildVal) {
                                _error.Show("Insufficient gold.");
                                return;
                            }
                            
                            Destroy(_currentBlueprint);
                            _spot.AddTower(pyroTower);
                            _goldInfo.ChangeValue(-t.buildVal);
                            break;
                        case "Blueprint_4":
                            t = darkTower.GetComponent<Tower>();

                            if (_goldInfo.gold < t.buildVal) {
                                _error.Show("Insufficient gold.");
                                return;
                            }
                            
                            Destroy(_currentBlueprint);
                            _spot.AddTower(darkTower);
                            _goldInfo.ChangeValue(-t.buildVal);
                            break;
                        case "Blueprint_5":
                            t = hourglassTower.GetComponent<Tower>();

                            if (_goldInfo.gold < t.buildVal) {
                                _error.Show("Insufficient gold.");
                                return;
                            }
                            
                            Destroy(_currentBlueprint);
                            _spot.AddTower(hourglassTower);
                            _goldInfo.ChangeValue(-t.buildVal);
                            break;
                    }
                    
                    _tower = _spot.GetTower();
                    t = _tower.upgradePrefab.GetComponent<Tower>();
                    _upgradeTooltip.UpdateTooltip(_tower.tName, t.type, _tower.damageMin,
                        _tower.damageMax, t.damageMin, t.damageMax,
                        _tower.atkSpeed * _tower.atkSpeedModif, 1.0f,
                        t.atkSpeed * t.atkSpeedModif, 1.0f, _tower.info,
                        t.buildVal, _tower.sellVal, false);
                    TurnOff("build");
                    TurnOn("upgrade");
                }
            }
            
            /* E - Upgrade Tower */
            else if (Input.GetKeyDown(KeyCode.E)) {
                if (_spot != null && _spot.HasTower() && _tower.upgradePrefab) {
                    /* Error msg if not enough gold to upgrade */
                    Tower t = _tower.upgradePrefab.GetComponent<Tower>();
                    if (_goldInfo.gold < t.buildVal) {
                        _error.Show("Insufficient gold.");
                        return;
                    }

                    if (_tower.slowTower)
                        _tower.RemoveSlows();
                    
                    _goldInfo.ChangeValue(-t.buildVal);
                    _spot.AddTower(_tower.upgradePrefab);
                    _tower = _spot.GetTower();
                    
                    if (_tower.upgradePrefab) {
                        t = _tower.upgradePrefab.GetComponent<Tower>();
                        _upgradeTooltip.UpdateTooltip(_tower.tName, t.type, _tower.damageMin,
                            _tower.damageMax, t.damageMin, t.damageMax,
                            _tower.atkSpeed * _tower.atkSpeedModif, 1.0f,
                            t.atkSpeed * t.atkSpeedModif, 1.0f, _tower.info,
                            t.buildVal, _tower.sellVal, false);


                    }
                    /* Tower is upgraded to MAX */
                    else {
                        _upgradeTooltip.UpdateTooltip(_tower.tName, _tower.type, _tower.damageMin,
                            _tower.damageMax, 0, 0,
                            _tower.atkSpeed * _tower.atkSpeedModif, 1.0f,
                            0, 0, _tower.info,
                            0, _tower.sellVal, true);
                    }
                }
            }
            
            /* Q - Sell Tower */
            else if (Input.GetKeyDown(KeyCode.Q)) {
                if (_spot != null && _spot.HasTower()) {
                    if (_tower.slowTower)
                        _tower.RemoveSlows();

                    _goldInfo.ChangeValue(_tower.sellVal);
                    _spot.RemoveTower();
                    CanBuild = false;
                    
                    TurnOff("upgrade");
                    TurnOn("interact");
                }
            }
        }
        else if (_interacting) {
            /* E - Interact */
            if (Input.GetKeyDown(KeyCode.E)) {
                CanBuild = true;
                TurnOff("interact");

                if (_spot != null) {
                    if (_spot.HasTower()) {
                        /* Tower is not upgraded to MAX */
                        if (_tower.upgradePrefab) {
                            Tower t = _tower.upgradePrefab.GetComponent<Tower>();
                            _upgradeTooltip.UpdateTooltip(_tower.tName, t.type, _tower.damageMin,
                                _tower.damageMax, t.damageMin, t.damageMax,
                                _tower.atkSpeed * _tower.atkSpeedModif, 1.0f,
                                t.atkSpeed * t.atkSpeedModif, 1.0f, _tower.info,
                                t.buildVal, _tower.sellVal, false);


                        }
                        /* Tower is upgraded to MAX */
                        else {
                            _upgradeTooltip.UpdateTooltip(_tower.tName, _tower.type, _tower.damageMin,
                                _tower.damageMax, 0, 0,
                                _tower.atkSpeed * _tower.atkSpeedModif, 1.0f,
                                0, 0, _tower.info,
                                0, _tower.sellVal, true);
                        }
                        
                        TurnOn("upgrade");
                    }
                    /* Add default blueprint tower (Ballista) */
                    else {
                        _currentBlueprint = _spot.AddBlueprint(blueprints[0]);

                        Tower t = ballistaTower.GetComponent<Tower>();
                        _buildTooltip.UpdateTooltip(t.tName, t.type, t.damageMin,
                            t.damageMax, t.atkSpeed * t.atkSpeedModif,
                            1.01f, t.info, t.buildVal);
                        
                        TurnOn("build");
                    }
                }
            }
        }
    }

    private void TurnOn(string type) {
        switch (type) {
            case "interact":
                interactCanvas.alpha = 1;
                interactCanvas.interactable = true;
                break;
            case "build":
                buildCanvas.alpha = 1;
                buildCanvas.interactable = true;
                break;
            case "upgrade":
                upgradeCanvas.alpha = 1;
                upgradeCanvas.interactable = true;
                break;
        }
    }

    private void TurnOff(string type) {
        switch (type) {
            case "interact":
                interactCanvas.alpha = 0;
                interactCanvas.interactable = false;
                break;
            case "build":
                buildCanvas.alpha = 0;
                buildCanvas.interactable = false;
                break;
            case "upgrade":
                upgradeCanvas.alpha = 0;
                upgradeCanvas.interactable = false;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TowerSpot")) {
            _spots.Add(other.gameObject);
        }
            
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("TowerSpot")){
            _spots.Remove(other.gameObject);
        }
    }
}
