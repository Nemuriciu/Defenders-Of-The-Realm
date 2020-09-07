using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour {
    public GameObject highlight;
    public BuildPanel build;
    public UpgradePanel upgrade;
    public GameObject[] prefabs;
    public Vector3 SpawnPos { get; private set; }
    public AudioClip[] clips;

    private GameObject _towerObj;
    private Tower _twr;
    private GoldInfo _goldInfo;
    private ErrorMessage _error;
    private GameSystem _system;
    private AudioSource _audio;

    private void Start() {
        _goldInfo = GameObject.Find("GoldGroup").GetComponent<GoldInfo>();
        _error = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        _system = GameObject.Find("System").GetComponent<GameSystem>();
        _audio = GameObject.Find("BuildAudio").GetComponent<AudioSource>();

        SpawnPos = new Vector3(
            transform.position.x + 0.912f,
            transform.position.y + 1.824f,
            transform.position.z - 0.912f);
    }
    
    /*
    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        /* Upgrade tower #1#
        if (HasTower()) {
            /* Set upgrade/sell values #1#
            _upgrade.SetValues(_twr.upgradePrefab ? 
                    _twr.upgradePrefab.GetComponent<Tower>().buildVal : 0,
                    _twr.sellVal);

            _upgrade.SpotInstance = this;
            _upgradeTooltip.SetActive(true);
            return;
        }
    }
    */
    
    private void OnMouseDown() {
        if (_system.phase == Phase.Start) return;
        if (build.IsActive || upgrade.IsActive) return;
        
        if (!_twr) {
            build.SpotInstance = this;
            build.OpenPanel();
        } else {
            upgrade.SpotInstance = this;
            upgrade.OpenPanel();
        }
    }
    
    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject() ||
            _system.phase == Phase.Start) return;
        highlight.SetActive(true);
    }
    
    private void OnMouseExit() {
        if (!highlight.activeSelf) return;
        highlight.SetActive(false);
    }
    
    public void AddTower(int prefabIndex) {
        GameObject tower = prefabs[prefabIndex];
        Tower twr = tower.GetComponent<Tower>();
        
        /* Verify enough gold to build */
        if (_goldInfo.gold < twr.buildVal) {
            _error.SetMessage("Not enough gold to build tower.");
            return;
        }
        
        _towerObj = Instantiate(tower, SpawnPos, Quaternion.identity, transform);
        _twr = _towerObj.GetComponent<Tower>();
        _goldInfo.ChangeValue(-_twr.buildVal);
    }

    public void RemoveTower() {
        if (_twr && _twr.slowTower)
            _twr.RemoveSlows();
        
        _twr.RemoveHealthbar();
        _twr = null;
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
            _audio.PlayOneShot(clips[1]);
        }
    }
    
    public void RepairTower() {
        if (_twr) {
            /* Verify enough gold to build */
            if (_goldInfo.gold < _twr.repairVal) {
                _error.SetMessage("Not enough gold to repair.");
                return;
            }
            
            _twr.Repair();
            _goldInfo.ChangeValue(-_twr.repairVal);
            _audio.PlayOneShot(clips[1]);
        }
    }

    public void SellTower() {
        if (_twr && _twr.slowTower)
            _twr.RemoveSlows();
        
        _goldInfo.ChangeValue(_twr.sellVal);

        _twr.RemoveHealthbar();
        _twr = null;
        Destroy(_towerObj);
        _audio.PlayOneShot(clips[0]);
    }

    public Tower GetTower() {
        return _twr;
    }
}
