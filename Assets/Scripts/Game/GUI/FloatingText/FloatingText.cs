using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {
    public static FloatingText Instance;                                    //current Instance
    
    [SerializeField] public Camera targetCamera;                                    //need main Camera to use World to screen 
    [SerializeField] public int poolSize;                                           //starting poolSize, great pools at Start
    [SerializeField] public GameObject objectToPool;                                //FloatingTextComponent
    [SerializeField] public TextTypeList textTypeList;
    
    // For each pool one specific parent and List
    [System.Serializable]
    public class ObjectPool {
        public Transform poolHolder;
        public List<GameObject> gameObject = new List<GameObject>();
        public List<FloatingTextComponent> component = new List<FloatingTextComponent>();
    }
    
    private ObjectPool[] _objectPool;

    private void Start() {

        if (Instance == null)
            Instance = this;
        InitializePools();

    }
    
    private void CreateParent() {
        //Initialize Array with Length of TextTypeList
        _objectPool = new ObjectPool[textTypeList.ListSize];

        for (int i = 0;i < textTypeList.ListSize; i++) {
            ObjectPool newOp = new ObjectPool {
                poolHolder = new GameObject(textTypeList.GetName(i) + "Parent",
                    typeof(RectTransform)).transform
            };
            newOp.poolHolder.SetParent(gameObject.transform);
            _objectPool[i] = newOp;
        }
    }
    private void InitializePools() {
        //Create Parent for each TextType 
        CreateParent();
        //Create x Amount of Objects
        //store each Object in the right pool and list
        //set Pool Parent
        for (int t = 0; t < textTypeList.scriptableText.Count; t++) {
            for (int i = 0; i < poolSize; i++) {
                GameObject gbj = Instantiate(objectToPool, _objectPool[t].poolHolder, false);
                gbj.SetActive(false);
                _objectPool[t].gameObject.Add(gbj);
                _objectPool[t].component.Add(gbj.GetComponent<FloatingTextComponent>());
            }
        }   
    }
    
    private int GetIndex ( int index ) { 
        //look through all GameObjects in the array
        for (int i = 0; i < _objectPool[index].gameObject.Count;i++) {
            //If one GameObject is not Active, return this GameObject
            if (!_objectPool[index].gameObject[i].activeInHierarchy) {
                //enable the GameObject before return
                _objectPool[index].gameObject[i].SetActive(true);
                return i;
            }
        }
        //If there is no active GameObject the for loop just return nothing and this code get called
        //we create an GameObject, parent it,add it to the Pool(object itself and component), activate it and return it
        GameObject newGameObject = Instantiate(objectToPool, _objectPool[index].poolHolder,false);
        _objectPool[index].gameObject.Add(newGameObject);
        _objectPool[index].component.Add(newGameObject.GetComponent<FloatingTextComponent>());
        newGameObject.SetActive(true);
        return _objectPool[index].gameObject.Count-1;
    }

    
    /// <summary>
    /// Initialize a Scriptable Text.
    /// </summary>
    /// <param name="listPosition">From which CustomType in your List. eg [0]DamageText</param>
    /// <param name="pos">Just paste your Position, handle offset and your SCT</param>
    /// <param name="msg">What you want to output.</param>
    public void InitializeScriptableText (int listPosition, Vector3 pos, string msg) {
        //Get the position from an active and ready to use GameObject
        int poolArrayIndex = GetIndex(listPosition);
        //prepare start Positions
        ScriptableText sct = textTypeList.scriptableText[listPosition];
        pos += new Vector3(sct.Offset.x, sct.Offset.y, 0);
        pos.x += Random.Range(sct.Min.x, sct.Max.x);
        pos.y += Random.Range(sct.Min.y, sct.Max.y);
        // call Initialize Method 
        _objectPool[listPosition].component[poolArrayIndex].Initialize(sct,pos,msg,targetCamera);
    }

    
    /// <summary>
    /// Disable all FloatingText GameObjects.
    /// </summary>
    public void DisableAll() {
        for(int i = 0; i < textTypeList.ListSize;i++) {
            for(int p = 0; p < _objectPool[i].gameObject.Count-1;p++) {
                _objectPool[i].gameObject[p].SetActive(false);
            }
        }
    }

}
