using System.Collections;
using UnityEngine;

public class WaveSpawn : MonoBehaviour {
    public Vector3 spawnPoint;
    public WaveCount waveScript;
    public GameObject footmanPrefab;
    public GameObject lichPrefab;
    public GameObject gruntPrefab;

    private bool _flag, _waveBreak, _newWave;
    private int _mobCount;
    private Transform _parent;
    private ArrayList _mobPool;
    private EventMessage _eventMessage;

    private int _footmanHealth = 250;
    private int _lichHealth = 800;
    private int _gruntHealth = 1750;
    
    private void Start() {
        _parent = GameObject.Find("Creatures").transform;
        _eventMessage = GameObject.Find("EventBox").GetComponent<EventMessage>();
        _mobPool = new ArrayList();
    }

    private void Update() {
        if (!_flag || _waveBreak) return;
        
        if (waveScript.enemyCount == 0) {
            /* Victory */
            if (waveScript.currentWave == waveScript.maxWave) {
                // TODO:
                return;
            }
            
            /* After Wave Break */
            if (!_newWave) {
                _waveBreak = true;
                StartCoroutine(WaveSwitch());
                return;
            }

            waveScript.currentWave++;
            _newWave = false;
            
            /* Generate wave */
            switch (waveScript.currentWave) {
                case 1:
                    GenerateWave1();
                    break;
                case 2:
                    GenerateWave2();
                    break;
                case 3:
                    GenerateWave3();
                    break;
                case 4:
                    GenerateWave4();
                    break;
                case 5:
                    GenerateWave5();
                    break;
            }
            
            waveScript.enemyCount = _mobCount;
            waveScript.Display();
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator WaveSwitch() {
        _eventMessage.Show("Wave Cleared");
        
        yield return new WaitUntil(() => !_eventMessage.isActive);
        
        yield return new WaitForSeconds(3);
        
        _eventMessage.Show("Wave " + (waveScript.currentWave + 1));
        
        yield return new WaitUntil(() => !_eventMessage.isActive);

        _waveBreak = false;
        _newWave = true;
    }
    
    private IEnumerator Spawn() {
        for (int i = 0; i < _mobCount; i++) {
            int ix = Random.Range(0, _mobPool.Count);
            string enemyType = _mobPool[ix] as string;
            _mobPool.Remove(ix);

            GameObject instance;
            Damager damager;

            switch (enemyType) {
                case "Footman":
                    instance = Instantiate(footmanPrefab, spawnPoint,
                        Quaternion.Euler(0, 210, 0), _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _footmanHealth;
                    break;
                
                case "Lich":
                    instance = Instantiate(lichPrefab, spawnPoint,
                        Quaternion.Euler(0, 210, 0), _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _lichHealth;
                    break;
                
                case "Grunt":
                    instance = Instantiate(gruntPrefab, spawnPoint,
                        Quaternion.Euler(0, 210, 0), _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _gruntHealth;
                    break;
            }
            
            yield return new WaitForSeconds(Random.Range(1.0f, 6.0f));
        }
    }
    
    public void Begin() {
        _flag = true;
        _newWave = true;
    }

    /* TODO: Change values based on difficulty (multiple lanes) */
    private void GenerateWave1() {
        _mobCount = 30;

        const int footmanCount = 26;
        const int lichCount = 4;

        for (int i = 0; i < footmanCount; i++)
            _mobPool.Add("Footman");
        for (int i = 0; i < lichCount; i++)
            _mobPool.Add("Lich");
    }
    
    private void GenerateWave2() {
        _mobCount = 42;

        const int footmanCount = 34;
        const int lichCount = 8;

        for (int i = 0; i < footmanCount; i++)
            _mobPool.Add("Footman");
        for (int i = 0; i < lichCount; i++)
            _mobPool.Add("Lich");

        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.15f);
    }
    
    private void GenerateWave3() {
        _mobCount = 50;

        const int footmanCount = 36;
        const int lichCount = 12;
        const int gruntCount = 2;

        for (int i = 0; i < footmanCount; i++)
            _mobPool.Add("Footman");
        for (int i = 0; i < lichCount; i++)
            _mobPool.Add("Lich");
        for (int i = 0; i < gruntCount; i++)
            _mobPool.Add("Grunt");
        
        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.15f);
        _lichHealth += Mathf.RoundToInt(_lichHealth * 0.15f);
    }
    
    private void GenerateWave4() {
        _mobCount = 55;

        const int footmanCount = 30;
        const int lichCount = 20;
        const int gruntCount = 5;

        for (int i = 0; i < footmanCount; i++)
            _mobPool.Add("Footman");
        for (int i = 0; i < lichCount; i++)
            _mobPool.Add("Lich");
        for (int i = 0; i < gruntCount; i++)
            _mobPool.Add("Grunt");
        
        _gruntHealth += Mathf.RoundToInt(_gruntHealth * 0.25f);
    }
    
    private void GenerateWave5() {
        _mobCount = 62;

        const int footmanCount = 30;
        const int lichCount = 24;
        const int gruntCount = 8;

        for (int i = 0; i < footmanCount; i++)
            _mobPool.Add("Footman");
        for (int i = 0; i < lichCount; i++)
            _mobPool.Add("Lich");
        for (int i = 0; i < gruntCount; i++)
            _mobPool.Add("Grunt");
        
        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.15f);
        _lichHealth += Mathf.RoundToInt(_lichHealth * 0.25f);
    }
}
