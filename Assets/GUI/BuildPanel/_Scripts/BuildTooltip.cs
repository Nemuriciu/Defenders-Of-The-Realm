using TMPro;
using UnityEngine;

public class BuildTooltip : MonoBehaviour {
    private TextMeshProUGUI _towerName;
    private TextMeshProUGUI _info;
    private Color32 _physicalColor, _magicalColor;

    private void Start() {
        _towerName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _info = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _physicalColor = new Color32(255, 153, 0, 255 );
        _magicalColor = new Color32(175, 75, 255, 255);
    }

    public void SetTooltip(Tower twr) {
        switch (twr.type) {
            case "Physical":
                _towerName.color = _physicalColor;
                break;
            case "Magical":
                _towerName.color = _magicalColor;
                break;
            default:
                _towerName.color = Color.white;
                break;
        }

        _towerName.text = twr.tName;
        _info.text = twr.info;
    }
}
