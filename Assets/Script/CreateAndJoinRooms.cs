using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField createInput;
    public InputField joinInput;
    public bool isMultiplayer;

    public void CreateRoom()
    {
        if(createInput)
            PhotonNetwork.CreateRoom(createInput.text);
        else
            PhotonNetwork.CreateRoom("asd");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        if(isMultiplayer)
        {
            PhotonNetwork.LoadLevel("Character Select");
        }    
        else
        {
            PhotonNetwork.LoadLevel("Character Select");
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
