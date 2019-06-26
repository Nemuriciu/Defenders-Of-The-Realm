using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text _options;

	private void Start () {
		_options = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		_options.color = new Color32 (8, 178, 127, 255);
		_options.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		_options.color = Color.white;
		_options.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		//TODO:
	}
}