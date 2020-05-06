using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour {
    public Vector3 SpawnPos { get; private set; }
    
    private GameObject _towerObj;
    private Tower _twr;
    private Building _build;
    private Upgrade _upgrade;
    private GameObject _upgradeTooltip;

    private void Awake() {
        _build = GameObject.Find("System").GetComponent<Building>();
        _upgradeTooltip = GameObject.Find("UpgradeGroup");
        _upgrade = _upgradeTooltip.GetComponent<Upgrade>();

        SpawnPos = new Vector3(
            transform.position.x + 0.912f,
            transform.position.y + 1.824f,
            transform.position.z - 0.912f);
    }

    private void Start() {
        _upgradeTooltip.SetActive(false);
    }
    
    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        /* Upgrade tower */
        if (HasTower()) {
            _upgrade.SpotInstance = this;
            _upgradeTooltip.SetActive(true);
            return;
        }
        
        /* Build new tower */
        if (_build.IsBuilding) {
            _build.ResetBlueprints();
            AddTower(_build.prefabs[_build.TowerId]);
            _build.Reset();
        }
    }

    private void OnMouseEnter() {
        _build.SpotInstance = this;

        if (HasTower()) return;
        
        if (_build.IsBuilding)
            _build.SetBlueprint(_build.TowerId, SpawnPos);
    }

    private void OnMouseExit() {
        _build.SpotInstance = null;
        
        if (HasTower()) return;
        
        if (_build.IsBuilding)
            _build.ResetBlueprints();
    }

    private void AddTower(GameObject tower) {
        if (_towerObj != null) 
            RemoveTower();
        
        _towerObj = Instantiate(tower, SpawnPos, Quaternion.identity, transform);
        _twr = _towerObj.GetComponent<Tower>();
    }
    
    public void RemoveTower() {
        if (_twr && _twr.slowTower)
            _twr.RemoveSlows();
        
        Destroy(_towerObj);
    }
    
    public void UpgradeTower() {
        if (_towerObj && _twr.upgradePrefab) {
            GameObject tower = _twr.upgradePrefab;
            
            RemoveTower();
            _towerObj = Instantiate(tower, SpawnPos, Quaternion.identity, transform);
            _twr = _towerObj.GetComponent<Tower>();
        }
    }

    public bool HasTower() {
        return _twr != null;
    }
}
