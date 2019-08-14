﻿using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public GameObject crossbowPrefab;
    public GameObject crystalPrefab;
    public GameObject hourglassPrefab;
    
    [Space(10)] 
    
    public GameObject crossbowBuild;
    public GameObject crystalBuild;
    public GameObject hourglassBuild;
    
    public List<int> costs;
    
    public AudioClip selectSound;
    public AudioClip buildSound;
    
    private Transform _twrGroup;
    private GameObject _instance;
    private Vector3 _instancePos;
    private int _selection;
    private TowerBuild _towerBuild;
    private PhaseScript _phaseScript;
    private ErrorMessage _err;
    private ProgressBar _slider;
    private AudioSource _audio;
    public bool IsBuilding { get; private set; }
    
    private void Start() {
        _twrGroup = GameObject.Find("Towers").transform;
        _err = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        _slider = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();
        _phaseScript = GameObject.Find("EventSystem").GetComponent<PhaseScript>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update() {
        /* RMB cancels current construction */
        if (Input.GetMouseButton(1)) {
            if (_instance) {
                IsBuilding = false;
                Destroy(_instance);
            }
        }
        
        /* Update tower position each frame */
        if (IsBuilding) {
            _instancePos = transform.position + (transform.forward * 5);

            Ray ray = new Ray(_instancePos + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 20, 1 << 1)) {
                if (hit.collider) {
                    _instancePos = new Vector3(_instancePos.x, hit.point.y, _instancePos.z);
                }
            }

            _instance.transform.position = _instancePos;
            _instance.transform.rotation = transform.rotation;

            /* Avoid confirmation between phases */
            if (_phaseScript.GetPhase() == PhaseScript.Phase.Loading) return;    
            
            /* Confirm construction */
            if (Input.GetMouseButtonUp(0)) {
                if (_towerBuild.IsValid()) {
                    /* Build Phase => Construction is instant */
                    if (_phaseScript.GetPhase() == PhaseScript.Phase.Build) {
                        switch (_selection) {
                            case 1:
                                Instantiate(crossbowPrefab, _instancePos, Quaternion.identity, _twrGroup);
                                break;
                            case 2:
                                Instantiate(crystalPrefab, _instancePos, Quaternion.identity, _twrGroup);
                                break;
                            case 3:
                                Instantiate(hourglassPrefab, _instancePos, Quaternion.identity, _twrGroup);
                                break;
                        }

                        _audio.clip = buildSound; 
                        _audio.Play();
                    } /* Wave Phase => Construction takes time */
                    else if (_phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
                        GameObject instance;
                        TowerBuild tb;
                        BuildAnimation bAnim;
                        
                        switch (_selection) {
                            case 1:
                                instance = Instantiate(crossbowBuild, _instancePos, 
                                    Quaternion.identity, _twrGroup);
                                tb = instance.GetComponent<TowerBuild>();
                                bAnim = instance.GetComponent<BuildAnimation>();
                                
                                tb.enabled = false;
                                bAnim.isActive = true;
                                break;
                            case 2:
                                instance = Instantiate(crystalBuild, _instancePos, 
                                    Quaternion.identity, _twrGroup);
                                tb = instance.GetComponent<TowerBuild>();
                                bAnim = instance.GetComponent<BuildAnimation>();
                                
                                tb.enabled = false;
                                bAnim.isActive = true;
                                break;
                            case 3:
                                instance = Instantiate(hourglassBuild, _instancePos, 
                                    Quaternion.identity, _twrGroup);
                                tb = instance.GetComponent<TowerBuild>();
                                bAnim = instance.GetComponent<BuildAnimation>();
                                
                                tb.enabled = false;
                                bAnim.isActive = true;
                                break;
                        }
                        
                        _audio.clip = buildSound;
                        _audio.Play();
                    }
                    
                    int cost = costs[_selection - 1];
                    Stats.PlayerGold -= cost;
                    _slider.ChangeValue(-cost);
                    
                    Destroy(_instance);
                    IsBuilding = false;
                }
                else _err.Show("Invalid Location.");
            }
        } else if (_phaseScript.GetPhase() == PhaseScript.Phase.Build ||
                 _phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (_instance) Destroy(_instance);
                if (costs[0] > Stats.PlayerGold) {
                    _err.Show("Insufficient Gold.");
                    return;
                }
            
                IsBuilding = true;
                _selection = 1;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();

                _audio.clip = selectSound;
                _audio.Play();
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                if (_instance) Destroy(_instance);
                if (costs[1] > Stats.PlayerGold) {
                    _err.Show("Insufficient Gold.");
                    return;
                }
            
                IsBuilding = true;
                _selection = 2;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(crystalBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();
                
                _audio.clip = selectSound;
                _audio.Play();
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                if (_instance) Destroy(_instance);
                if (costs[2] > Stats.PlayerGold) {
                    _err.Show("Insufficient Gold.");
                    return;
                }
            
                IsBuilding = true;
                _selection = 3;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(hourglassBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();
                
                _audio.clip = selectSound;
                _audio.Play();
            }
        }
    }

}