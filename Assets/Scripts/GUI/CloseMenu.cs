using UnityEngine;
using UnityEngine.EventSystems;

public class CloseMenu : MonoBehaviour, IPointerClickHandler {
    public GameMenu gameMenu;

    public void OnPointerClick(PointerEventData eventData) {
        gameMenu.CloseMenu(true);
    }
}
