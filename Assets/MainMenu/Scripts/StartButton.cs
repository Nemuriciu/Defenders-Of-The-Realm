using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerClickHandler {
    private AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        _audio.Play();

        if (Info.selectedDifficulty != null)
            SceneManager.LoadScene("LoadingScene");
    }
}
