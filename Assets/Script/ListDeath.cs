using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListDeath : MonoBehaviour
{
    public int deathCount;
    public static ListDeath instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {  
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
