using TMPro;
using UnityEngine;

public class WaveCount : MonoBehaviour {
    [HideInInspector] public int currentWave, maxWave = 5, enemyCount;
    
    private TextMeshProUGUI _currentWaveText;
    private TextMeshProUGUI _enemyCountText;

    private void Start() {
        _currentWaveText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _enemyCountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Display() {
        _currentWaveText.text = currentWave + " / " + maxWave;
        _enemyCountText.text = enemyCount.ToString();
    }
}
