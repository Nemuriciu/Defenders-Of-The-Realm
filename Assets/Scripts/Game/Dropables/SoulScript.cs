using UnityEngine;

public class SoulScript : MonoBehaviour {
    public Vector3 rotationAngle;
    public float rotationSpeed;
    public float floatSpeed;
    public float floatRate;
    
    private bool _goingUp = true;
    private bool _isTriggered;
    private float _floatTimer, _acc = 1;
    private const float MaxSpeed = 20;
    private GameObject _player;
    private ProgressBar _buildBar;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _buildBar = GameObject.Find("BuildBar").GetComponent<ProgressBar>();
    }

    private void Update () {
        if (_isTriggered) {
            Vector3 pos = _player.transform.position;
            pos.y += 1;
            
            transform.position = Vector3.MoveTowards(transform.position, pos, _acc * Time.deltaTime);

            if (_acc < MaxSpeed)
                _acc += 0.5f;
            
            if (transform.position == pos) {
                _buildBar.ChangeValue(10);
                Destroy(gameObject);
            }
        }
        
        transform.Rotate(rotationSpeed * Time.deltaTime * rotationAngle);

        _floatTimer += Time.deltaTime;
        Vector3 moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
        transform.Translate(moveDir);

        if (_goingUp && _floatTimer >= floatRate) {
            _goingUp = false;
            _floatTimer = 0;
            floatSpeed = -floatSpeed;
        } else if(!_goingUp && _floatTimer >= floatRate) {
            _goingUp = true;
            _floatTimer = 0;
            floatSpeed = +floatSpeed;
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            _isTriggered = true;
    }
}
