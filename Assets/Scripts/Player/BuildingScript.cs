using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public GameObject prefab;
    // public GameObject crossbowPrefab;
    // public GameObject crystalPrefab;
    // public GameObject hourglassPrefab;
    //
    // [Space(10)] 
    //
    // public GameObject crossbowBuild;
    // public GameObject crystalBuild;
    // public GameObject hourglassBuild;
    //
    // public List<int> costs;
    //
    // public AudioClip selectSound;
    // public AudioClip buildSound;
    //
    // private Transform _twrGroup;
     private GameObject _instance;
     private Vector3 _instancePos;
    // private int _selection;
    // private TowerBuild _towerBuild;
    // private PhaseScript _phaseScript;
    // private ErrorMessage _err;
    // private ProgressBar _buildBar;
    // private ProgressBar _twrLimit;
    // private AudioSource _audio;
     public bool IsBuilding { get; private set; }
    //
    // private void Start() {
    //     _twrGroup = GameObject.Find("Towers").transform;
    //     _err = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
    //     _buildBar = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();
    //     _twrLimit = GameObject.Find("TowerLimit").GetComponentInChildren<ProgressBar>();
    //     _phaseScript = GameObject.Find("EventSystem").GetComponent<PhaseScript>();
    //     _audio = GetComponent<AudioSource>();
    //
    //     switch (Info.selectedDifficulty) {
    //         case "Easy":
    //             Stats.maxTowers = 12;
    //             break;
    //         case "Normal":
    //             Stats.maxTowers = 25;
    //             break;
    //         case "Hard":
    //             Stats.maxTowers = 36;
    //             break;
    //         case "Very Hard":
    //             Stats.maxTowers = 48;
    //             break;
    //     }
    // }
    //
    //private void Update() {
    //
    //         /* Avoid confirmation between phases */
    //         if (_phaseScript.GetPhase() == PhaseScript.Phase.Loading) return;    
    //         
    //         /* Confirm construction */
    //         if (Input.GetMouseButtonUp(0)) {
    //             if (_towerBuild.IsValid()) {
    //                 /* Build Phase => Construction is instant */
    //                 if (_phaseScript.GetPhase() == PhaseScript.Phase.Build) {
    //                     switch (_selection) {
    //                         case 1:
    //                             Instantiate(crossbowPrefab, _instancePos, Quaternion.identity, _twrGroup);
    //                             Stats.activeTowers++;
    //                             _twrLimit.ChangeValue(1);
    //                             break;
    //                         case 2:
    //                             Instantiate(crystalPrefab, _instancePos, Quaternion.identity, _twrGroup);
    //                             Stats.activeTowers += 2;
    //                             _twrLimit.ChangeValue(2);
    //                             break;
    //                         case 3:
    //                             Instantiate(hourglassPrefab, _instancePos, Quaternion.identity, _twrGroup);
    //                             Stats.activeTowers += 2;
    //                             _twrLimit.ChangeValue(2);
    //                             break;
    //                     }
    //
    //                     _audio.clip = buildSound; 
    //                     _audio.Play();
    //                 } /* Wave Phase => Construction takes time */
    //                 else if (_phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
    //                     GameObject instance;
    //                     TowerBuild tb;
    //                     BuildAnimation bAnim;
    //                     
    //                     switch (_selection) {
    //                         case 1:
    //                             instance = Instantiate(crossbowBuild, _instancePos, 
    //                                 Quaternion.identity, _twrGroup);
    //                             tb = instance.GetComponent<TowerBuild>();
    //                             bAnim = instance.GetComponent<BuildAnimation>();
    //                             
    //                             tb.enabled = false;
    //                             bAnim.isActive = true;
    //                             
    //                             Stats.activeTowers++;
    //                             _twrLimit.ChangeValue(1);
    //                             break;
    //                         case 2:
    //                             instance = Instantiate(crystalBuild, _instancePos, 
    //                                 Quaternion.identity, _twrGroup);
    //                             tb = instance.GetComponent<TowerBuild>();
    //                             bAnim = instance.GetComponent<BuildAnimation>();
    //                             
    //                             tb.enabled = false;
    //                             bAnim.isActive = true;
    //                             
    //                             Stats.activeTowers += 2;
    //                             _twrLimit.ChangeValue(2);
    //                             break;
    //                         case 3:
    //                             instance = Instantiate(hourglassBuild, _instancePos, 
    //                                 Quaternion.identity, _twrGroup);
    //                             tb = instance.GetComponent<TowerBuild>();
    //                             bAnim = instance.GetComponent<BuildAnimation>();
    //                             
    //                             tb.enabled = false;
    //                             bAnim.isActive = true;
    //                             
    //                             Stats.activeTowers += 2;
    //                             _twrLimit.ChangeValue(2);
    //                             break;
    //                     }
    //                     
    //                     _audio.clip = buildSound;
    //                     _audio.Play();
    //                 }
    //                 
    //                 int cost = costs[_selection - 1];
    //                 Stats.PlayerGold -= cost;
    //                 _buildBar.ChangeValue(-cost);
    //                 
    //                 Destroy(_instance);
    //                 IsBuilding = false;
    //             }
    //             else _err.Show("Invalid Location.");
    //         }
    //     } else if (_phaseScript.GetPhase() == PhaseScript.Phase.Build ||
    //              _phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
    //         if (Input.GetKeyDown(KeyCode.Alpha1)) {
    //             if (_instance) Destroy(_instance);
    //             if (costs[0] > Stats.PlayerGold) {
    //                 _err.Show("Insufficient Gold.");
    //                 return;
    //             }
    //             
    //             if (Stats.activeTowers + 1 > Stats.maxTowers) {
    //                 _err.Show("Tower Limit Reached.");
    //                 return;
    //             }
    //         
    //             IsBuilding = true;
    //             _selection = 1;
    //             _instancePos = transform.position + (transform.forward * 5);
    //             _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
    //             _towerBuild = _instance.GetComponent<TowerBuild>();
    //
    //             _audio.clip = selectSound;
    //             _audio.Play();
    //         } 
    //         else if (Input.GetKeyDown(KeyCode.Alpha2)) {
    //             if (_instance) Destroy(_instance);
    //             if (costs[1] > Stats.PlayerGold) {
    //                 _err.Show("Insufficient Gold.");
    //                 return;
    //             }
    //             
    //             if (Stats.activeTowers + 2 > Stats.maxTowers) {
    //                 _err.Show("Tower Limit Reached.");
    //                 return;
    //             }
    //         
    //             IsBuilding = true;
    //             _selection = 2;
    //             _instancePos = transform.position + (transform.forward * 5);
    //             _instance = Instantiate(crystalBuild, _instancePos, Quaternion.identity, _twrGroup);
    //             _towerBuild = _instance.GetComponent<TowerBuild>();
    //             
    //             _audio.clip = selectSound;
    //             _audio.Play();
    //         } 
    //         else if (Input.GetKeyDown(KeyCode.Alpha3)) {
    //             if (_instance) Destroy(_instance);
    //             if (costs[2] > Stats.PlayerGold) {
    //                 _err.Show("Insufficient Gold.");
    //                 return;
    //             }
    //             
    //             if (Stats.activeTowers + 2 > Stats.maxTowers) {
    //                 _err.Show("Tower Limit Reached.");
    //                 return;
    //             }
    //         
    //             IsBuilding = true;
    //             _selection = 3;
    //             _instancePos = transform.position + (transform.forward * 5);
    //             _instance = Instantiate(hourglassBuild, _instancePos, Quaternion.identity, _twrGroup);
    //             _towerBuild = _instance.GetComponent<TowerBuild>();
    //             
    //             _audio.clip = selectSound;
    //             _audio.Play();
    //         }
    //     }
    // }

}