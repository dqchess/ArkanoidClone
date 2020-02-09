using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform _transform;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _inputMultiplier = 1.0f;
    [SerializeField]
    private float _movDamping = 5.0f;
    [SerializeField]
    private float _playerMov_X = 0.0f;
    private float _movementClamp = 3.45f;
    private float _input_X = 0.0f;



    private bool _isMoving = false;

    public float InputMultiplier { get => _inputMultiplier; set => _inputMultiplier = value; }
    public float MovementDamping { get => _movDamping; set => _movDamping = value; }

    void Start()
    {
        _transform = transform;
        _collider = _transform.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        _playerMov_X = Mathf.Clamp(ManageMovement(), -_movementClamp, _movementClamp);
       
        _isMoving = Mathf.Abs(_input_X) > 0 ? true : false;

        MovePlayer();
    }

    private void FixedUpdate()
    {
       
    }

    private float ManageMovement()
    {
        _input_X = Input.GetAxis("Horizontal") * _inputMultiplier;

        float xValue = _playerMov_X;
         
        return Mathf.Lerp(xValue, _input_X, 1 / (Time.deltaTime * _movDamping));
    }

    private void MovePlayer()
    {
        if(CanMove())
            _transform.position += new Vector3(_playerMov_X, 0, 0);
        
        ClampMovement();
    }

    private bool CanMove()
    {           
        return _isMoving && Mathf.Abs(_transform.position.x) <= _movementClamp;
    }

    private void ClampMovement()
    {
        if(Mathf.Abs(_transform.position.x) > _movementClamp)
        {
            var hMoveSign = Mathf.Sign(_transform.position.x);

            _transform.position = new Vector3(_movementClamp * hMoveSign, _transform.position.y, _transform.position.z);
        }

    }

}
