using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private TextMeshProUGUI _text;
	private RectTransform _rect;

	private void Start () {
		_text = gameObject.GetComponent<TextMeshProUGUI> ();
		_rect = gameObject.GetComponent<RectTransform>();
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
		switch (_text.text) {
			case "Start":
				SceneManager.LoadScene ("GameScene");
				break;
			case "Exit":
				Application.Quit();
				break;
		}
	}
}
