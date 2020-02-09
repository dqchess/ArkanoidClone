using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGolpeable 
{
    bool CanBeDestroyed { get; set; }    
    int Hits { get; }
    void GetDamage(int damage);
}
