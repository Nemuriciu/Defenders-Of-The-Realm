using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text options;

	void Start () {
		options = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		options.color = new Color32 (8, 178, 127, 255);
		options.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		options.color = Color.white;
		options.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		//TODO:
	}
}