using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
	
	public CreatureInfo healthLink;				//A path to the Health filed;			
	public RectTransform healthbarPrefab;			//Our health bar prefab;
	public float yOffset;							//Horizontal position offset;
	public bool keepSize = true;	                
	public float scale = 1;							//Scale of the healthbar;
	public Vector2 sizeOffsets;						
	public bool drawOffDistance;					//Disable health bar if it out of drawDistance;
	public float drawDistance = 10;
	public bool showHealthInfo, isHit;						//Show the health info on top of the health bar or not;
	public enum HealthInfoAlignment {Top, Center, Bottom};
	public HealthInfoAlignment healthInfoAlignment = HealthInfoAlignment.Center;
	public float healthInfoSize = 10;
    public AlphaSettings alphaSettings;
	private Image _healthVolume, _backGround;			//Health bar images, should be named as "Health" and "Background";
	private TextMeshProUGUI _healthInfo;
	private CanvasGroup _canvasGroup;
	private Vector2 _healthbarPosition, _healthbarSize, _healthInfoPosition;
	private Transform _thisT;
	private int _defaultHealth, _lastHealth;
	private float _camDistance, _dist, _pos, _rate;
	private Camera _cam;
    private GameObject _healthbarRoot;
	[HideInInspector]public Canvas canvas;
	
	private void Awake() {
		var canvases = (Canvas[])FindObjectsOfType(typeof(Canvas));
		if (canvases.Length == 0)
			Debug.LogError("There is no Canvas in the scene or Canvas GameObject isn't active, please create one - GameObject->UI->Canvas or activate existing");
		
		foreach (var c in canvases) {
			if(c.enabled && c.gameObject.activeSelf && c.renderMode == RenderMode.ScreenSpaceOverlay)
				canvas = c;
			else
				Debug.LogError("There is no Canvas with RenderMode: ScreenSpace - Overlay in the scene or it's disabled, please create one - GameObject->UI->Canvas or enable existing");
		}

        _defaultHealth = healthLink.GetHealth();
        _lastHealth = _defaultHealth;
	}
	
	private void Start () {
		if(!healthbarPrefab) {
			Debug.LogWarning("HealthbarPrefab is empty, please assign your healthbar prefab in inspector");
			return;
		}
		
		_thisT = transform;
        _healthbarRoot = canvas.transform.Find("HealthbarRoot") != null ? canvas.transform.Find("HealthbarRoot").gameObject : new GameObject("HealthbarRoot", typeof(RectTransform), typeof(HealthbarRoot));
        _healthbarRoot.transform.SetParent(canvas.transform, false);
		healthbarPrefab = Instantiate(healthbarPrefab, new Vector2 (-1000, -1000), Quaternion.identity);
        healthbarPrefab.name = "HealthBar";
        healthbarPrefab.SetParent(_healthbarRoot.transform, false);
		_canvasGroup = healthbarPrefab.GetComponent<CanvasGroup> ();
		
		_healthVolume = healthbarPrefab.transform.Find ("Health").GetComponent<Image>();
		_backGround = healthbarPrefab.transform.Find ("Background").GetComponent<Image>();
		_healthInfo = healthbarPrefab.transform.Find ("HealthInfo").GetComponent<TextMeshProUGUI> ();
		_healthInfo.enableAutoSizing = true;
		_healthInfo.rectTransform.anchoredPosition = Vector2.zero;
		_healthInfoPosition = _healthInfo.rectTransform.anchoredPosition;
		_healthInfo.fontSizeMin = 1;
		_healthInfo.fontSizeMax = 64;
		
		_healthbarSize = healthbarPrefab.sizeDelta;
        _canvasGroup.alpha = alphaSettings.nullAlpha;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
		_cam = Camera.main;
		
		healthLink.SetHealthbar(this);
	}
	
	// Update is called once per frame
	private void LateUpdate () {
		if(!healthbarPrefab)
			return;
		
		healthbarPrefab.transform.position = _cam.WorldToScreenPoint(new Vector3(_thisT.position.x, _thisT.position.y + yOffset, _thisT.position.z));
		_healthVolume.fillAmount =  (float)healthLink.GetHealth() / _defaultHealth;

		const float maxDifference = 0.1F;


		if(_backGround.fillAmount > _healthVolume.fillAmount + maxDifference)
			_backGround.fillAmount = _healthVolume.fillAmount + maxDifference;
        if (_backGround.fillAmount > _healthVolume.fillAmount)
            _backGround.fillAmount -= (1 / ((float)_defaultHealth / 100)) * Time.deltaTime;
        else
            _backGround.fillAmount = _healthVolume.fillAmount;
	}
	
	
	private void Update() {
		if(!healthbarPrefab)
			return;
		
		_camDistance = Vector3.Dot(_thisT.position - _cam.transform.position, _cam.transform.forward);
		
		if(showHealthInfo)
			_healthInfo.text = healthLink.GetHealth() +" / "+_defaultHealth;
		else
			_healthInfo.text = "";

        if(_lastHealth != healthLink.GetHealth()) {
            _rate = Time.time + alphaSettings.onHit.duration;
            _lastHealth = healthLink.GetHealth();
        }

        if (isHit) {
	        if (!OutDistance() && IsVisible()) {
		        if (healthLink.GetHealth() <= 0) {
			        if (alphaSettings.nullFadeSpeed > 0) {
				        if (_backGround.fillAmount <= 0)
					        _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, alphaSettings.nullAlpha,
						        alphaSettings.nullFadeSpeed);
			        }
			        else
				        _canvasGroup.alpha = alphaSettings.nullAlpha;
		        }
		        else if (healthLink.GetHealth() == _defaultHealth)
			        _canvasGroup.alpha = alphaSettings.fullFadeSpeed > 0
				        ? Mathf.MoveTowards(_canvasGroup.alpha, alphaSettings.fullAlpha, alphaSettings.fullFadeSpeed)
				        : alphaSettings.fullAlpha;
		        else {
			        if (_rate > Time.time)
				        _canvasGroup.alpha = alphaSettings.onHit.onHitAlpha;
			        else
				        _canvasGroup.alpha = alphaSettings.onHit.fadeSpeed > 0
					        ? Mathf.MoveTowards(_canvasGroup.alpha, alphaSettings.defaultAlpha,
						        alphaSettings.onHit.fadeSpeed)
					        : alphaSettings.defaultAlpha;
		        }
	        }
	        else
		        _canvasGroup.alpha = alphaSettings.defaultFadeSpeed > 0
			        ? Mathf.MoveTowards(_canvasGroup.alpha, 0, alphaSettings.defaultFadeSpeed)
			        : 0;
        }

        _dist = keepSize ? _camDistance / scale :  scale;

		healthbarPrefab.sizeDelta = new Vector2 (_healthbarSize.x/(_dist-sizeOffsets.x/100), _healthbarSize.y/(_dist-sizeOffsets.y/100));
		
		_healthInfo.rectTransform.sizeDelta = new Vector2 (healthbarPrefab.sizeDelta.x * healthInfoSize/10, 
		                                                  healthbarPrefab.sizeDelta.y * healthInfoSize/10);
		
		_healthInfoPosition.y = healthbarPrefab.sizeDelta.y + (_healthInfo.rectTransform.sizeDelta.y - healthbarPrefab.sizeDelta.y) / 2;
		
		if(healthInfoAlignment == HealthInfoAlignment.Top)
			_healthInfo.rectTransform.anchoredPosition = _healthInfoPosition;
		else if (healthInfoAlignment == HealthInfoAlignment.Center)
			_healthInfo.rectTransform.anchoredPosition = Vector2.zero;
		else
			_healthInfo.rectTransform.anchoredPosition = -_healthInfoPosition;

        if(healthLink.GetHealth() > _defaultHealth)
            _defaultHealth = healthLink.GetHealth();
	}

	private bool IsVisible() {
		return canvas.pixelRect.Contains (healthbarPrefab.position);
	}

	private bool OutDistance() {
        return drawOffDistance && _camDistance > drawDistance;
    }

	public void RemoveHealthbar() {
		_healthbarRoot.GetComponent<HealthbarRoot>().healthBars.Remove(healthbarPrefab);
		Destroy(healthbarPrefab.gameObject);
	}
}

[System.Serializable]
public class AlphaSettings {
    
    public float defaultAlpha = 0.7F;           //Default healthbar alpha (health is bigger then zero and not full);
    public float defaultFadeSpeed = 0.1F;
    public float fullAlpha = 1.0F;             //Healthbar alpha when health is full;
    public float fullFadeSpeed = 0.1F;
    public float nullAlpha;              //Healthbar alpha when health is zero or less;
    public float nullFadeSpeed = 0.1F;
    public OnHit onHit;                         //On hit settings
}

[System.Serializable]
public class OnHit {
    public float fadeSpeed = 0.1F;              //Alpha state fade speed;
    public float onHitAlpha = 1.0F;             //On hit alpha;
    public float duration = 1.0F;               //Duration of alpha state;
}
