using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour {
    public Spot SpotInstance { get; set; }
    public bool IsActive { get; private set; }

    private CanvasGroup _panel;
    private CanvasGroup _repair;
    private Camera _cam;
    private GameObject _blockingPanel;
    
    private TextMeshProUGUI _sellValue;
    private TextMeshProUGUI _upgradeValue;
    private TextMeshProUGUI _repairValue;
    
    private void Start() {
        _cam = Camera.main;
        _panel = GetComponent<CanvasGroup>();
        _blockingPanel = transform.GetChild(3).gameObject;
        _repair = transform.GetChild(2).GetComponent<CanvasGroup>();
        _sellValue = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _upgradeValue = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _repairValue = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (!SpotInstance) return; 
        
        transform.position = _cam.WorldToScreenPoint(SpotInstance.SpawnPos);
    }

    public void OpenPanel() {
        /* Set Sell & Upgrade values */
        Tower twr = SpotInstance.GetTower();
        _sellValue.text = twr.sellVal.ToString();

        if (twr.upgradePrefab) {
            Tower upgrTwr = twr.upgradePrefab.GetComponent<Tower>();
            _upgradeValue.text = upgrTwr.buildVal.ToString();
        }
        else
            _upgradeValue.text = "MAX";
        
        /* Set Repair */
        if (twr.health < twr.baseHealth) {
            _repair.alpha = 1;
            _repair.blocksRaycasts = true;
            _repairValue.text = twr.repairVal.ToString();
        }

        _panel.alpha = 1;
        _panel.blocksRaycasts = true;
        IsActive = true;
    }

    public void ClosePanel() {
        _blockingPanel.SetActive(true);
        _repair.alpha = 0;
        _repair.blocksRaycasts = false;
        _panel.alpha = 0;
        _panel.blocksRaycasts = false;
        IsActive = false;
    }

    public void CloseClickBlocker() {
        _blockingPanel.SetActive(false);
    }
}
