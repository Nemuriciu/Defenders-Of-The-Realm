using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject creature;
    public float offsetY;
    public Vector3[] waypoint_1;
    public Vector3[] waypoint_2;

    private Transform _parent;
    private Vector3 _spawnPos;
    private Vector3[] _path;

    private void Start() {
        _parent = GameObject.Find("Enemies").transform;
        _spawnPos = transform.position + new Vector3(0, offsetY, 0);

        // TODO:
        _path = waypoint_1;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T) && _path.Length > 0) {
            SpawnMob(creature);
        }
    }

    public void SpawnMob(GameObject mob) {
        GameObject obj = Instantiate(mob, _spawnPos, transform.rotation, _parent);
        CreatureInfo cInfo = obj.GetComponent<CreatureInfo>();
        cInfo.SetWaypoints(_path);
    }
}
