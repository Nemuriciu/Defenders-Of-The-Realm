using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour, IPointerClickHandler {
    
    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("StartMenu");
    }
}
