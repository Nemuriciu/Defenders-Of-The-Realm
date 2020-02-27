using TMPro;
using UnityEngine;

public class GoldInfo : MonoBehaviour {
    [HideInInspector] 
    public int gold;

    private TextMeshProUGUI _goldText;
    private CanvasGroup _canvasGroup;
    private const int InitGold = 850;
    private int _acc;

    private void Start() {
        _goldText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
        ChangeValue(InitGold);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("Gold:  " + _acc);
            _acc = 0;
        }
    }

    public void ChangeValue(int value) {
        gold += value;
        _goldText.text = gold.ToString();
    }
    
    /* DEBUG */
    public void MobChangeValue(int value) {
        gold += value;
        _acc += value;
        _goldText.text = gold.ToString();
    }

    public void SetVisible() {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }
}
