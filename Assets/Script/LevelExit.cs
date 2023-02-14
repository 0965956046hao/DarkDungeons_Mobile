using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelExit : MonoBehaviour
{

    public string levelToLoad;
    public List<GameObject> listPlayerExit = new List<GameObject>();
    private bool isAddList;
    // Start is called before the first frame update
    void Start()
    {
        isAddList = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (SpawnMultiplayer.instance.listPlayer.Count == 1)
            {
                //SceneManager.LoadScene(levelToLoad);
                //LevelManager.instance.triggerPoint = other.transform;
                StartCoroutine(LevelManager.instance.LevelEnd());
                CharacterTracker.instance.currentCoin = LevelManager.instance.currentCoin;
                CharacterTracker.instance.currentHealth = PlayerHeathController.instance.currentHeath;
                CharacterTracker.instance.maxHealth = PlayerHeathController.instance.maxHeath;
            }
            else
            {
                foreach(GameObject player in listPlayerExit)
                {
                    if (other.gameObject == player)
                        isAddList = true;
                    else
                        isAddList = false;
                } 
                if(isAddList == false)
                {
                    listPlayerExit.Add(other.gameObject);
                }    
            }    
              
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(listPlayerExit.Count == SpawnMultiplayer.instance.listPlayer.Count)
        {
            if (other.GetComponent<PlayerController>().isMasterPlayer == false)
            {
                StartCoroutine(LevelManager.instance.LevelEnd());
                CharacterTracker.instance.currentCoin = LevelManager.instance.currentCoin;
                CharacterTracker.instance.currentHealth = PlayerHeathController.instance.currentHeath;
                CharacterTracker.instance.maxHealth = PlayerHeathController.instance.maxHeath;
            }
        }    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (listPlayerExit.Count == SpawnMultiplayer.instance.listPlayer.Count)
        {
            if (collision.gameObject.GetComponent<PlayerController>().isMasterPlayer == false)
            {
                StartCoroutine(LevelManager.instance.LevelEnd());
                CharacterTracker.instance.currentCoin = LevelManager.instance.currentCoin;
                CharacterTracker.instance.currentHealth = PlayerHeathController.instance.currentHeath;
                CharacterTracker.instance.maxHealth = PlayerHeathController.instance.maxHeath;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        listPlayerExit.Remove(collision.gameObject);
    }
}
