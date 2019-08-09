using UnityEngine;
using UnityEngine.UI;

public class BuildAnimation : MonoBehaviour {
    [HideInInspector] public bool isActive;
    public float timer;
    public GameObject towerPrefab;
    public GameObject loadingBarPrefab;

    
    private Transform _twrGroup;
    private Transform _canvas;
    private Camera _cam;
    private GameObject _loadingBar;
    private RectTransform _loadingBarRect;
    private Image _loadingBarFill;
    private bool _inst, _inRange;
    private float _timer;

    private void Start() {
        _twrGroup = GameObject.Find("Towers").transform;
        _canvas = GameObject.Find("Canvas").transform;
        _cam = Camera.main;
        _timer = timer;
    }

    private void Update() {
        if (isActive) {
            if (!_inst) {
                _loadingBar = Instantiate(loadingBarPrefab, Vector3.zero, Quaternion.identity, _canvas);
                _loadingBar.transform.SetAsFirstSibling();
                _loadingBarRect = _loadingBar.GetComponent<RectTransform>();
                _loadingBarFill = _loadingBar.transform.GetChild(0).GetComponent<Image>();
                _inst = _inRange = true;
            }
            
            if (_timer < 0) {
                Instantiate(towerPrefab, gameObject.transform.position, Quaternion.identity, _twrGroup);
                Destroy(_loadingBar);
                Destroy(gameObject);
            }
            
            _timer -= Time.deltaTime;
            
            //float camDistance = Vector3.Distance(transform.position, _player.position);
            float camDistance = Vector3.Dot(transform.position - _cam.transform.position, _cam.transform.forward);
            
            if (_inRange && (camDistance >= 50 || camDistance < 1.5f)) {
                _inRange = false;
                _loadingBar.SetActive(false);
            } else if (!_inRange && (camDistance < 50 && camDistance > 1.5f)) {
                _inRange = true;
                _loadingBar.SetActive(true);
            }

            float fillAmount = (timer - _timer) / timer;
            Vector3 pos = gameObject.transform.position;
            pos.y += 5;

            _loadingBarRect.localScale = new Vector3(1 - camDistance / 75,
                1 - camDistance / 75, 1 - camDistance / 75);
            _loadingBarRect.position = _cam.WorldToScreenPoint(pos);
            _loadingBarFill.fillAmount = (fillAmount > 1) ? 1 : fillAmount;
            
            
        }    
    }
}
