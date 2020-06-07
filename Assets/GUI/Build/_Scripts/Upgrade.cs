using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour, IPointerClickHandler {
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI sellText;
    public Spot SpotInstance { private get; set; }
    
    private Camera _cam;
    private bool _enabled = true;
    
    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (SpotInstance && SpotInstance.HasTower()) {
            transform.position = _cam.WorldToScreenPoint(SpotInstance.SpawnPos);
            
            if (_enabled) {
                /* Upgrade tower */
                if (Input.GetKeyDown(KeyCode.E)) {
                    SpotInstance.UpgradeTower();

                    SpotInstance = null;
                    _enabled = false;
                    gameObject.SetActive(false);
                }
                /* Sell tower */
                else if (_enabled && Input.GetKeyDown(KeyCode.Q)) {
                    SpotInstance.SellTower();

                    SpotInstance = null;
                    _enabled = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!_enabled) {
            _enabled = true;
            return;
        }

        SpotInstance = null;
        _enabled = false;
        gameObject.SetActive(false);
    }

    public void SetValues(int upgrade, int sell) {
        upgradeText.text = upgrade == 0 ? "MAX" : upgrade.ToString();
        sellText.text = sell.ToString();
    }
}
