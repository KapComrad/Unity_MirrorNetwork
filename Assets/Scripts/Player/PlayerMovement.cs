using System.Collections;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Color32 _color;
    private MeshRenderer _renderer;

    private Rigidbody _rigidbody;


    private float _horizontalInput;
    private float _verticalInput;

    private bool _canMove = true;
    [Header("Player variables")]
    [SerializeField] private float _speed = 5;
    private float _maximumSpeed = 10;
    [Header("Jerk parametr")]
    [SerializeField] private float _jerkRange = 250;
    private float _jerkTime = 1.5f;

    private bool _mouseButtonClicked;
    private Coroutine _jerkTimeCoroutine;

    private void Start()
    {
        base.OnStartServer();
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _mouseButtonClicked = Input.GetMouseButtonDown(0);
    }

    private void FixedUpdate()
    {
        if(_canMove)
            Movement();
        Jerk();
    }
    public void ChangeColor()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        _color = Color.red;
        _renderer.material.color = _color;

        while (true)
        {
            yield return wait;
            _color = Color.white;
            _renderer.material.color = _color;
            StopCoroutine(Timer());
        }
        

    }

    private void Movement()
    {
        _rigidbody.velocity = new Vector3(_horizontalInput * _speed, _rigidbody.velocity.y, _verticalInput * _speed);
    }

    private void Jerk()
    {
        if (_mouseButtonClicked)
        {
            Debug.Log("Mouse clicked");
            _canMove = false;
            _rigidbody.AddForce(new Vector3(_horizontalInput * _jerkRange, _rigidbody.velocity.y, _verticalInput * _jerkRange));

            _jerkTimeCoroutine = StartCoroutine(JerkTime());
        }

    }

    private IEnumerator JerkTime()
    {
        yield return new WaitForSeconds(_jerkTime);
        _canMove = true;
    }
}
