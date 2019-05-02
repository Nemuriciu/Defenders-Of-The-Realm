using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text exit;

	void Start () {
		exit = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		exit.color = new Color32 (8, 178, 127, 255);
		exit.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		exit.color = Color.white;
		exit.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		Application.Quit ();
	}
}
