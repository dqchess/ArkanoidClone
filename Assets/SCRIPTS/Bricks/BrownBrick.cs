using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBrick : BrickBase
{
    public BrownBrick(BlockSocket socket) : base(socket)
    {
        Type = BrickType.Brown;

        SetBrick();
    }
   

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Type = BrickType.Brown;
        
        SetBrick();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
