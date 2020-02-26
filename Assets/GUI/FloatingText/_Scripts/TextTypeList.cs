using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableText {
    public string textTypeName;
    
    [SerializeField] public bool useIcon;
    public bool UseIcon => useIcon;

    [SerializeField] public Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] public Vector2 iconPosition;
    public Vector2 IconPosition => iconPosition;

    [SerializeField] public Vector2 iconSize = new Vector2(100,100);
    public Vector2 IconSize => iconSize;

    [Tooltip("Offset From Start(Spawn) Position")]
    //Offset Start Position
    [SerializeField] public Vector2 offset;
    public Vector2 Offset => offset;

    [Tooltip("Random.Range(min,max)")]
    // Min getter
    [SerializeField] public Vector2 min;          
    public Vector2 Min => min;

    [Tooltip("Random.Range(min,max)")]
    // Max getter
    [SerializeField] public Vector2 max;          
    public Vector2 Max => max;

    [Tooltip("Text remain on Screen.")]
    // "WorldSpace" or CanvasScreenSpace
    [SerializeField] public bool onScreen;        
    public bool OnScreen => onScreen;

    [Tooltip("Overwrite the StartPosition.")]
    [SerializeField] public float xAxis = 0.5f;
    [SerializeField] public float yAxis = 0.5f;
    public Vector3 ScreenPosition => new Vector3(Screen.width * xAxis, Screen.height * yAxis,0);

    [Tooltip("This is not converted to ScreenSpace just added to the SpawnPosition.")]
    //the Text goes from Spawn point to Spawn point + animationDirection
    [SerializeField] public Vector2 animationDirection = new Vector2(0, 0);         
    public Vector2 AnimationDirection => animationDirection;
    
    // Unit's Builtin Animation Curve
    [SerializeField] public AnimationCurve animCurveX = 
        AnimationCurve.Linear(0,0,1,1);                          
    public AnimationCurve AnimCurveX => animCurveX;    // Animation Curve Getter

    // Unit's Builtin Animation Curve
    [SerializeField] public AnimationCurve animCurveY = 
        AnimationCurve.Linear(0, 0, 1, 1);                       
    public AnimationCurve AnimCurveY => animCurveY;    // Animation Curve Getter

    // Pick a Font which fits best
    [SerializeField] public Font font;                
    public Font Font => font;                          // Font getter, to only access 

    // Choose the Size for your needs
    [SerializeField] public int fontSize = 20;
    public int FontSize => fontSize;                   // Font size Getter, only to access this

    [Tooltip("Is used to Increase the FontSize over Time with the AnimationCurve.Tip: 0 means no Animation")]
    // Increase Fontsize + Increase Amount over Time
    [SerializeField] public int increaseAmount = 10;                                                        
    public int IncreaseAmount => increaseAmount;       // Font increase Amount Getter, only to access , read this

    // Use this to increase the Font size
    [Tooltip("Use this to Animate the size of the Font.")]
    [SerializeField] public AnimationCurve fontSizeAnimation = 
        AnimationCurve.Linear(0, 0, 1, 1);           
    public AnimationCurve FontSizeAnimation => fontSizeAnimation;        //AnimationCurve getter

    // Curve Length ReadyOnly just for clarity
    /*
    [SerializeField] private float fontAnimLength;                                                   
    public float FontAnimLength => fontAnimLength;   
    */                   // Curve Length getter

    // Change Color over the Animation Time
    [Tooltip("Change smoothly between Colors.")]
    [SerializeField] public Gradient colorGradient;                                                         
    public Gradient ColorGradient => colorGradient;                     // Color Gradient Getter, to only access


    [Tooltip("Check this for Text Outline Effect.ColorGradient with Alpha dont work right if true.")]
    public bool useOutline;
    [Tooltip("Text Outline Color.")]
    public Color outlineColor = Color.black;
    [Tooltip("Size of the Outline Effect.")]
    public Vector2 outlineEffectDistance = new Vector2(1, -1);
}
[CreateAssetMenu(menuName = ("ScriptableText / new List"))]

public class TextTypeList : ScriptableObject {
    public List<ScriptableText> scriptableText = new List<ScriptableText>();
    public int ListSize => scriptableText.Count;
    public string GetName(int index) { return scriptableText[index].textTypeName; }
}
