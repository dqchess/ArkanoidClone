using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlueBrick : BrickBase
{
    public LightBlueBrick(BlockSocket socket) : base (socket)
    {

    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Type = BrickType.LightBlue;

        SetBrick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
