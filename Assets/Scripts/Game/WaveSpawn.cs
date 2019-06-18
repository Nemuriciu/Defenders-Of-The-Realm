using System.Collections;
using UnityEngine;

public class WaveSpawn : MonoBehaviour {
    public Vector3 spawnPoint;
    //public GameObject checkpoint;
    public GameObject prefab;

    private bool _flag;

    // Update is called once per frame
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.F1)) return;
        
        _flag = !_flag;
        Debug.Log("Spawning is " + _flag);

        if (_flag)
            StartCoroutine(nameof(Spawn));
    }
    
    private IEnumerator Spawn() {
        while (_flag) {
            Instantiate(prefab, spawnPoint, Quaternion.Euler(0, 210, 0), transform);
            yield return new WaitForSeconds(3);
        }
    }
}
