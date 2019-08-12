using TMPro;
using UnityEngine;

public class StatsMenu : MonoBehaviour {
    public GameObject panel;

    public TextMeshPro monstersKilled;
    public TextMeshPro towerDamage;
    public TextMeshPro playerDamage;
    public TextMeshPro goldReceived;
    
    private void Start() {
        monstersKilled.text = Stats.monstersKilled.ToString();
        towerDamage.text = Stats.towerDamage.ToString();
        playerDamage.text = Stats.playerDamage.ToString();
        goldReceived.text = Stats.goldReceived.ToString();
    }

    public void ActivatePanel() {
        panel.SetActive(true);
    }
}
