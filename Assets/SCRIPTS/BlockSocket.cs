using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSocket : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool _isEmpty = true;

    private BrickBase _socketBrick = null;
    [SerializeField]
    private int _currentBrickType = 0;
    private GameObject _brickGO = null;

    public BrickBase Brick { get => _socketBrick; set => _socketBrick = value; }
    public int CurrentBrickType { get => _currentBrickType; set => _currentBrickType = value; }

    private void Awake()
    {
        
    }
    void Start()
    {
       if (_brickGO == null)
        {
            _brickGO = Instantiate(BlockManager.Instance.YellowBrick, this.transform) as GameObject;

            _socketBrick = _brickGO.GetComponent<BrickBase>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    public void SwitchBrick(int brickType)
    {
        _currentBrickType = brickType;
    }
    public void CheckState()
    {
        if (CurrentBrickType != (int)_socketBrick.Type)
            SwitchType(_currentBrickType);
    }

    private void SwitchType(int brickYpe)
    {
        Destroy(_brickGO);

        switch (brickYpe)
        {
            case (0):
                
                _brickGO = Instantiate(BlockManager.Instance.YellowBrick, this.transform) as GameObject;
                break;
            case (1):
                _brickGO = Instantiate(BlockManager.Instance.LightBlueBrick, this.transform) as GameObject;
                break;
            case (2):
                _brickGO = Instantiate(BlockManager.Instance.BlueBrick, this.transform) as GameObject;
                break;
            case (3):
                _brickGO = Instantiate(BlockManager.Instance.BrownBrick, this.transform) as GameObject;                
                break;
            default:
                _brickGO = Instantiate(BlockManager.Instance.YellowBrick, this.transform) as GameObject;
                break;
        }
    }

    public void Initialize(int brickType)
    {
        if (_brickGO != null)
        {
            Destroy(_brickGO.transform.gameObject);

            SwitchType(brickType);

            _socketBrick = _brickGO.GetComponent<BrickBase>();

        }
    }

}
