using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public GameObject crossbowPrefab, crossbowBuild;

    private Transform _twrGroup;
    private GameObject _instance;
    private TowerBuild _towerBuild;
    private Vector3 _instancePos;
    private bool _isBuilding;
    

    private void Start() {
        _twrGroup = GameObject.Find("Towers").transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_instance) {
                _isBuilding = false;
                Destroy(_instance);
            }
        }
        
        if (_isBuilding) {
            _instancePos = transform.position + (transform.forward * 5);

            Ray ray = new Ray(_instancePos + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 20, 1 << 1)) {
                if (hit.collider) {
                    _instancePos = new Vector3(_instancePos.x, hit.point.y, _instancePos.z);
                }
            }

            _instance.transform.position = _instancePos;
            
            if (Input.GetMouseButton(0)) {
                if (_towerBuild.IsValid()) {
                    Instantiate(crossbowPrefab, _instancePos, Quaternion.identity, _twrGroup);
                    _isBuilding = false;
                    Destroy(_instance);
                }

                //TODO: Else Print Error Message on Screen 
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _isBuilding = true;
            _instancePos = transform.position + (transform.forward * 5);
            _instance = Instantiate(crossbowBuild, _instancePos, Quaternion.identity, _twrGroup);
            _towerBuild = _instance.GetComponent<TowerBuild>();
        }

    }
}
