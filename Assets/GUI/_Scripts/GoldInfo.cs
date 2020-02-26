using TMPro;
using UnityEngine;

public class GoldInfo : MonoBehaviour {
    [HideInInspector] 
    public int gold;

    private TextMeshProUGUI _goldText;
    private CanvasGroup _canvasGroup;

    private void Start() {
        _goldText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
        // TODO:
        ChangeValue(1000);
    }

    public void ChangeValue(int value) {
        gold += value;
        _goldText.text = gold.ToString();
    }

    public void SetVisible() {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }
}
