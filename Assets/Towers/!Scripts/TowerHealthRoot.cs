using System.Collections.Generic;
using UnityEngine;

public class TowerHealthRoot : MonoBehaviour {
    public List<GameObject> pool;
    public GameObject prefab;
    
    private void Start() {
        pool = new List<GameObject>();
        
        for (int i = 0; i < 50; i++) {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false); 
            pool.Add(obj);
        }
    }
}
