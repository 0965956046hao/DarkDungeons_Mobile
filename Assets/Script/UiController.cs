using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    public Slider healthBar;
    public Text healthText;
    public Text coinText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public GameObject deathScreen;

    public string newGameScene, mainMenuScene;

    public GameObject pauseMenu, bigMapText, mapDisplay;

    public Image currentGun;
    public Text gunText;

    public Slider bossHealthBar;

    public float waitToCreate;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        fadeOutBlack = true;
        fadeToBlack = false;
        waitToCreate = 1;
        if (CharacterTracker.instance)
        {
            PlayerHeathController.instance.maxHeath = CharacterTracker.instance.maxHealth;
            PlayerHeathController.instance.currentHeath = CharacterTracker.instance.currentHealth;

            LevelManager.instance.currentCoin = CharacterTracker.instance.currentCoin;
        }


        currentGun.sprite = PlayerHeathController.instance.myPlayer.avalibleGuns[PlayerHeathController.instance.myPlayer.currentGun].gunUi;
        gunText.text = PlayerHeathController.instance.myPlayer.avalibleGuns[PlayerHeathController.instance.myPlayer.currentGun].weaponName;
        healthBar.maxValue = PlayerHeathController.instance.maxHeath;
        healthBar.value = PlayerHeathController.instance.currentHeath;
        healthText.text = PlayerHeathController.instance.currentHeath.ToString() + " / " + PlayerHeathController.instance.maxHeath.ToString();
        coinText.text = LevelManager.instance.currentCoin.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHeathController.instance.myPlayer)
        {
            currentGun.sprite = PlayerHeathController.instance.myPlayer.avalibleGuns[PlayerHeathController.instance.myPlayer.currentGun].gunUi;
            gunText.text = PlayerHeathController.instance.myPlayer.avalibleGuns[PlayerHeathController.instance.myPlayer.currentGun].weaponName;
        }    
        
        if (fadeOutBlack)
        {
            if (waitToCreate > 0)
                waitToCreate -= Time.deltaTime;
            else
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    fadeOutBlack = false;
                }
            }  
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
        Destroy(PlayerHeathController.instance.myPlayer.gameObject);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(mainMenuScene);
        Destroy(PlayerHeathController.instance.myPlayer.gameObject);
        Destroy(JTCanvas.instance.gameObject);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }    

    public void ActiveBigMap()
    {
        foreach(GameObject player in SpawnMultiplayer.instance.listPlayer)
        {
            if (player.GetComponent<PlayerController>().view.IsMine)
                player.GetComponent<PlayerController>().mainCam.GetComponent<CameraController>().activeBigMap = true;
        }    
    }    
}
