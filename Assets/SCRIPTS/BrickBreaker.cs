using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreaker : MonoBehaviour
{
    private Transform _transform;
    private float Speed = 2.0f;
    private float _movementDamping = 100.0f;
    private CircleCollider2D _collider = null;
    private int _direction = 0;
    private float _acceleration = 0.0f;
    private int _damage = 1;

    public Vector3 direction = Vector3.zero;

    public GameObject hitten;
    [SerializeField]
    Vector3 _currentDirenction = Vector3.zero;
    [SerializeField]
    Vector2 _lastCatchedDir = Vector2.zero;
    void Start()
    {
        _transform = transform;
        _collider = _transform.GetComponent<CircleCollider2D>();
        _direction = 1;
        direction = new Vector3(-1f, 1, 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();


    }

    private void ManageMovement()
    {
        // _acceleration = Mathf.Lerp(_acceleration, _direction, Time.deltaTime * Speed);


        //_currentDirenction = GetReflectingVector();



        // direccion LOCAL
        _currentDirenction = GetReflectingVector();

        if (_currentDirenction != Vector3.zero)
            _lastCatchedDir = _currentDirenction;
        

        _transform.position += direction * Time.deltaTime * Speed;

       // _transform.position += _currentDirenction;
        //_transform.position += dirVector;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("On Trigger Enter");


        //if (_currentDirenction != Vector3.zero)
        //    direction = _currentDirenction;
        //else
        //    direction = new Vector3(-1, 1 * _direction, 0).normalized;

        direction = _lastCatchedDir;

        float reflectionAngle = GetReflectionAngle();

        Vector3 rotationVector = new Vector3(0, 0, reflectionAngle);

        transform.Rotate(rotationVector);

        switch(other.gameObject.tag)
        {
            case ("brick"):

                IGolpeable target = other.transform.GetComponent(typeof(IGolpeable)) as IGolpeable;

                if (target != null && target.CanBeDestroyed)
                    target.GetDamage(_damage);
               
                _direction = - 1;
                _acceleration = 0;
                break;
            case ("Racket"):

                _direction = 1;
                _acceleration = 0;
                break;
            case("Bound"):
                
                break;
            case ("LowerBound"):
                break;
        }
    }

    private float GetReflectionAngle()
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, _transform.up, 2.0f);

        if (hit.collider != null)
        { 
            var angle = Vector2.Angle(_transform.up, hit.normal);

            return angle;
        }

        return 0.0f;
    }

    private Vector3 GetReflectingVector()
    {
       
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, direction, 2.0f, 9);

        if(hit.transform != null)
            Debug.Log($"HITTEN -- > {hit.transform.gameObject.name}");

        if(hit.collider != null && hit.collider != _collider)
        {

            hitten = hit.transform.gameObject;

            Vector2 vector = new Vector2(_transform.position.x, _transform.position.y);

            Vector2 hitVector = hit.point - vector;

            Vector2 localHitVector = _transform.InverseTransformVector(hitVector).normalized;

            //Vector2 localHit = _transform.InverseTransformVector(vector);

            Vector2 reflect = hit.point + hit.normal + new Vector2(hitVector.x * 2, 0);


            Vector2 localReflect = hit.transform.InverseTransformVector(reflect - hit.point).normalized;
            // Vector2 reflectpoint = new Vector2(-hitVector.x, 0) * 2;

            Vector2 reflectingVector = reflect - hit.point;

            return hit.transform.InverseTransformVector(reflectingVector).normalized;         
        }

        return Vector3.zero;
    }

    private Vector3 GetDirVector()
    {
        // Vector Local suma t(ranform.y * direction (1/-1) + Vector H).Normalized();

        // VectorH --> VectorY + angulo con respecto a la normal

        Vector3 normal = new Vector2(0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 2.0f);

        if (hit.collider != null)
        {
            normal = hit.normal;
        }

        Vector3 rightVector = new Vector3(1, 0, 0);

        return hit.transform.InverseTransformVector(normal + rightVector).normalized * Time.deltaTime * Speed;

    }
}
