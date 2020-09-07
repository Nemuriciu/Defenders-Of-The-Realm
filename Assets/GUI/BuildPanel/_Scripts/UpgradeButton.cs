using UnityEngine;

public class UpgradeButton : MonoBehaviour {
    private RectTransform _rect;
    private UpgradePanel _upgrade;

    private void Start() {
        _rect = GetComponent<RectTransform>();
        _upgrade = GetComponentInParent<UpgradePanel>();
    }

    public void SellTower() {
        _upgrade.SpotInstance.SellTower();
        _upgrade.ClosePanel();
    }
    
    public void UpgradeTower() {
        _upgrade.SpotInstance.UpgradeTower();
        _upgrade.ClosePanel();
    }
    
    public void RepairTower() {
        _upgrade.SpotInstance.RepairTower();
        _upgrade.ClosePanel();
    }

    public void ZoomIn() {
        _rect.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }
    
    public void ZoomOut() {
        _rect.localScale = Vector3.one;
    }
}
