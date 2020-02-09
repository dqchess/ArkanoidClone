using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreaker : MonoBehaviour
{
    private Transform _transform;
    private float Speed = 5.0f;
    private float _movementDamping = 100.0f;
    private CircleCollider2D _collider = null;
    private int _direction = 0;
    private float _acceleration = 0.0f;
    private int _damage = 1;
    void Start()
    {
        _transform = transform;
        _collider = _transform.GetComponent<CircleCollider2D>();
        _direction = 1;

    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        _acceleration = Mathf.Lerp(_acceleration, _direction, Time.deltaTime * Speed);

        var movY =  _direction * (Time.deltaTime * Speed);

        _transform.position += new Vector3(0, movY, 0);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("On Trigger Enter");

        switch(other.gameObject.tag)
        {
            case ("brick"):

                IGolpeable target = other.transform.GetComponent(typeof(IGolpeable)) as IGolpeable ;

                if (target != null && target.CanBeDestroyed)
                    target.GetDamage(_damage);
               
                _direction = - 1;
                _acceleration = 0;
                break;
            case ("Racket"):

                _direction = 1;
                _acceleration = 0;
                break;
        }
    }
}
