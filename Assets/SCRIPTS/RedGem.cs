using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGem : Item
{
   
    private int _dmgBuff;
    private float _buffTime;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();

        Type = transform.GetType().Name;

        _dmgBuff = 2;

        _buffTime = 5.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyBuff(Racket player)
    {
        throw new System.NotImplementedException();
    }
}
