using System.Collections;
using TMPro;
using UnityEngine;

public enum Phase {
    Start,
    Build,
    Combat,
    End
};

public class GameSystem : MonoBehaviour {
    public Spawn[] portals;
    public AudioClip[] clips;
    public GameObject defeat, victory;
    
    [HideInInspector] 
    public Phase phase;
    [HideInInspector] 
    public int waveNr = 1;
    [HideInInspector] 
    public int creatureNr = -1;
    [HideInInspector]
    public bool isBuilding, isPaused;
    
    private EventMessage _event;
    private AudioSource _audio;
    private TextMeshProUGUI _phaseText;
    private TextMeshProUGUI _waveText;
    private TextMeshProUGUI _mobCountText;
    private GoldInfo _goldInfo;
    private int _waveCreatureNr;
    private Transform _enemiesT;
    
    private void Start() {
        _audio = GetComponent<AudioSource>();
        _event = GameObject.Find("EventBox").GetComponent<EventMessage>();
        _phaseText = GameObject.Find("PhaseBox").GetComponent<TextMeshProUGUI>();
        _waveText = GameObject.Find("WaveInfo").GetComponentInChildren<TextMeshProUGUI>();
        _mobCountText = GameObject.Find("MobCount").GetComponentInChildren<TextMeshProUGUI>();
        _goldInfo = GameObject.Find("GoldGroup").GetComponent<GoldInfo>();
        _enemiesT = GameObject.Find("Enemies").transform;

        phase = Phase.Start;

        Destroy(GameObject.Find("IntroAudio"));
        GameTime.OriginalTimeScale = Time.timeScale;
    }

    private void Update() {
        if (GameTime.IsPaused)
            return;

        /* DEBUG */
        if (phase != Phase.End) {
            if (Input.GetKeyDown(KeyCode.F9)) {
                phase = Phase.End;
                KillAll();
                Victory();
            }

            if (Input.GetKeyDown(KeyCode.F12)) {
                phase = Phase.End;
                KillAll();
                Defeat();
            }  
        }
        
        switch (phase) {
            case Phase.Start: {
                /* Start Build Phase when pressing B key */
                if (Input.GetKeyDown(KeyCode.B))
                    StartCoroutine(BuildPhaseEvent());
                break;
            }
            case Phase.Build:
                /* Start Combat Phase when pressing B key */
                if (Input.GetKeyDown(KeyCode.B)) {
                    _phaseText.gameObject.SetActive(false);
                    StartCoroutine(NextWaveEvent("Wave  " + waveNr));
                }
        
                break;
        
            case Phase.Combat:
                switch (creatureNr) {
                    /* Spawn wave when creatureNr not init (-1) */
                    case -1: {
                        creatureNr = _waveCreatureNr;
                        StartCoroutine(portals[0].Wave());
                        StartCoroutine(portals[1].Wave());
                        break;
                    }
                    /* Wave Cleared when creatureNr reaches 0 */
                    case 0:
                        if (waveNr < 5) {
                            creatureNr = -2;
                            StartCoroutine(BuildPhaseEvent());
                        }
                        else {
                            /* TODO: Victory */
                            creatureNr = -2;
                            Victory();
                        }
                        
                        break;
                }
        
                break;
            
            case Phase.End:
                break;
        }
    }

    private IEnumerator BuildPhaseEvent() {
        switch (phase) {
            case Phase.Start:
                _phaseText.gameObject.SetActive(false);
                break;
            case Phase.Combat:
                _event.Show("Wave Cleared");
                _audio.PlayOneShot(clips[1]);
                _goldInfo.ChangeValue(RNG.GoldIncome[RNG.waveNr]);
                yield return new WaitForSeconds(4.0f);
                break;
        }
        
        _event.Show("Build Phase");
        _audio.PlayOneShot(clips[0]);
        yield return new WaitForSeconds(3);
        
        switch (phase) {
            case Phase.Start:
                _phaseText.text = "Press 'B' to start combat phase";
                break;
            case Phase.Combat:
                _phaseText.text = "Press 'B' to start next wave";
                waveNr++;
                RNG.waveNr++;
                break;
        }
        
        _waveCreatureNr = RNG.WaveCreatureNr();
        _mobCountText.text = _waveCreatureNr.ToString();

        _phaseText.gameObject.SetActive(true);
        phase = Phase.Build;
    }
    
    private IEnumerator NextWaveEvent(string text) {
        if (waveNr == 1) {
            _event.Show("Combat Phase");
            _audio.PlayOneShot(clips[0]);
            yield return new WaitForSeconds(3.2f);

            CanvasGroup waveCanvasGroup = _waveText.gameObject.GetComponentInParent<CanvasGroup>();
            CanvasGroup mobCanvasGroup = _mobCountText.gameObject.GetComponentInParent<CanvasGroup>();
            waveCanvasGroup.alpha = 1;
            mobCanvasGroup.alpha = 1;
        }
        else creatureNr = -1;

        _waveText.text = "Wave  " + waveNr + " / 5";
        _event.Show(text);
        if (waveNr != 1) 
            _audio.PlayOneShot(clips[0]);
        yield return new WaitForSeconds(3);
        phase = Phase.Combat;
    }

    public void UpdateMobCount() {
        _mobCountText.text = creatureNr.ToString();
    }

    private void KillAll() {
        waveNr = 5;
        //creatureNr = 0;
        _event.Show("");
        _waveText.text = "Wave 5 / 5";
        _phaseText.text = "";

        foreach (Transform enemy in _enemiesT) {
            CreatureInfo c = enemy.GetComponent<CreatureInfo>();

            if (c.IsAlive) 
                c.Kill();
        }
    }

    private void Victory() {
        victory.gameObject.SetActive(true);
    }

    public void Defeat() {
        defeat.gameObject.SetActive(true);
    }
}
