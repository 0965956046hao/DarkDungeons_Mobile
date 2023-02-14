using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public int currentCoin;

    public bool isPaused;

    public Transform startPoint;
    public List<GameObject> listPlayer = new List<GameObject>();

    public Transform triggerPoint;
    //public bool setTranform;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //listPlayer = SpawnMultiplayer.instance.listPlayer;
        foreach(GameObject player in SpawnMultiplayer.instance.listPlayer)
        {
            player.transform.position = startPoint.position;
            player.GetComponent<PlayerController>().canMove = true;
        }    
        currentCoin = CharacterTracker.instance.currentCoin;
        UiController.instance.coinText.text = currentCoin.ToString();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }  
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();
        UiController.instance.StartFadeToBlack();
        foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
        {
            //player.GetComponent<PlayerController>().canMove = false;
            //player.transform.position = triggerPoint.transform.position;
        }
       
        yield return new WaitForSeconds(waitToLoad);
        
        SceneManager.LoadScene(nextLevel);
        //PhotonNetwork.LoadLevel(nextLevel);
        
        CharacterTracker.instance.currentCoin = currentCoin;
        CharacterTracker.instance.currentHealth = PlayerHeathController.instance.currentHeath;
        CharacterTracker.instance.maxHealth = PlayerHeathController.instance.maxHeath;
    }

    public void GetCoins(int mount)
    {
        currentCoin += mount;
        UiController.instance.coinText.text = currentCoin.ToString();
    }

    public void SpendCoins(int mount)
    {
        currentCoin -= mount;

        if(currentCoin < 0)
        {
            currentCoin = 0;
        }
        UiController.instance.coinText.text = currentCoin.ToString();
    }
    
    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UiController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }   
        else
        {
            UiController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           listPlayer.Add(collision.gameObject);
        }
    }
}
