using TMPro;
using UnityEngine;

public class StatsMenu : MonoBehaviour {
    public GameObject panel;

    public TextMeshProUGUI monstersKilled;
    public TextMeshProUGUI towerDamage;
    public TextMeshProUGUI playerDamage;
    public TextMeshProUGUI goldReceived;

    public void ActivatePanel(bool flag) {
        monstersKilled.text = Stats.monstersKilled.ToString();
        towerDamage.text = Stats.towerDamage.ToString();
        playerDamage.text = Stats.playerDamage.ToString();
        goldReceived.text = Stats.goldReceived.ToString();
        
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (flag) {
            Stats.savedTimeScale = Time.timeScale;
            Time.timeScale = 0;    
        }
    }
}
