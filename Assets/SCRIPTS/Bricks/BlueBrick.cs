using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBrick : BrickBase
{
    public BlueBrick(BlockSocket socket) : base(socket)
    {

    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Type = BrickType.Blue;

        SetBrick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
