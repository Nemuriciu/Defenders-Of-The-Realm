using TMPro;
using UnityEngine;

public class GoldInfo : MonoBehaviour {
    [HideInInspector] 
    public int gold;

    private TextMeshProUGUI _goldText;
    private const int InitGold = 650;
    private int _acc;

    private void Start() {
        _goldText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        
        ChangeValue(InitGold);
    }

    public void ChangeValue(int value) {
        gold += value;
        _goldText.text = gold.ToString();
    }
}
