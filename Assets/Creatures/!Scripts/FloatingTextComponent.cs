using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextComponent : MonoBehaviour {
    [SerializeField] private Text text;
    [SerializeField] private Image icon;
    private RectTransform _rectTransform;
    private float _amount;                       //Amount to Increase FontSize
    private float _fontSize;                     //regular FontSize
    private Vector3 _startPosition;              //Position for this GameObject
    private AnimationCurve _animCurveX;
    private AnimationCurve _animCurveY;
    private AnimationCurve _fontSizeAnimCurve;
    private float _animDuration;                 //duration from longest AnimationCurve
    private Gradient _colorGradient;             //Text Color Gradient
    private Outline _outline;                    //Text outline Effect
    private Camera _cam;                         //Main Camera
    private Vector2 _animationDirection;         // e.g. from 0,0 to 30,100
    private bool _onScreen;                      //Fake WorldSpace or ScreenSpace

    private void OnEnable() {
        if (text == null)
            text = GetComponentInChildren<Text>();
        if (icon == null)
            icon = GetComponentInChildren<Image>();
        if (_outline == null)
            _outline = GetComponentInChildren<Outline>();
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }
    
    /// <summary>
    /// Initialize the Floating Text Component.
    /// </summary>
    /// <param name="sct">To get all the Information.</param>
    /// <param name="pos">Start at this point.</param>
    /// <param name="sctText">See this on Screen.</param>
    /// <param name="targetCam">Main Camera, need this to convert World to Screen point</param>
    public void Initialize ( ScriptableText sct, Vector3 pos, string sctText, Camera targetCam ) {
        //If Outline effect is set to false ignore the next variables
        if (sct.useOutline) {
            _outline.enabled = sct.useOutline;
            _outline.effectColor = sct.outlineColor;
            _outline.effectDistance = sct.outlineEffectDistance;
        }
        else
            _outline.enabled = sct.useOutline;

        //Better stores ref to Camera then call Camera.main, it is actually just FindObjectOfTag("MainCamera").
        _cam = targetCam;
        //Font from ScriptableText
        text.font = sct.Font;
        
        if (sct.UseIcon) {
            icon.enabled = true;
            RectTransform rect = icon.rectTransform;
            rect.localPosition = sct.IconPosition;
            rect.sizeDelta = sct.IconSize;

            icon.sprite = sct.Icon;
        }
        
        //Font Size from ScriptableText as ref / start point for Lerp
        _fontSize = sct.FontSize;
        //Amount to increase the Text Size
        _amount = sct.IncreaseAmount + sct.FontSize;
        //set the Text size
        text.fontSize = sct.FontSize;

        //set Color Gradient
        _colorGradient = sct.ColorGradient;
        //set Animation Curve for Fonz size Animation
        _fontSizeAnimCurve = sct.FontSizeAnimation;
        //set animationCurve
        _animCurveX = sct.AnimCurveX;
        _animCurveY = sct.AnimCurveY;

        //set Animation Length
        //---Explanation Ternary if (XCurveTime >  YCurveTime) animDuration = XCurveTime else animDuration = YCurveTime
        _animDuration = sct.AnimCurveX.keys[sct.AnimCurveX.length - 1].time >= sct.AnimCurveY.keys[sct.AnimCurveY.length - 1].time ? 
            sct.AnimCurveX.keys[sct.AnimCurveX.length - 1].time : sct.AnimCurveY.keys[sct.AnimCurveY.length - 1].time;
        _onScreen = sct.OnScreen;
        
        _startPosition = _onScreen == false ? pos : sct.ScreenPosition;
        _animationDirection = sct.AnimationDirection;
        //set Text
        text.text = sctText;
        
        /*  */
        gameObject.layer = 0;

        //activate Floating Text Object
        gameObject.SetActive(true);

        StartCoroutine(AnimateTextComponent());
    }

    private IEnumerator AnimateTextComponent() {
        //Increase timer over Time to Evaluate Animation Curve and Color Gradient
        float timer = 0;
        //Animation Length
        float animationTime = _animDuration;
        //change Icon Alpha with Text Alpha
        Color tempColor = icon.color;
        //as long as timer is not bigger than the Animation Length
        while (timer < animationTime) {
            //Evaluate  Timer with the Curve
            var curveTimerX = _animCurveX.Evaluate(timer);
            var curveTimerY = _animCurveY.Evaluate(timer);

            // if amount == 0 no Size Animation
            if (_amount != 0) {
                var fontSizeCurveAnim = _fontSizeAnimCurve.Evaluate(timer);
                text.fontSize = (int)Mathf.Lerp(_fontSize, _amount, fontSizeCurveAnim);
            }

            text.color = _colorGradient.Evaluate(timer);
            tempColor.a = text.color.a;
            icon.color = tempColor;


            //Based on the Transform Z-Axis, disable if less than 0
            bool isInView = _rectTransform.position.z < 0;
            gameObject.SetActive(!isInView);
            //ternary position = If(onScreen ==false)cam.WorldToScreenPoint(startPosition) else startPosition
            transform.position = _onScreen == false ? _cam.WorldToScreenPoint(_startPosition) : _startPosition;
            //Lerp the Text GameObject in localSpace
            var x = Mathf.Lerp(0, _animationDirection.x, curveTimerX);
            var y = Mathf.Lerp(0, _animationDirection.y, curveTimerY);
            text.rectTransform.localPosition = new Vector3(x,y, 0);

            text.enabled = true;
            
            timer += Time.deltaTime;

            yield return null;
        }
        //disable Text Component to avoid weird jump behaviour
        text.enabled = false;
        text.transform.localPosition = Vector3.zero;
        //deactivate this Object -> back to Pool
        gameObject.SetActive(false);
    }
}
