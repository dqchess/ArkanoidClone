using Assets.SCRIPTS.Bricks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreaker : MonoBehaviour
{
    private Transform _transform;
    private float Speed = 3.0f;
    private float _movementDamping = 100.0f;
    private CircleCollider2D _collider = null;
    private int _verticalDir = 0;
    private int _horizontalDir = 0;
    private float _acceleration = 0.0f;
    private int _damage = 1;

    private bool _racketIsMoving = false;
    private bool _isKickedBack = false;
    private bool _isSteepAngle = false;
    private bool _isColliding = false;  // para evitar que interaccione con otros bricks = 1 rebote, 1 cambio de direction.Y, 1 Destroy(brick)
    
    private BounceType _bounceType = BounceType.Default;

   

    public Vector3 Direction = Vector3.zero;

    public GameObject Hitten;
    [SerializeField]
    Vector3 _currentDirection = Vector3.zero;
    [SerializeField]
    Vector2 _lastCatchedDir = Vector2.zero;
    public int Damage { get => _damage; set => _damage = value; }
    public bool RacketIsMoving { set => _racketIsMoving = value; }
    void Start()
    {
        _transform = transform;
        _collider = _transform.GetComponent<CircleCollider2D>();
        _verticalDir = 1;
        _horizontalDir = 0;
        Direction = new Vector3(-0.8f, -0.3f, 0).normalized;  // startDirection --> en gameManager --> random dir, random pos --> segun nivel? 
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

       // _transform.Translate(Direction * Speed * Time.deltaTime);
        _transform.position += Direction * Time.deltaTime * Speed;

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

        if(!_isColliding)
        {
            _isColliding = true;

            Vector3 rotationvector = new Vector3(0, 0, 360);

            transform.Rotate(rotationvector);

            switch (other.gameObject.tag)
            {
                case ("brick"):

                    IGolpeable target = other.transform.GetComponent(typeof(IGolpeable)) as IGolpeable;

                    if (target != null && target.CanBeDestroyed)
                        target.GetDamage(_damage);

                    _verticalDir = -1;

                    if (_lastCatchedDir == Vector2.zero)
                    {
                        _lastCatchedDir = Direction;
                        _lastCatchedDir.y = -_lastCatchedDir.y;
                    }

                    // Direction.y = -Direction.y;

                    break;
                case ("Racket"):

                    //  Direction.y = -Direction.y;

                    break;
                case ("SideBound"):

                    break;
                case ("LowerBound"):
                    _transform.gameObject.SetActive(false);
                    Direction = Vector3.zero;
                    break;
            }


            Debug.Log($"NEXT DIR :: {_lastCatchedDir.x} / {_lastCatchedDir.y} _verticalDir --> {_verticalDir}");

            Direction = _lastCatchedDir;

            _isSteepAngle = false;
        }

       
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isColliding = !_isColliding;
        _isSteepAngle = false;
    }

    private float GetReflectionAngle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Direction, 3.0f, 9);
       
        if (hit.collider != null)
        { 
             float angle = Vector2.Angle(hit.normal, -direction);
            // float angle2 = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
             return  angle;            
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

            
            float angle = GetReflectionAngle(hitVector.normalized);

            // return SetBounceDirection(localHitVector.normalized, hit.transform.tag);

            switch (hit.transform.tag)
            {

                case ("SideBound"):
                    

                    if (angle <= 20)
                    {
                        _isSteepAngle = true;

                        _bounceType = GetBoundBounceType();

                        return SetBounceDirection(localHitVector, hit.transform.tag);
                    }

                    return -localHitVector;

                //localHitVector.x = -localHitVector.x;  

                //return localHitVector.normalized;

                case ("brick"):

                    //if (Mathf.Abs(angle) <= 30.0f)
                    //    return -localHitVector;
                    localHitVector.y = Mathf.Sign(hit.normal.y) > 0 ? -localHitVector.y : localHitVector.y;
                    
                    return SetBounceDirection(localHitVector.normalized, hit.transform.tag);


                case ("Racket"):

                    if (Mathf.Abs(angle) >= 65.0f)
                        _isSteepAngle = true;

                    _bounceType = GetRacketBounceType();

                    return SetBounceDirection(localHitVector.normalized, hit.transform.tag);

                    //if (Mathf.Abs(angle) >= 65.0f)
                    //{
                    //    _isSteepAngle = true;

                    //    _bounceType = GetRacketBounceType();

                    //    return SetBounceDirection(localHitVector, hit.transform.tag);

                    //    switch ((int)_bounceType)
                    //    {
                    //        case (1):
                    //            return -localHitVector;
                    //        case (2):
                    //            localHitVector.y = -localHitVector.y;
                    //            return localHitVector;
                    //        case (0):
                    //            return localHitVector;
                    //    }
                    //}


                    localHitVector.y = -localHitVector.y * -_verticalDir;

                    return localHitVector.normalized;

                case ("Bound"):

                    localHitVector.y = -localHitVector.y * -_verticalDir;

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
        Vector3 normal = new Vector2(0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 2.0f);

        if (hit.collider != null)
        {
            normal = hit.normal;
        }

        Vector3 rightVector = new Vector3(1, 0, 0);

        return hit.transform.InverseTransformVector(normal + rightVector).normalized * Time.deltaTime * Speed;

    }

    private BounceType GetBoundBounceType()
    {
        if ((_isSteepAngle && _racketIsMoving))
            return BounceType.KickedBack;

        else if (_isSteepAngle && _isKickedBack)
            return BounceType.Perpendicular;

        else return BounceType.Default;
    }

    private BounceType GetRacketBounceType()
    {
        switch (_isSteepAngle)
        {
            case (true):

                switch(_racketIsMoving)
                {
                    case (true):

                        switch(_isKickedBack)
                        {
                            case (true):
                                break;
                            case (false):
                                break;
                        }

                        break;

                    case (false):

                        switch (_isKickedBack)
                        {
                            case (true):
                                break;
                            case (false):
                                break;
                        }

                        break;
                }

                break;

            case (false):

                switch(_racketIsMoving)
                {
                    case (true):
                        break;

                    case (false):
                        break;
                }

                break;
        }

        return BounceType.Default;
    }

    private Vector3 SetBounceDirection(Vector3 hitVector, string hitTag)
    {
        switch(hitTag)
        {
            case ("Brick"):

                hitVector.y = hitVector.y * -_verticalDir;

                return hitVector.normalized;
                
            case ("SideBound"):

                switch (Mathf.Sign(hitVector.y))
                {
                    case (1):
                        return -hitVector;

                    case (-1):
                        
                        hitVector.x = -hitVector.x;
                        
                        return hitVector.normalized;
                }
                break;

            case ("Racket"):

                switch ((int)_bounceType)
                {
                    case (1):                          // KickedBack
                        return -hitVector;
                    
                    case (2):                          // Perpendicular
                        hitVector.y = -hitVector.y;
                        return hitVector;
                   
                    case (0):                         // default
                    default:
                        return hitVector;
                }

        }

        return Vector3.zero;
    }
}
