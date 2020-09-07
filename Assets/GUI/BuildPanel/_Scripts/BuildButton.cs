using UnityEngine;

public class BuildButton : MonoBehaviour {
    public Tower tower;
    public BuildTooltip tooltip;
    
    private RectTransform _rect;
    private BuildPanel _build;
    private CanvasGroup _tooltipGr;

    private void Start() {
        _rect = GetComponent<RectTransform>();
        _build = GetComponentInParent<BuildPanel>();
        _tooltipGr = tooltip.GetComponent<CanvasGroup>();
    }

    public void BuildTower(int index) {
        _build.SpotInstance.AddTower(index);
        _build.ClosePanel();
        _tooltipGr.alpha = 0;
    }

    public void ZoomIn() {
        _rect.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        
        /* Open tower tooltip */
        tooltip.SetTooltip(tower);
        _tooltipGr.alpha = 1;
    }
    
    public void ZoomOut() {
        _rect.localScale = Vector3.one;
        _tooltipGr.alpha = 0;
    }
}
