using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCenter : MonoBehaviour
{
    public List<RoomCenter> listCenter = new List<RoomCenter>();
    public static ListCenter instance;
    public List<RoomController> listRoomOutLine = new List<RoomController>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called  once per frame
    void Update()
    {
        if (listCenter.Count > 0)
        {
            
            for(int i = 0; i < listCenter.Count; i++)
            {
                listCenter[i].theRoom = listRoomOutLine[i];
            }    

        }    
    }
}
