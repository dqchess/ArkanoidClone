using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBrick : BrickBase
{
    public YellowBrick(BlockSocket socket) : base(socket)
    {
        Type = BrickType.Yellow;

        SetBrick();

        CanBeDestroyed = Type == BrickType.Brown ? true : false;        
    }  

    

    public override void Start()
    {
        base.Start();

        Type = BrickType.Yellow;

        SetBrick();      
    }

    
    void Update()
    {
        
    }
}
