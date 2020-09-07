using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthbarRoot : MonoBehaviour {
    public List<Transform> healthBars;
    private Camera _cam;
    private Transform _camT;     
	
    private void Start () {
        healthBars = new List<Transform>();
        _cam = Camera.main;
        
        if (_cam != null) 
            _camT = _cam.transform;

        for (int i = 0; i < transform.childCount; i++)
            healthBars.Add(transform.GetChild(i));
	}
	
	private void Update () {
        healthBars.Sort(DistanceCompare);

        for(int i = 0; i < healthBars.Count; i++)
            healthBars[i].SetSiblingIndex(healthBars.Count - (i + 1));
	}

    private int DistanceCompare(Transform a, Transform b) {
        return Mathf.Abs((WorldPos(a.position) - _camT.position).sqrMagnitude).
            CompareTo(Mathf.Abs((WorldPos(b.position) - _camT.position).sqrMagnitude));
    }

    private Vector3 WorldPos(Vector3 pos) {
        return _cam.ScreenToWorldPoint(pos);
    }
}
