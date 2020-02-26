using TMPro;
using UnityEngine;

public class UpgradeTooltip : MonoBehaviour {
    public TextMeshProUGUI oldDamage, oldSpeed, oldRange;
    public TextMeshProUGUI towerName, type, newDamage, newSpeed,
        newRange, info, upgradeVal, sellVal;
    [Space(15)] 
    public TextMeshProUGUI damageArrow;
    public TextMeshProUGUI speedArrow;
    public TextMeshProUGUI rangeArrow;
    
    private Color32 _t1Color, _t2Color, _t3Color;
    private Color32 _physicalColor, _magicalColor;
    private Color32 _goodColor, _badColor;

    private void Start() {
        _t1Color = new Color32(0, 175, 0, 255);
        _t2Color = new Color32(175, 0, 175, 255);
        _t3Color = new Color32(175, 0, 0, 255);
        
        _physicalColor = new Color32(225, 125, 0, 255);
        _magicalColor = new Color32(0, 200, 255, 255);
        _goodColor = new Color32(0, 225, 0, 255);
        _badColor = new Color32(225, 0, 0, 255);
    }

    public void UpdateTooltip(string tName, string tType, int oldMinDamage, int oldMaxDamage,
        int newMinDamage, int newMaxDamage, float oldTSpeed, float oldTRange, float newTSpeed,
        float newTRange, string tInfo, int tUpgradeVal, int tSellVal, bool isMaxed) {
        float oldAvgDamage = (oldMinDamage + oldMaxDamage) / 2.0f;
        float newAvgDamage = (newMinDamage + newMaxDamage) / 2.0f;
        
        towerName.text = tName;
        if (towerName.text.Contains("T1"))
            towerName.color = _t1Color;
        else if (towerName.text.Contains("T2"))
            towerName.color = _t2Color;
        else if (towerName.text.Contains("T3"))
            towerName.color = _t3Color;
        
        info.text = tInfo;
        upgradeVal.text = "" + tUpgradeVal;
        
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

        /* Update old values */
        oldDamage.text = oldAvgDamage > 0 ?
            "" + (oldMinDamage + oldMaxDamage) / 2.0f : "-";
        oldSpeed.text = oldTSpeed > 0 ?
            Mathf.Round(oldTSpeed * 100f) / 100f + "/s" : "-";
        oldRange.text = oldTRange > 0 ? "" + oldTRange : "-";

        if (isMaxed) {
            SetMax(true);
            return;
        }
        
        SetMax(false);
        
        /* New Damage Value & Color */
        newDamage.text = newAvgDamage > 0 ? "" + newAvgDamage : "-";
        
        if (newAvgDamage > oldAvgDamage)
            newDamage.color = _goodColor;
        else if (newAvgDamage.Equals(oldAvgDamage))
            newDamage.color = Color.white;

        /* New Speed Value & Color */
        newSpeed.text = newTSpeed > 0 ?
            Mathf.Round(newTSpeed * 100f) / 100f + "/s" : "-";

        if (newTSpeed > oldTSpeed)
            newSpeed.color = _goodColor;
        else if (newTSpeed < oldTSpeed)
            newSpeed.color = _badColor;
        else
            newSpeed.color = Color.white;
        
        /* New Range Value & Color */
        newRange.text = newTRange > 0 ? "" + newTRange : "-";
        
        if (newTRange > oldTRange)
            newRange.color = _goodColor;
        else if (newTRange.Equals(oldTRange))
            newRange.color = Color.white;
    }

    private void SetMax(bool isMaxed) {
        if (isMaxed) {
            damageArrow.enabled = false;
            speedArrow.enabled = false;
            rangeArrow.enabled = false;

            newDamage.enabled = false;
            newSpeed.enabled = false;
            newRange.enabled = false;

            upgradeVal.text = "MAX";
        }
        else {
            damageArrow.enabled = true;
            speedArrow.enabled = true;
            rangeArrow.enabled = true;

            newDamage.enabled = true;
            newSpeed.enabled = true;
            newRange.enabled = true;
        }
    }
}
