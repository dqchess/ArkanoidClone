using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _instance = this;
    }

    private List<BlockSocket> _blockSockets;
    private int _maxBlocks = 36;

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

    void Start()
    {
        _blockSockets = new List<BlockSocket>(_maxBlocks);

        FillSockets(_blockSockets);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestFill()
    {
        
    }

    private void FillSockets(List<BlockSocket> sockets)
    {

    }
}
