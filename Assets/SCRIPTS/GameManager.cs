using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;
    public static GameManager Instance 
    { 
        get 
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                
                go.AddComponent<GameManager>();
                
                _instance = go.GetComponent<GameManager>();
            }

            return _instance;
        }
    }

    public BrickBreaker BreakerPrefab;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
