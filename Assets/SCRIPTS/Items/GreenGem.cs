﻿using System.Collections;
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
        
        Type = transform.GetType().Name;
        
        _dmgBuff = 1;
        
        _buffTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyBuff(Racket player)
    {
        if(!player.HasBuff)
        {
            player.BrickBreaker.Damage += _dmgBuff;
            player.BuffTime = _buffTime;
        }       
    }
}
