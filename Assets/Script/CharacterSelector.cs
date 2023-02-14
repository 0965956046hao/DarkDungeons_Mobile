using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterSelector : MonoBehaviour
{
    [HideInInspector]
    public bool canSelect;


    public GameObject message;

    public PlayerController playerToSpawn;

    public bool doseNotUnCock;

    // Start is called before the first frame update
    void Start()
    {
        if(!doseNotUnCock)
        {
            if (PlayerPrefs.HasKey(playerToSpawn.name))
            {
                if (PlayerPrefs.GetInt(playerToSpawn.name) == 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSelect)
        {
            if(Input.GetKeyDown(KeyCode.E) || FixedJoystick.instance.isClicked )
            {
                AudioManager.instance.PlaySFX(20);
                Vector3 playerPos = new Vector3();
                //PlayerController playToSwitch = new PlayerController();
                foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
                {
                    if(player.GetComponent<PlayerController>().view.IsMine)
                    {
                        playerPos = player.transform.position;
                        //playToSwitch = player.GetComponent<PlayerController>();
                        
                        //SpawnMultiplayer.instance.listPlayer.Remove(player);
                        PlayerController newPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, playerPos, playerToSpawn.transform.rotation).GetComponent<PlayerController>();
                        //playToSwitch = newPlayer;
                        PhotonNetwork.Destroy(player);

                        gameObject.SetActive(false);

                        CharacterSelectManager.instance.activePlayer = newPlayer;
                        CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true);
                        CharacterSelectManager.instance.activeCharSelect = this;
                    }
                } 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = false   ;
            message.SetActive(false);
        }
    }
}
