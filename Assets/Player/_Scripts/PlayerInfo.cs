using System.Collections;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    public GameObject physicalProjectile;
    public GameObject magicalProjectile;
    [HideInInspector] public GameObject projectile;

    private Animator _leftAnim, _rightAnim;
    private Building _buildScript;

    private GameObject _physicalLeft, _physicalRight;
    private GameObject _magicalLeft, _magicalRight;
    private string _spellType;
    private bool _scrollDelay;
    
    private void Start() {
        _buildScript = GetComponent<Building>();
        _leftAnim = GameObject.Find("Left_hand").GetComponent<Animator>();
        _rightAnim = GameObject.Find("Right_hand").GetComponent<Animator>();
        _physicalLeft = GameObject.Find("PhysicalFire_Left");
        _physicalRight = GameObject.Find("PhysicalFire_Right");
        _magicalLeft = GameObject.Find("MagicalFire_Left");
        _magicalRight = GameObject.Find("MagicalFire_Right");
        
        _magicalLeft.SetActive(false);
        _magicalRight.SetActive(false);
        _spellType = "Physical";
        projectile = physicalProjectile;
    }

    private void Update() {
        if (!_buildScript.CanBuild)
            if (Input.GetMouseButtonDown(0)) {
                _leftAnim.SetTrigger("Shot");
                _rightAnim.SetTrigger("Shot");
            }

        /* Change spell type on Mouse Scroll */
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 ||
            Input.GetAxis("Mouse ScrollWheel") < 0) && !_scrollDelay) {
            switch (_spellType) {
                case "Physical":
                    _spellType = "Magical";
                    projectile = magicalProjectile;
                    
                    _physicalLeft.SetActive(false);
                    _physicalRight.SetActive(false);
                    _magicalLeft.SetActive(true);
                    _magicalRight.SetActive(true);
                    break;
                case "Magical":
                    _spellType = "Physical";
                    projectile = physicalProjectile;
                    
                    _magicalLeft.SetActive(false);
                    _magicalRight.SetActive(false);
                    _physicalLeft.SetActive(true);
                    _physicalRight.SetActive(true);
                    break;
            }

            _scrollDelay = true;
            StartCoroutine(ScrollDelay());
        }
    }

    private IEnumerator ScrollDelay() {
        yield return new WaitForSeconds(1.5f);
        _scrollDelay = false;
    }
}
