using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGem : Item
{

    private int _dmgBuff;
    private float _buffTime;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        _dmgBuff = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyBuff(Racket player)
    {
        player.BrickBreaker.Damage += _dmgBuff;
    }
}
