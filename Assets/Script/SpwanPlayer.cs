using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpwanPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.Instantiate(playerPrefab.name, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
