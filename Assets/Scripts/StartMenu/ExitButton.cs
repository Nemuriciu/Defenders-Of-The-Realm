using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text _exit;

	private void Start () {
		_exit = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		_exit.color = new Color32 (8, 178, 127, 255);
		_exit.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		_exit.color = Color.white;
		_exit.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		Application.Quit ();
	}
}
