using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTCanvas : MonoBehaviour
{
    public static JTCanvas instance;
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
