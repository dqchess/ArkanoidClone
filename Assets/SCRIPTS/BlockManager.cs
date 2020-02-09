using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private static BlockManager _instance;
    public static BlockManager Instance 
    { 
        get 
        { 
            if(_instance == null)
            {
                GameObject go = new GameObject("BlockManager");
                
                go.AddComponent<BlockManager>();

                _instance = go.GetComponent<BlockManager>();
            }

            return _instance;
        } 
    }

    private Transform _transform;
    private List<BlockSocket> _blockSockets;
    private int _maxBlocks = 0;
    private int _brownCount = 0;
    private int _yellowCount = 0;
    private int _blueCount = 0;
    private int _lightBlueCount = 0;

    public List<BlockSocket> BlockSockets { get => _blockSockets; set => _blockSockets = value; }

    //List BrickBase
        // en BrickBase --> enum BrickType
        // Spawn() --> if yellowCount < maxYellow Spawn (BrickList-Single(b => b.Type == BrickType.Yellow);
        // StatePattern ?
             // en BrickSocket.cs
            // BrickBase CurrentBrick = SpawnBrick(BrickType.Yellow) devuelve un Brick 

    public GameObject YellowBrick;
    public GameObject LightBlueBrick;
    public GameObject BlueBrick;
    public GameObject BrownBrick;

    void Awake()
    {
        _instance = this;
        _transform = transform;

        _maxBlocks = _transform.childCount;
        _brownCount = Mathf.RoundToInt( _maxBlocks / 3);
        _yellowCount = Mathf.RoundToInt(_maxBlocks / 3);
        _blueCount = Mathf.RoundToInt((_maxBlocks / 3) / 2);
        _lightBlueCount = Mathf.RoundToInt((_maxBlocks / 3) / 2);

        _blockSockets = GetBrickSockets();

       

       
    }

    void Start()
    {
        // FillSockets(_blockSockets);

        //CheckForEmpties();
        FillSockets(_blockSockets);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FillSockets(List<BlockSocket> sockets)
    {
        for(int i = 0; i < _maxBlocks; i++)
        {
            bool canSpawn = false;

            BlockSocket socket = null;
            
            while(!canSpawn)
            {
                int socketNum = Random.Range(0, 36);

                socket = sockets[socketNum].GetComponent<BlockSocket>();

                SpawnBrick(socket);

                canSpawn = true;
            }
          
        }
    }

    private List<BlockSocket> GetBrickSockets()
    {
        List<BlockSocket> sockets = new List<BlockSocket>();

        for(int i = 0; i < _transform.childCount; i++)
        {
            BlockSocket socket = _transform.GetChild(i).GetComponent<BlockSocket>();
            
            sockets.Add(socket);
        }

        return sockets;
    }

    private void SpawnBrick(BlockSocket socket)
    {
        if (_brownCount > 0)
        {
            socket.Initialize((int)BrickType.Brown);
            _brownCount--;
            return;
        }
        else if(_blueCount > 0)
        {
            socket.Initialize((int)BrickType.Blue);
            _blueCount--;
            return;
        }
        else if(_lightBlueCount > 0)
        {
            socket.Initialize((int)BrickType.LightBlue);
            _lightBlueCount--;
            return;
        }
        else if(_yellowCount > 0)
        {           
            socket.Initialize((int)BrickType.Yellow);
            _yellowCount--;                     
        }
    }

    private void CheckForEmpties()
    {        
        var emptyBricks = _blockSockets.Where(b => b.Brick == null).ToList();

        emptyBricks.ForEach(e => SpawnBrick(e));
        
    }
}
