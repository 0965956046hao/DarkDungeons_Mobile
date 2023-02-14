using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool closeWhenEnter/*, openWhenEnemiesCleared*/;

    public GameObject[] doors;

    //public List<GameObject> enemies = new List<GameObject>();

    //public List<EnemiesController> enemiesController = new List<EnemiesController>();

    public bool roomActive;

    public GameObject mapHider;
    public Vector3 myPlayerTranform;

    public GameObject joinPlayer;
    public List<GameObject> listJoinPlayer = new List<GameObject>();
    public List<GameObject> listJoinedPlayer = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ListCenter.instance.listRoomOutLine.Add(this);
        foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
            listJoinPlayer.Add(player);
        foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
            listJoinedPlayer.Add(player);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in listJoinPlayer)
            if (player == null)
                listJoinPlayer.Remove(player);
        foreach (GameObject player in listJoinedPlayer)
            if (player == null)
                listJoinedPlayer.Remove(player);
        /*if(roomActive)
        {
            foreach (EnemiesController enemyController in enemiesController)
            {
                enemyController.isActiveActack = true;
            }
        }
        else
        {
            foreach (EnemiesController enemyController in enemiesController)
            {
                enemyController.isActiveActack = false;
            }
        }
        

        if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
             for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }

                if(enemies.Count == 0)
                {
                    foreach(GameObject door in doors)
                    {
                        door.SetActive(false);

                        closeWhenEnter = false;
                    }
                }
            }
        }*/

    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEnter = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("in");
            if (closeWhenEnter)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }

                myPlayerTranform = other.transform.position;
                joinPlayer = other.gameObject;
                listJoinPlayer.Remove(joinPlayer);
                foreach (GameObject player in listJoinPlayer)
                {
                    if(player != joinPlayer)
                    {
                        Debug.Log("transform");
                        player.transform.position = myPlayerTranform;
                    }
                   
                }
            }
            
            roomActive = true;
            
            mapHider.SetActive(false);
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            listJoinedPlayer.Remove(other.gameObject);
            if(listJoinedPlayer.Count == 0)
                roomActive = false;
        }
    }
}
