using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealthUpgrade, isWeapon, isPoisionResistance, isSpiteResistance, isIceResistance;

    public int itemCost;

    public int healthUpgradeAmount;

    public Guns[] potentiaGuns;
    private Guns theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;
    public PlayerController myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if(isWeapon)
        {
            int selectedGun = Random.Range(0, potentiaGuns.Length);
            theGun = potentiaGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - ";
            itemCost = theGun.itemCost; 
        }
        foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
            if (player.GetComponent<PlayerController>().view.IsMine)
                myPlayer = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inBuyZone)
        {
            if(Input.GetKeyDown(KeyCode.E) || FixedJoystick.instance.isClicked)
            {
                if(LevelManager.instance.currentCoin >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);
                    
                    if(isHealthRestore)
                    {
                        PlayerHeathController.instance.HealPlayer(PlayerHeathController.instance.maxHeath / 2);
                    }

                    if(isHealthUpgrade)
                    {
                        PlayerHeathController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    if (isSpiteResistance)
                    {
                        myPlayer.spiteResistance = true;
                    }

                    if (isPoisionResistance)
                    {
                        myPlayer.poisonResistance = true;
                    }
                    if (isIceResistance)
                    {
                        myPlayer.iceResistance = true;
                    }
                    if(isWeapon)
                    {
                        Guns gunClone = Instantiate(theGun);
                        gunClone.transform.parent = myPlayer.gunArm;
                        gunClone.transform.position = myPlayer.gunArm.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        myPlayer.avalibleGuns.Add(gunClone);
                        myPlayer.currentGun = PlayerController.instance.avalibleGuns.Count - 1;
                        myPlayer.SwitchGun();
                    }
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    AudioManager.instance.PlaySFX(18);
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
