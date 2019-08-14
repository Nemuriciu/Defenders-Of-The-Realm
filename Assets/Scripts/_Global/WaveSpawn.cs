using System.Collections;
using UnityEngine;

public class WaveSpawn : MonoBehaviour {
    public WaveCount waveScript;
    public GameObject footmanPrefab;
    public GameObject lichPrefab;
    public GameObject gruntPrefab;

    private bool _flag, _waveBreak, _newWave, _victory;
    private int _mobCount;
    private Transform _parent;
    private ArrayList _mobPool;
    private ArrayList _spawnPoints;
    private EventMessage _eventMessage;
    private StatsMenu _statsMenu;

    private int _footmanHealth = 250;
    private int _lichHealth = 800;
    private int _gruntHealth = 1750;
    private float _monsterSpeed = 3.0f;
    
    private void Start() {
        _parent = GameObject.Find("Creatures").transform;
        _eventMessage = GameObject.Find("EventBox").GetComponent<EventMessage>();
        _statsMenu = GameObject.Find("Canvas").GetComponent<StatsMenu>();
        _mobPool = new ArrayList();
    }

    private void Update() {
        if (!_flag || _waveBreak || _victory) return;
        
        if (waveScript.enemyCount == 0) {
            
            /* Victory */
            if (waveScript.currentWave == waveScript.maxWave) {
                _victory = true;
                StartCoroutine(Victory());
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
            
            waveScript.enemyCount = _mobCount * _spawnPoints.Count;
            waveScript.Display();

            foreach (Transform spawnPoint in _spawnPoints)
                StartCoroutine(Spawn(spawnPoint));
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
    
    private IEnumerator Spawn(Transform spawnPoint) {
        ArrayList mobPool = _mobPool;
        
        for (int i = 0; i < _mobCount; i++) {
            int ix = Random.Range(0, mobPool.Count);
            string enemyType = mobPool[ix] as string;
            mobPool.Remove(ix);

            GameObject instance;
            Damager damager;

            switch (enemyType) {
                case "Footman":
                    instance = Instantiate(footmanPrefab, 
                        spawnPoint.position + new Vector3(0, 1, 0),
                        spawnPoint.rotation, _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _footmanHealth;
                    damager.speed = _monsterSpeed;
                    break;
                
                case "Lich":
                    instance = Instantiate(lichPrefab,
                        spawnPoint.position + new Vector3(0, 1, 0),
                        spawnPoint.rotation, _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _lichHealth;
                    damager.speed = _monsterSpeed;
                    break;
                
                case "Grunt":
                    instance = Instantiate(gruntPrefab,
                        spawnPoint.position + new Vector3(0, 1, 0),
                        spawnPoint.rotation, _parent);
                    damager = instance.GetComponent<Damager>();
                    damager.health = damager.maxHealth = _gruntHealth;
                    damager.speed = _monsterSpeed;
                    break;
            }
            
            yield return new WaitForSeconds(Random.Range(1.0f, 2.5f));
        }
    }

    private IEnumerator Victory() {
        yield return new WaitForSeconds(5);
        
        _statsMenu.ActivatePanel(true);
    }
    
    public void Begin() {
        _flag = true;
        _newWave = true;
    }

    public void UpdateSpawnPoints(ArrayList spawnPoints) {
        _spawnPoints = spawnPoints;
    }

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

        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.25f);
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
        
        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.25f);
        _lichHealth += Mathf.RoundToInt(_lichHealth * 0.25f);
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
        
        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.5f);
        _lichHealth += Mathf.RoundToInt(_lichHealth * 0.25f);
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
        
        _footmanHealth += Mathf.RoundToInt(_footmanHealth * 0.25f);
        _lichHealth += Mathf.RoundToInt(_lichHealth * 0.25f);
    }
}
