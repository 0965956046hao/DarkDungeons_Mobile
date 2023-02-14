using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;

    public List<GameObject> enemies = new List<GameObject>();

    public List<EnemiesController> enemiesController = new List<EnemiesController>();

    public RoomController theRoom;

    public bool shopRoom;

    public List<GameObject> shopItems;

    public Transform[] itemPositions;
    public static RoomCenter instance;

    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ListCenter.instance.GetComponent<ListCenter>().listCenter.Add(this);
        if(openWhenEnemiesCleared)
        {
            theRoom.closeWhenEnter = true;
        }
        if(shopRoom)
        {
            foreach( Transform itemPosition in itemPositions)
            {
                int itemSelcet = Random.Range(0, shopItems.Count);
                Instantiate(shopItems[itemSelcet], itemPosition.transform.position, itemPosition.rotation);
                shopItems.RemoveAt(itemSelcet);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (openWhenEnemiesCleared)
        {
            theRoom.closeWhenEnter = true;
        }
        if (theRoom.roomActive)
        {
            foreach (EnemiesController enemyController in enemiesController)
            {
                enemyController.isActiveActack = true;
                
            }
            //PlayerController.instance.enemies = enemies;
            
        }
        else
        {
            foreach (EnemiesController enemyController in enemiesController)
            {
                enemyController.isActiveActack = false;
            }
        }


        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }

                if (enemies.Count == 0)
                {
                    theRoom.OpenDoors();
                    openWhenEnemiesCleared = false;
                }
            }
        }

    }
    //MOBILE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach (GameObject enemy in enemies)
            {
                collision.GetComponent<PlayerController>().enemies.Add(enemy);
            }
            foreach (EnemiesController enemieController in enemiesController)
            {
                enemieController.AddPlayer(collision.gameObject);
            }
        }

    }
}
