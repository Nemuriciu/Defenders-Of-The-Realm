using System.Collections;
using UnityEngine;

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
    private GameSystem _system;

    private void Start() {
        _parent = GameObject.Find("Enemies").transform;
        _spawnPos = transform.position - new Vector3(0, offsetY, 0);
        _system = GameObject.Find("System").GetComponent<GameSystem>();
    }

    /*private void Update() {
        if (Input.GetKeyDown(KeyCode.F1) && name.Equals("Portal_1")) {
            SpawnMob(spiderlings[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F2) && name.Equals("Portal_1")) {
            SpawnMob(turtles[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F3) && name.Equals("Portal_1")) {
            SpawnMob(skeletons[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F4) && name.Equals("Portal_1")) {
            SpawnMob(bats[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F5) && name.Equals("Portal_1")) {
            SpawnMob(mages[Random.Range(0, 3)]);
        }
        
        if (Input.GetKeyDown(KeyCode.F6) && name.Equals("Portal_1")) {
            SpawnMob(orcs[Random.Range(0, 3)]);
        }
    }*/

    public IEnumerator Wave() {
        ArrayList cList = RNG.WaveCreatureList();
        
        while (cList.Count > 0) {
            /* Extract one creature randomly from list */
            int r = Random.Range(0, cList.Count);
            string creature =  cList[r] as string;
            string affinity = RNG.GetResist(creature);
            cList.RemoveAt(r);

            /* Instantiate mob type */
            switch (creature) {
                case "Spiderling":
                    switch (affinity) {
                        case "None":
                            SpawnMob(spiderlings[0]);
                            break;
                        case "Physical":
                            SpawnMob(spiderlings[1]);
                            break;
                        case "Magical":
                            SpawnMob(spiderlings[2]);
                            break;
                    }
                    break;
                case "Turtle":
                    switch (affinity) {
                        case "None":
                            SpawnMob(turtles[0]);
                            break;
                        case "Physical":
                            SpawnMob(turtles[1]);
                            break;
                        case "Magical":
                            SpawnMob(turtles[2]);
                            break;
                    }
                    break;
                case "Skeleton":
                    switch (affinity) {
                        case "None":
                            SpawnMob(skeletons[0]);
                            break;
                        case "Physical":
                            SpawnMob(skeletons[1]);
                            break;
                        case "Magical":
                            SpawnMob(skeletons[2]);
                            break;
                    }
                    break;
                case "Bat":
                    switch (affinity) {
                        case "None":
                            SpawnMob(bats[0]);
                            break;
                        case "Physical":
                            SpawnMob(bats[1]);
                            break;
                        case "Magical":
                            SpawnMob(bats[2]);
                            break;
                    }
                    break;
                case "Mage":
                    switch (affinity) {
                        case "None":
                            SpawnMob(mages[0]);
                            break;
                        case "Physical":
                            SpawnMob(mages[1]);
                            break;
                        case "Magical":
                            SpawnMob(mages[2]);
                            break;
                    }
                    break;
                case "Orc":
                    switch (affinity) {
                        case "None":
                            SpawnMob(orcs[0]);
                            break;
                        case "Physical":
                            SpawnMob(orcs[1]);
                            break;
                        case "Magical":
                            SpawnMob(orcs[2]);
                            break;
                    }
                    break;
            }
    
            yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));

            if (_system.phase == Phase.End)
                break;
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
}