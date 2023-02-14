using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoomOutLine : MonoBehaviour
{
    public List<GameObject> listRoomOutLine = new List<GameObject>();
    public static ListRoomOutLine instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
