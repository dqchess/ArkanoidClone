using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlueBrick : BrickBase
{
    public LightBlueBrick(BlockSocket socket) : base (socket) {}

    
    public override void Start()
    {
        base.Start();

        Type = BrickType.LightBlue;

        SetBrick();
    }

    
    void Update()
    {
        
    }
}
