﻿using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public GameObject crossbowPrefab, crossbowBuild;
    public List<int> costs;
    
    private Transform _twrGroup;
    private GameObject _instance;
    private Vector3 _instancePos;
    private int _selection;
    private bool _isBuilding;
    
    private TowerBuild _towerBuild;
    private PhaseScript _phaseScript;
    private ErrorMessage _err;
    private ProgressBar _slider;
    
    private void Start() {
        _twrGroup = GameObject.Find("Towers").transform;
        _err = GameObject.Find("ErrorBox").GetComponent<ErrorMessage>();
        _slider = GameObject.Find("BuildBar").GetComponentInChildren<ProgressBar>();
        _phaseScript = GameObject.Find("EventSystem").GetComponent<PhaseScript>();
    }

    private void Update() {
        /* RMB cancels current construction */
        if (Input.GetMouseButton(1)) {
            if (_instance) {
                _isBuilding = false;
                Destroy(_instance);
            }
        }
        
        /* Update tower position each frame */
        if (_isBuilding) {
            _instancePos = transform.position + (transform.forward * 5);

            Ray ray = new Ray(_instancePos + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 20, 1 << 1)) {
                if (hit.collider) {
                    _instancePos = new Vector3(_instancePos.x, hit.point.y, _instancePos.z);
                }
            }

            _instance.transform.position = _instancePos;

            /* Avoid confirmation between phases */
            if (_phaseScript.GetPhase() == PhaseScript.Phase.Loading) return;    
            
            /* Confirm construction */
            if (Input.GetMouseButton(0)) {
                if (_towerBuild.IsValid()) {
                    /* Build Phase => Construction is instant */
                    if (_phaseScript.GetPhase() == PhaseScript.Phase.Build) {
                        switch (_selection) {
                            case 1:
                                Instantiate(crossbowPrefab, _instancePos, Quaternion.identity, _twrGroup);
                                break;
                            //TODO: case 2,3 
                        }
                    } /* Wave Phase => Construction takes time */
                    else if (_phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
                        switch (_selection) {
                            case 1:
                                GameObject o = Instantiate(crossbowBuild, _instancePos, 
                                    Quaternion.identity, _twrGroup);
                                TowerBuild tb = o.GetComponent<TowerBuild>();
                                BuildAnimation bAnim = o.GetComponent<BuildAnimation>();
                                
                                tb.enabled = false;
                                bAnim.isActive = true;
                                break;
                            //TODO: case 2,3 
                        }
                    }
                    
                    
                    _isBuilding = false;
                    Destroy(_instance);

                    int cost = costs[_selection - 1];
                    Stats.PlayerGold -= cost;
                    _slider.ChangeValue(-cost);
                }
                else _err.Show("Invalid location.");
            }
        } else if (_phaseScript.GetPhase() == PhaseScript.Phase.Build ||
                 _phaseScript.GetPhase() == PhaseScript.Phase.Wave) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                if (_instance) Destroy(_instance);
                if (costs[0] > Stats.PlayerGold) {
                    _err.Show("Insufficient gold.");
                    return;
                }
            
                _isBuilding = true;
                _selection = 1;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                /*
                if (_instance) Destroy(_instance);
                if (costs[1] > Stats.PlayerGold) {
                    _err.Show("Insufficient gold.");
                    return;
                }
            
                _isBuilding = true;
                _selection = 2;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();
                */
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                /*
                if (_instance) Destroy(_instance);
                if (costs[1] > Stats.PlayerGold) {
                    _err.Show("Insufficient gold.");
                    return;
                }
            
                _isBuilding = true;
                _selection = 2;
                _instancePos = transform.position + (transform.forward * 5);
                _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
                _towerBuild = _instance.GetComponent<TowerBuild>();
                */
            }
        }
    }
}
