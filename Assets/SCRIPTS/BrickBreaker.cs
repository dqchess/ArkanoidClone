using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreaker : MonoBehaviour
{
    private Transform _transform;
    private float Speed = 2.0f;
    private float _movementDamping = 100.0f;
    private CircleCollider2D _collider = null;
    private int _verticalDir = 0;
    private int _horizontalDir = 0;
    private float _acceleration = 0.0f;
    private int _damage = 1;

    //public delegate void OnChangeDirection();
    //public event OnChangeDirection _changeDirection;

    public Vector3 Direction = Vector3.zero;

    public GameObject Hitten;
    [SerializeField]
    Vector3 _currentDirection = Vector3.zero;
    [SerializeField]
    Vector2 _lastCatchedDir = Vector2.zero;
    void Start()
    {
        _transform = transform;
        _collider = _transform.GetComponent<CircleCollider2D>();
        _verticalDir = 1;
        _horizontalDir = 0;
        Direction = new Vector3(-1, 1, 0).normalized;
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
        _currentDirection = GetReflectingVector();

        if (_currentDirection != Vector3.zero)
            _lastCatchedDir = _currentDirection;

        _transform.Translate(Direction * Speed * Time.deltaTime);
        //_transform.position += Direction * Time.deltaTime * Speed;

       // _transform.position += _currentDirenction;
        //_transform.position += dirVector;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On Trigger Enter");


        //if (_currentDirenction != Vector3.zero)
        //    direction = _currentDirenction;
        //else
        //    direction = new Vector3(-1, 1 * _direction, 0).normalized;



        //float reflectionAngle = GetReflectionAngle();

        //Vector3 rotationVector = new Vector3(0, 0, reflectionAngle);

        //transform.Rotate(rotationVector);

        switch (other.gameObject.tag)
        {
            case ("brick"):

                IGolpeable target = other.transform.GetComponent(typeof(IGolpeable)) as IGolpeable;

                if (target != null && target.CanBeDestroyed)
                    target.GetDamage(_damage);

                _verticalDir = -1;
                Direction.y = -Direction.y;
               
                break;
            case ("Racket"):          
                _verticalDir = 1;
                Direction.y = -Direction.y;
                break;
            case ("SideBound"):
                Direction.x = -Direction.x;
                break;
            case ("LowerBound"):
                _transform.gameObject.SetActive(false);
                Direction = Vector3.zero;
                break;
        }
        

        Debug.Log($"NEXT DIR :: {_lastCatchedDir.x} / {_lastCatchedDir.y} _verticalDir --> {_verticalDir}");
        //Direction = _lastCatchedDir;
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
       
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Direction, 2.0f, 9);                  

        if(hit.collider != null && hit.collider != _collider)
        {
            Debug.Log($"HITTEN -- > {hit.transform.gameObject.name}");

            Hitten = hit.transform.gameObject;

            Vector2 vector = new Vector2(_transform.position.x, _transform.position.y);

            Vector2 hitVector = hit.point - vector;

            Vector2 localHitVector = _transform.InverseTransformVector(hitVector).normalized;

            //Vector2 localHit = _transform.InverseTransformVector(vector);

            Vector2 reflect = Vector2.zero;

            switch(hit.transform.tag)
            {
                
                case ("SideBound"):
                    //reflect = hit.point + hit.normal + (new Vector2(0, localHitVector.y > 0 ? localHitVector.y : 0.1f * _verticalDir) * 2); 
                    reflect = localHitVector;
                    reflect.x = -reflect.x;                                        
                    return reflect.normalized;
                case("brick"):
               
                    //localHitVector.y = localHitVector.y * _verticalDir;
                    return localHitVector.normalized;
                case ("Racket"):

                    localHitVector.x = -localHitVector.x;
                    return localHitVector.normalized;
                //reflect = hit.point + hit.normal + (new Vector2(localHitVector.x, 0) * 2);
                
            }
           


            //Vector2 localReflect = hit.transform.InverseTransformVector(reflect - hit.point).normalized;
            // Vector2 reflectpoint = new Vector2(-hitVector.x, 0) * 2;

           // return hit.transform.InverseTransformVector(reflect - hit.point).normalized;

            //Vector2 reflectingVector = reflect - hit.point;

            //return hit.transform.InverseTransformVector(reflectingVector).normalized;         
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
