using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {
    public GameObject[] ballista;
    
    
    private Camera _cam;
    private Spot _spot;
    private ArrayList _spots;
    private GameObject _activeSpot;
    private GameObject _spiritTwr;
    private Tower _tower;
    private bool _canBuild;
    
    
    private const float Angle = 45.0f;

    private void Start() {
        _cam = Camera.main;
        _spots = new ArrayList();
    }
    
    private void Update() {
        /* Check if still facing the Tower Spot from last frame */
        if (_activeSpot) {
            if(!_spots.Contains(_activeSpot) ||
               Vector3.Angle(transform.forward, 
                   _activeSpot.transform.position - transform.position) > Angle ||
               _cam.transform.eulerAngles.x < 12 || _cam.transform.eulerAngles.x > 75) {
                _activeSpot = null;
                _spot = null;
                
                TurnOff();
            }
            
        }
        /* Check if facing any Tower Spot */
        else {
            foreach (GameObject spot in _spots) {
                if(Vector3.Angle(transform.forward, 
                       spot.transform.position - transform.position) < Angle &&
                   _cam.transform.eulerAngles.x > 12 && _cam.transform.eulerAngles.x < 75) {
                    _activeSpot = spot;
                    _spot = _activeSpot.GetComponent<Spot>();
                    
                    TurnOn();
                    break;
                }
            }
        }

        /* Allow user interactions */
        if (_canBuild) {
            if (Input.GetKeyDown(KeyCode.B) && !_spot.HasTower()) {
                _spot.AddTower(ballista[0], _spiritTwr);
                _tower = _spot.GetTower();
            }
            else if (Input.GetKeyDown(KeyCode.N) && _spot.HasTower()) {
                _spot.RemoveTower();
            }
        }
    }

    private void TurnOn() {
        // TODO: Turn UI on
        
        _canBuild = true;
        
        if (!_spot.HasTower()) {
            _spiritTwr = _spot.AddSpirit();
            
        }
        else {
            _tower = _spot.GetTower();
            _tower.SetOutline(true);
        }
    }

    private void TurnOff() {
        // TODO: Turn UI off
        
        _canBuild = false;
        
        if (_spiritTwr)
            Destroy(_spiritTwr);
        else {
            if (_tower) {
                _tower.SetOutline(false);
                _tower = null;    
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TowerSpot")) {
            _spots.Add(other.gameObject);
        }
            
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("TowerSpot")){
            _spots.Remove(other.gameObject);
        }
    }
}
