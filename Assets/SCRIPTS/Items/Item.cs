using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private string _type = string.Empty;
    private Transform _tranform;

    public string Type { get; set; }
    public Transform GemTransform { get; set; }

    public abstract void ApplyBuff(Racket player);

    public virtual void Awake()
    {
        _tranform = transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
