using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour {
    [Header("Creatures")] 
    public GameObject[] spiderlings;
    public GameObject[] turtles;
    public GameObject[] skeletons;
    public GameObject[] bats;
    public GameObject[] mages;
    public GameObject[] orcs;
    [Space(15)]
    
    public float offsetY;
    public PortalTooltip portalTooltip;
    public Vector3[] waypoint;

    private Transform _parent;
    private Vector3 _spawnPos;

    private void Start() {
        _parent = GameObject.Find("Enemies").transform;
        _spawnPos = transform.position - new Vector3(0, offsetY, 0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1) && name.Equals("Portal_1")) {
            SpawnMob(skeletons[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F2) && name.Equals("Portal_1")) {
            SpawnMob(spiderlings[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F3) && name.Equals("Portal_1")) {
            SpawnMob(turtles[Random.Range(0, 3)]);
        }
    }

    public IEnumerator Wave(int cNumber) {
        ArrayList cList = RNG.WaveCreatureList();
        
        while (cList.Count > 0) {
            /* Extract one creature randomly from list */
            int r = Random.Range(0, cList.Count);
            string creature =  cList[r] as string;
            cList.RemoveAt(r);

            /* Instantiate mob type */
            switch (creature) {
                case "Spiderling":
                    SpawnMob(spiderlings[0]);
                    break;
                case "Turtle":
                    SpawnMob(turtles[0]);
                    break;
                case "Skeleton":
                    SpawnMob(skeletons[0]);
                    break;
                case "Bat":
                    SpawnMob(bats[0]);
                    break;
                case "Mage":
                    SpawnMob(mages[0]);
                    break;
                case "Orc":
                    SpawnMob(orcs[0]);
                    break;
            }
    
            yield return new WaitForSeconds(Random.Range(0.25f, 2.0f));
        }
    }

    private void SpawnMob(GameObject mob) {
        Vector3 randPos = new Vector3(
            _spawnPos.x,
            _spawnPos.y,
            _spawnPos.z + Random.Range(-0.3f, 0.3f));
        
        GameObject obj = Instantiate(mob, randPos, transform.rotation, _parent);
        CreatureInfo cInfo = obj.GetComponent<CreatureInfo>();
        
        cInfo.SetWaypoint(waypoint);
    }

    /*private void SpawnGroup(IEnumerable mobs) {
        foreach (GameObject mob in mobs) {
            Vector3 randPos = new Vector3(
                _spawnPos.x,
                _spawnPos.y,
                _spawnPos.z + Random.Range(-0.3f, 0.3f));
            
            GameObject obj = Instantiate(mob, randPos, transform.rotation, _parent);
            CreatureInfo cInfo = obj.GetComponent<CreatureInfo>();
        
            cInfo.SetWaypoint(waypoint);
        }
    }*/
}
