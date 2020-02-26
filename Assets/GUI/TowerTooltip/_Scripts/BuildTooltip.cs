using TMPro;
using UnityEngine;

public class BuildTooltip : MonoBehaviour {
    public TextMeshProUGUI towerName, type, 
        damage, speed, range, info, buildVal;

    private Color32 _t1Color, _physicalColor, _magicalColor;

    private void Start() {
        _t1Color = new Color32(0, 175, 0, 255);
        _physicalColor = new Color32(225, 125, 0, 255);
        _magicalColor = new Color32(0, 200, 255, 255);
    }

    public void UpdateTooltip(string tName, string tType, int minDamage, int maxDamage,
        float tSpeed, float tRange, string tInfo, int tBuildVal) {
        float avgDamage = (minDamage + maxDamage) / 2.0f;
        
        towerName.text = tName;
        towerName.color = _t1Color;
        
        /* Set type text & color */
        switch (tType) {
            case "Physical":
                type.text = "Physical";
                type.color = _physicalColor;
                break;
            case "Magical":
                type.text = "Magical";
                type.color = _magicalColor;
                break;
            default:
                type.text = "N/A";
                type.color = Color.white;
                break;
        }

        /* Update tower values */
        damage.text = avgDamage > 0 ? "" + avgDamage : "-";
        speed.text = tSpeed > 0 ?
            Mathf.Round(tSpeed * 100f) / 100f + "/s" : "-";
        range.text = tRange > 0 ? "" + tRange : "-";
        info.text = tInfo;
        buildVal.text = "" + tBuildVal;
    }
}
