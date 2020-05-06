using UnityEngine;

public class Building : MonoBehaviour {
    public Texture2D[] cursor;
    public GameObject[] blueprints;
    public GameObject[] prefabs;

    public bool IsBuilding { get; private set; }
    public int TowerId { get; private set; } = -1;
    public Spot SpotInstance { get; set; }

    private void Update() {
        /* Close building mode on RMB */
        if (IsBuilding && (Input.GetMouseButton(1) || 
                           Input.GetKeyDown(KeyCode.Escape))) {
            if (SpotInstance && !SpotInstance.HasTower())
                ResetBlueprints();
            
            TowerId = -1;
            IsBuilding = false;
            Cursor.SetCursor(cursor[0], Vector2.zero, CursorMode.Auto);
            return;
        }
        
        /* Ballista - 1 */
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            TowerId = 0;
            
            if (!IsBuilding) {
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                IsBuilding = true;
            }

            if (SpotInstance && !SpotInstance.HasTower()) {
                ResetBlueprints();
                SetBlueprint(TowerId, SpotInstance.SpawnPos);
            }
                
            return;
        }
        
        /* Crystal - 2 */
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TowerId = 1;
            
            if (!IsBuilding) {
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                IsBuilding = true;
            }

            if (SpotInstance && !SpotInstance.HasTower()) {
                ResetBlueprints();
                SetBlueprint(TowerId, SpotInstance.SpawnPos);
            }
                
            return;
        }
        
        /* Pyro - 3 */
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TowerId = 2;
            
            if (!IsBuilding) {
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                IsBuilding = true;
            }

            if (SpotInstance && !SpotInstance.HasTower()) {
                ResetBlueprints();
                SetBlueprint(TowerId, SpotInstance.SpawnPos);
            }
                
            return;
        }
        
        /* Dark - 4 */
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            TowerId = 3;
            
            if (!IsBuilding) {
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                IsBuilding = true;
            }

            if (SpotInstance && !SpotInstance.HasTower()) {
                ResetBlueprints();
                SetBlueprint(TowerId, SpotInstance.SpawnPos);
            }
                
            return;
        }
        
        /* Hourglass - 5 */
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            TowerId = 4;
            
            if (!IsBuilding) {
                Cursor.SetCursor(cursor[1], Vector2.zero, CursorMode.Auto);
                IsBuilding = true;
            }

            if (SpotInstance && !SpotInstance.HasTower()) {
                ResetBlueprints();
                SetBlueprint(TowerId, SpotInstance.SpawnPos);
            }
        }
    }

    public void SetBlueprint(int id, Vector3 pos) {
        blueprints[id].transform.position = pos;
        blueprints[id].SetActive(true);
    }

    public void ResetBlueprints() {
        foreach (var bp in blueprints) {
            bp.transform.position = Vector3.zero;
            bp.SetActive(false);
        }
    }

    public void Reset() {
        TowerId = -1;
        IsBuilding = false;
        Cursor.SetCursor(cursor[0], Vector2.zero, CursorMode.Auto);
    }
}
