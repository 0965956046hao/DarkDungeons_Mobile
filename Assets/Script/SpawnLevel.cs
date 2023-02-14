using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnLevel : MonoBehaviour
{
    public GameObject levelPrefab;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevel()
    {
        PhotonNetwork.Instantiate(levelPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
