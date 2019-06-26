using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private Text _start;

	private void Start () {
		_start = gameObject.GetComponent<Text> ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		_start.color = new Color32 (8, 178, 127, 255);
		_start.fontSize = 55;
	}

	public void OnPointerExit(PointerEventData eventData) {
		_start.color = Color.white;
		_start.fontSize = 45;
	}

	public void OnPointerClick(PointerEventData eventData) {
		SceneManager.LoadScene ("GameScene");
	}
}
