using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour {
    public Vector3 SpawnPos { get; private set; }
    
    private GameObject _towerObj;
    private Tower _twr;
    private Building _build;
    private Upgrade _upgrade;
    private GameObject _upgradeTooltip;

    private GoldInfo _goldInfo;
    private ErrorMessage _error;

    private void Awake() {
        _build = GameObject.Find("System").GetComponent<Building>();
        _upgradeTooltip = GameObject.Find("UpgradeGroup");
        _upgrade = _upgradeTooltip.GetComponent<Upgrade>();
        _goldInfo = GameObject.Find("GoldGroup").GetComponent<GoldInfo>();
        _error = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();

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
            /* Set upgrade/sell values */
            _upgrade.SetValues(_twr.upgradePrefab ? 
                    _twr.upgradePrefab.GetComponent<Tower>().buildVal : 0,
                    _twr.sellVal);

            _upgrade.SpotInstance = this;
            _upgradeTooltip.SetActive(true);
            return;
        }
        
        /* Build new tower */
        if (_build.IsBuilding) {
            GameObject tower = _build.prefabs[_build.TowerId];
            Tower twr = tower.GetComponent<Tower>();
            
            /* Verify enough gold to build */
            if (_goldInfo.gold < twr.buildVal) {
                _error.SetMessage("Not enough gold to build.");
                return;
            }
            
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
        _goldInfo.ChangeValue(-_twr.buildVal);
    }
    
    private void RemoveTower() {
        if (_twr && _twr.slowTower)
            _twr.RemoveSlows();
        
        Destroy(_towerObj);
    }

    public void UpgradeTower() {
        if (_towerObj && _twr.upgradePrefab) {
            GameObject tower = _twr.upgradePrefab;
            Tower twr = tower.GetComponent<Tower>();
            
            /* Verify enough gold to build */
            if (_goldInfo.gold < twr.buildVal) {
                _error.SetMessage("Not enough gold to upgrade.");
                return;
            }
            
            RemoveTower();
            _towerObj = Instantiate(tower, SpawnPos, Quaternion.identity, transform);
            _twr = _towerObj.GetComponent<Tower>();
            _goldInfo.ChangeValue(-_twr.buildVal);
        }
    }

    public void SellTower() {
        if (_twr && _twr.slowTower)
            _twr.RemoveSlows();
        
        _goldInfo.ChangeValue(_twr.sellVal);
        
        Destroy(_towerObj);
    }
    
    public bool HasTower() {
        return _twr != null;
    }
}
