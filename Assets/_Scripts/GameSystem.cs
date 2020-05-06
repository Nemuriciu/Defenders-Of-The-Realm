using System.Collections;
using TMPro;
using UnityEngine;

public enum Phase {
    Start,
    Build,
    Combat
};

public class GameSystem : MonoBehaviour {
    public Spawn[] portals;
    public AudioClip[] clips;
    
    [HideInInspector] 
    public Phase phase;
    [HideInInspector] 
    public int waveNr = 1;
    [HideInInspector] 
    public int creatureNr = -1;
    
    //private ErrorMessage _error;
    //private EventMessage _event;
    private AudioSource _audio;
    //private TextMeshProUGUI _phaseText;
    //private TextMeshProUGUI _waveText;
    //private GoldInfo _goldInfo;
    private int[] _waveCreatureNr;
    
    private void Start() {
        //_error = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        //_audio = GetComponent<AudioSource>();
        //_event = GameObject.Find("EventBox").GetComponent<EventMessage>();
        //_phaseText = GameObject.Find("PhaseBox").GetComponent<TextMeshProUGUI>();
        //_waveText = GameObject.Find("WaveInfo").GetComponentInChildren<TextMeshProUGUI>();
        //_goldInfo = GameObject.Find("GoldInfo").GetComponent<GoldInfo>();

        // phase = Phase.Start;
        // _audio.clip = clips[0];
        // _audio.loop = true;
        // _audio.Play();
    }

    private void Update() {
        // switch (phase) {
        //     case Phase.Start: {
        //         /* Start Build Phase when pressing B key */
        //         if (Input.GetKeyDown(KeyCode.B))
        //             StartCoroutine(BuildPhaseEvent());
        //         break;
        //     }
        //     case Phase.Build:
        //         /* Start Combat Phase when pressing B key */
        //         if (Input.GetKeyDown(KeyCode.B)) {
        //             _phaseText.gameObject.SetActive(false);
        //             StartCoroutine(NextWaveEvent("Wave  " + waveNr));
        //         }
        //
        //         break;
        //
        //     case Phase.Combat:
        //         switch (creatureNr) {
        //             /* Spawn wave when creatureNr not init (-1) */
        //             case -1: {
        //                 creatureNr = _waveCreatureNr[0];
        //                 StartCoroutine(portals[0].Wave(_waveCreatureNr[1]));
        //                 StartCoroutine(portals[1].Wave(_waveCreatureNr[2]));
        //                 StartCoroutine(portals[2].Wave(_waveCreatureNr[3]));
        //                 break;
        //             }
        //             /* Wave Cleared when creatureNr reaches 0 */
        //             case 0:
        //                 if (waveNr < 5) {
        //                     creatureNr = -2;
        //                     StartCoroutine(BuildPhaseEvent());
        //                 }
        //                 else {
        //                     /* TODO: Victory */
        //                     creatureNr = -2;
        //                     Debug.Log("Victory");
        //                 }
        //
        //                 break;
        //         }
        //
        //         break;
        // }
    }

    /*private IEnumerator BuildPhaseEvent() {
        switch (phase) {
            case Phase.Start:
                _phaseText.gameObject.SetActive(false);
                _goldInfo.SetVisible();
                break;
            case Phase.Combat:
                _event.Show("Wave Cleared");
                yield return new WaitForSeconds(4.0f);
                break;
        }
        
        _event.Show("Build Phase");
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
        
        /* Set creatureNr and portal tooltips #1#
        _waveCreatureNr = RNG.GenerateWaveCreatureNr();
        portals[0].portalTooltip.SetText("Creatures: " + _waveCreatureNr[1]);
        portals[1].portalTooltip.SetText("Creatures: " + _waveCreatureNr[2]);
        portals[2].portalTooltip.SetText("Creatures: " + _waveCreatureNr[3]);

        _phaseText.gameObject.SetActive(true);
        phase = Phase.Build;
    }*/
    
    /*private IEnumerator NextWaveEvent(string text) {
        if (waveNr == 1) {
            _event.Show("Combat Phase");
            yield return new WaitForSeconds(3.2f);

            CanvasGroup cg = _waveText.gameObject.GetComponentInParent<CanvasGroup>();
            cg.alpha = 1;
            cg.interactable = true;
        }
        else creatureNr = -1;

        _waveText.text = "Wave  " + waveNr + " / 5";
        _event.Show(text);
        yield return new WaitForSeconds(3);
        phase = Phase.Combat;
    }*/
}
