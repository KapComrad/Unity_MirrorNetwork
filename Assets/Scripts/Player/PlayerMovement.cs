using System.Collections;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody _rigidbody;

    private float _horizontalInput;
    private float _verticalInput; 

    [Header("Movement parametrs")]
    [SerializeField] private float _speed = 5;
    public bool canMove = true;

    [Header("Jerk parametrs")]
    [Tooltip("You can change range here ->")]
    [SerializeField] private float _jerkRange = 250;
    private float _jerkTime = 1.5f;

    private bool _mouseButtonClicked;
    public bool _jerking;

    private void Start()
    {
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
        if (!isLocalPlayer) return;

        if (canMove)
            Movement();
        Jerk();

        if (transform.position.y < -50)
        {
            OutOfBounds();
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
            canMove = false;
            _jerking = true;
            _rigidbody.AddForce(new Vector3(_horizontalInput * _jerkRange, _rigidbody.velocity.y, _verticalInput * _jerkRange));

            StartCoroutine(JerkTime());
        }

    }

    private IEnumerator JerkTime()
    {
        yield return new WaitForSeconds(_jerkTime);
        canMove = true;
        _jerking = false;
    }

    [Command]
    private void OutOfBounds()
    {
        TpToSpawn(5,5,5);
    }

    [TargetRpc]
    private void TpToSpawn(float newX , float newY , float newZ)
    {
        transform.position = new Vector3(newX, newY, newZ);
    }
}
