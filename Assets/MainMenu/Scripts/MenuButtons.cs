using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public GameObject panel;
	
	private TextMeshProUGUI _text;
	private RectTransform _rect;
	private AudioSource _audio;

	private void Start () {
		_text = GetComponent<TextMeshProUGUI>();
		_rect = GetComponent<RectTransform>();
		_audio = GetComponent<AudioSource>();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		_text.color = new Color32 (8, 178, 127, 255);
		_rect.localScale += new Vector3(0.2f, 0.2f, 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData) {
		_text.color = Color.white;
		_rect.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
	}

	public void OnPointerClick(PointerEventData eventData) {
		_audio.Play();

		if (Info.isPanelOpen) return;
		
		switch (_text.text) {
			case "Exit":
				Application.Quit();
				break;
			default:
				panel.SetActive(true);
				Info.isPanelOpen = true;
				break;
		}
	}
}
