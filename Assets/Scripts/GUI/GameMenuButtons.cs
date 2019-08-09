using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour, IPointerClickHandler {
    private TextMeshProUGUI _text;
    private AudioSource _audio;
    private GameMenu _gameMenu;
    
    private string _buttonText;
    private bool _flag;

    private void Start() {
        _audio = GetComponent<AudioSource>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _gameMenu = GameObject.Find("Canvas").GetComponent<GameMenu>();
        _buttonText = _text.text;
    }


    public void OnPointerClick(PointerEventData eventData) {
        _audio.Play();

        switch (_buttonText) {
            case "Quit":
                Application.Quit();
                break;
            case "Main Menu":
                _gameMenu.CloseMenu(false);
                SceneManager.LoadScene("StartMenu");
                break;
        }
    }
}