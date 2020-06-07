using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private TextMeshProUGUI _text;
	private RectTransform _rect;
	private Color32 _highlight;

	private CanvasGroup _main,
		_options,
		_playPanel,
		_videoPanel,
		_audioPanel;
	
	private void Start () {
		_text = GetComponent<TextMeshProUGUI>();
		_rect = GetComponent<RectTransform>();
		_main = GameObject.Find("MainMenu").GetComponent<CanvasGroup>();
		_options = GameObject.Find("OptionsMenu").GetComponent<CanvasGroup>();
		_playPanel = GameObject.Find("PlayPanel").GetComponent<CanvasGroup>();
		_videoPanel = GameObject.Find("VideoPanel").GetComponent<CanvasGroup>();
		_audioPanel = GameObject.Find("AudioPanel").GetComponent<CanvasGroup>();
		_highlight = new Color32 (0, 125, 255, 255);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		_text.color = _highlight;
		_rect.localScale += new Vector3(0.2f, 0.2f, 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData) {
		_text.color = Color.white;
		_rect.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
	}

	public void OnPointerClick(PointerEventData eventData) {
		switch (_text.text) {
			case "Play":
				_playPanel.alpha = 1;
				_playPanel.interactable = true;
				_playPanel.blocksRaycasts = true;
				break;
			case "Profile":
				break;
			case "Options":
				_main.alpha = 0;
				_main.interactable = false;
				_main.blocksRaycasts = false;

				_options.alpha = 1;
				_options.interactable = true;
				_options.blocksRaycasts = true;
				break;
			case "Exit":
				Application.Quit();
				break;
			case "Video":
				_videoPanel.alpha = 1;
				_videoPanel.interactable = true;
				_videoPanel.blocksRaycasts = true;
				_videoPanel.GetComponent<VideoOptions>().Refresh();
				break;
			case "Audio":
				_audioPanel.alpha = 1;
				_audioPanel.interactable = true;
				_audioPanel.blocksRaycasts = true;
				break;
			case "Back":
				_options.alpha = 0;
				_options.interactable = false;
				_options.blocksRaycasts = false;

				_main.alpha = 1;
				_main.interactable = true;
				_main.blocksRaycasts = true;
				break;
		}
	}
}
