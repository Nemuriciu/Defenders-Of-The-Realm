using UnityEngine;

public class Spot : MonoBehaviour {
    private Camera _cam;
    private GameObject _tower;
    private const float OffsetY = 0.507f;

    private void Start() {
        _cam = Camera.main;
    }

    public GameObject AddBlueprint(GameObject blueprint) {
        return Instantiate(blueprint, 
            new Vector3(transform.position.x, OffsetY, transform.position.z), 
            Quaternion.LookRotation(_cam.transform.right) * Quaternion.Euler(0,40,0),
            transform);
    }
    
    public void AddTower(GameObject tower) {
        if (_tower) 
            RemoveTower();
        
        _tower = Instantiate(tower, 
            new Vector3(transform.position.x, OffsetY, transform.position.z), 
            Quaternion.LookRotation(_cam.transform.right) * Quaternion.Euler(0,40,0),
            transform);
    }

    public void RemoveTower() {
        Destroy(_tower);
    }

    public bool HasTower() {
        return _tower;
    }

    public Tower GetTower() {
        return HasTower() ? _tower.GetComponent<Tower>() : null;
    }
}
