using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour {
    [Header("Creatures")]
    public GameObject[] frogman;
    public GameObject[] rabbid;
    public GameObject[] succubus;
    public GameObject[] ogre;
    public GameObject[] treant;
    public GameObject[] golem;
    [Space(15)]
    
    public float offsetY;
    public PortalTooltip portalTooltip;
    public Vector3[] waypoint1;
    public Vector3[] waypoint2;

    private Transform _parent;
    private Vector3 _spawnPos;
    private Vector3[] _path;

    private void Start() {
        _parent = GameObject.Find("Enemies").transform;
        _spawnPos = transform.position + new Vector3(0, offsetY, 0);
    }

    public IEnumerator Wave(int cNumber) {
        //Debug.Log(gameObject.name + ":  " + cNumber);
        
        while (cNumber > 0) {
            ArrayList creatures = new ArrayList();
            string creatureType = RNG.GenerateMob();
            string resist;
            float timer = 0;
            int count;
            
            switch (creatureType) {
                case "Frogman":
                    count = RNG.GenerateNr("Frogman");
                    count = count > cNumber ? cNumber : count;
                    cNumber -= count;
                    portalTooltip.SetText("Creatures: " + cNumber);

                    while (count > 0) {
                        resist = RNG.GenerateResist("Frogman");

                        switch (resist) {
                            case "None":
                                creatures.Add(frogman[0]);
                                break;
                            case "Physical":
                                creatures.Add(frogman[1]);
                                break;
                            case "Magical":
                                creatures.Add(frogman[2]);
                                break;
                        }

                        count--;
                    }

                    SpawnGroup(creatures);
                    timer = RNG.GetTimer("Frogman");
                    break;
                case "Rabbid":
                    count = RNG.GenerateNr("Rabbid");
                    count = count > cNumber ? cNumber : count;
                    cNumber -= count;
                    portalTooltip.SetText("Creatures: " + cNumber);

                    while (count > 0) {
                        resist = RNG.GenerateResist("Rabbid");

                        switch (resist) {
                            case "None":
                                creatures.Add(rabbid[0]);
                                break;
                            case "Physical":
                                creatures.Add(rabbid[1]);
                                break;
                            case "Magical":
                                creatures.Add(rabbid[2]);
                                break;
                        }

                        count--;
                    }

                    SpawnGroup(creatures);
                    timer = RNG.GetTimer("Rabbid");
                    break;
                case "Succubus":
                    count = RNG.GenerateNr("Succubus");
                    count = count > cNumber ? cNumber : count;
                    cNumber -= count;
                    portalTooltip.SetText("Creatures: " + cNumber);

                    while (count > 0) {
                        resist = RNG.GenerateResist("Succubus");

                        switch (resist) {
                            case "None":
                                creatures.Add(succubus[0]);
                                break;
                            case "Physical":
                                creatures.Add(succubus[1]);
                                break;
                            case "Magical":
                                creatures.Add(succubus[2]);
                                break;
                        }

                        count--;
                    }

                    SpawnGroup(creatures);
                    timer = RNG.GetTimer("Succubus");
                    break;
                case "Ogre":
                    count = RNG.GenerateNr("Ogre");
                    count = count > cNumber ? cNumber : count;
                    cNumber -= count;
                    portalTooltip.SetText("Creatures: " + cNumber);

                    while (count > 0) {
                        resist = RNG.GenerateResist("Ogre");

                        switch (resist) {
                            case "None":
                                creatures.Add(ogre[0]);
                                break;
                            case "Physical":
                                creatures.Add(ogre[1]);
                                break;
                            case "Magical":
                                creatures.Add(ogre[2]);
                                break;
                        }

                        count--;
                    }

                    SpawnGroup(creatures);
                    timer = RNG.GetTimer("Ogre");
                    break;
                case "Treant":
                    cNumber--;
                    portalTooltip.SetText("Creatures: " + cNumber);
                    resist = RNG.GenerateResist("Treant");

                    switch (resist) {
                        case "None":
                            SpawnMob(treant[0]);
                            break;
                        case "Physical":
                            SpawnMob(treant[1]);
                            break;
                        case "Magical":
                            SpawnMob(treant[2]);
                            break;
                    }

                    timer = RNG.GetTimer("Treant");
                    break;
                case "Golem":
                    cNumber--;
                    portalTooltip.SetText("Creatures: " + cNumber);
                    resist = RNG.GenerateResist("Golem");

                    switch (resist) {
                        case "Physical":
                            SpawnMob(golem[0]);
                            break;
                        case "Magical_1":
                            SpawnMob(golem[1]);
                            break;
                        case "Magical_2":
                            SpawnMob(golem[2]);
                            break;
                    }
                    
                    timer = RNG.GetTimer("Golem");
                    break;
            }

            yield return new WaitForSeconds(timer);
        }
    }

    private void SpawnMob(GameObject mob) {
        GameObject obj = Instantiate(mob, _spawnPos, transform.rotation, _parent);
        CreatureInfo cInfo = obj.GetComponent<CreatureInfo>();

        if (waypoint2.Length == 0)
            _path = waypoint1;
        else
            _path = Random.Range(0.0f, 1.0f) >= 0.5f ? waypoint1 : waypoint2;
        cInfo.SetWaypoints(_path);
    }

    private void SpawnGroup(IEnumerable mobs) {
        foreach (GameObject mob in mobs) {
            Vector3 randPos = new Vector3(
                _spawnPos.x + Random.Range(0.0f, 2f),
                _spawnPos.y,
                _spawnPos.z + Random.Range(-0.8f, 0.8f));
            
            GameObject obj = Instantiate(mob, randPos, transform.rotation, _parent);
            CreatureInfo cInfo = obj.GetComponent<CreatureInfo>();
        
            if (waypoint2.Length == 0)
                _path = waypoint1;
            else
                _path = Random.Range(0.0f, 1.0f) >= 0.5f ? waypoint1 : waypoint2;
            cInfo.SetWaypoints(_path);
        }
    }
}
