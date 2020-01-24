using UnityEngine;

public class Spot : MonoBehaviour {
    public GameObject spiritTower;
    
    private GameObject _tower;
    private const float OffsetY = 0.6384f;
    
    public GameObject AddSpirit() {
        return Instantiate(spiritTower, 
            new Vector3(transform.position.x, OffsetY, transform.position.z), 
            Quaternion.identity, transform);
    }
    
    public void AddTower(GameObject tower, GameObject spirit) {
        Destroy(spirit);
        
        _tower = Instantiate(tower, 
            new Vector3(transform.position.x, OffsetY, transform.position.z), 
            Quaternion.identity, transform);
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
