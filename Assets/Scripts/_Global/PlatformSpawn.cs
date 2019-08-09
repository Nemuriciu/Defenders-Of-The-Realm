using System.Collections;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {
    public GameObject[] portals;
    
    private ArrayList _platforms;
    private ArrayList _activePortals;
    private WaveSpawn _waveSpawn;
    private int _portalCount;

    private void Start() {
        _platforms = new ArrayList();
        _activePortals = new ArrayList();
        _waveSpawn = GetComponent<WaveSpawn>();

        foreach (var t in portals)
            _platforms.Add(t);

        SetPortalCount();
        SpawnPlatforms();
        
        ArrayList spawnPoints = new ArrayList();

        foreach (GameObject portal in _activePortals)
            spawnPoints.Add(portal.transform);

        _waveSpawn.UpdateSpawnPoints(spawnPoints);
    }
    
    private void SetPortalCount() {
        switch (Info.selectedDifficulty) {
            case "Easy":
                _portalCount = 1;
                break;
            case "Normal":
                _portalCount = 2;
                break;
            case "Hard":
                _portalCount = 3;
                break;
            case "Very Hard":
                _portalCount = 5;
                break;
        }
    }

    private void SpawnPlatforms() {
        for (int i = 0; i < _portalCount; i++) {
            int ix = Random.Range(0, _platforms.Count);
            Debug.Log(ix);
            GameObject platform = _platforms[ix] as GameObject;
            
            _platforms.RemoveAt(ix);
            _activePortals.Add(platform);

            if (platform != null) platform.SetActive(true);
        }
    }
}
