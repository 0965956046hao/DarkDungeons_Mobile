using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public CreateAndJoinRooms createRoom;
    public bool isMultiplayer;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if(isMultiplayer)
        {
            SceneManager.LoadScene("Lobby");
        }
        else
            createRoom.CreateRoom();
    }
}

