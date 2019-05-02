using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text start;

	void Start () {
		start = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		start.color = new Color32 (8, 178, 127, 255);
		start.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		start.color = Color.white;
		start.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		SceneManager.LoadScene ("GameScene");
	}
}
