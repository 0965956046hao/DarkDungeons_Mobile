using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHeathController : MonoBehaviour
{
    public static PlayerHeathController instance;

    public int currentHeath;
    public int maxHeath;

    public float damageInvincLenght;
    private float invincCount;

    public PlayerController myPlayer;

    public PlayerController otherPlayer;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentHeath = maxHeath;
        maxHeath = CharacterTracker.instance.maxHealth;
        currentHeath = CharacterTracker.instance.currentHealth;

        UiController.instance.healthBar.maxValue = maxHeath;
        UiController.instance.healthBar.value = currentHeath;
        UiController.instance.healthText.text = currentHeath.ToString() + " / " + maxHeath.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in SpawnMultiplayer.instance.listPlayer)
        {
            if (player.GetComponent<PlayerController>().view.IsMine)
                myPlayer = player.GetComponent<PlayerController>();
            else
                otherPlayer = player.GetComponent<PlayerController>();
        }
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;
            if(invincCount <=0)
            {
                myPlayer.anim.SetBool("beDamage", false);
                myPlayer.bodySR.color = new Color(myPlayer.bodySR.color.r, myPlayer.bodySR.color.g, myPlayer.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if(invincCount <=0)
        {
            currentHeath--;

            AudioManager.instance.PlaySFX(11);

            invincCount = damageInvincLenght;

            myPlayer.anim.SetBool("beDamage", true);

            UiController.instance.healthBar.value = currentHeath;
            UiController.instance.healthText.text = currentHeath.ToString() + " / " + maxHeath.ToString();

            if (currentHeath <= 0)
            {
                AudioManager.instance.PlaySFX(10);
                SpawnMultiplayer.instance.listPlayer.Remove(myPlayer.gameObject);
                Destroy(myPlayer.gameObject);
                if (otherPlayer != null)
                    otherPlayer.mainCam.SetActive(true);
               if(ListDeath.instance)
                {
                    PhotonNetwork.Instantiate("All Death Flag", transform.position, transform.rotation);
                }   
               else
                    PhotonNetwork.Instantiate("List Death", transform.position, transform.rotation);
                if(SpawnMultiplayer.instance.listPlayer.Count == 0)
                {
                    AudioManager.instance.PlayGameOver();
                    UiController.instance.deathScreen.SetActive(true);

                }
            }
        }
    }

    public void MakeInvicible(float length)
    {
        invincCount = length;
        myPlayer.bodySR.color = new Color(myPlayer.bodySR.color.r, myPlayer.bodySR.color.g, myPlayer.bodySR.color.b, .5f);

    }

    public void HealPlayer(int healAmount)
    {
        currentHeath += healAmount;
        if(currentHeath > maxHeath)
        {
            currentHeath = maxHeath;
        }
        UiController.instance.healthBar.value = currentHeath;
        UiController.instance.healthText.text = currentHeath.ToString() + " / " + maxHeath.ToString();
    }


    public void IncreaseMaxHealth(int amount)
    {
        maxHeath += amount;
        currentHeath += amount;

        UiController.instance.healthBar.maxValue = maxHeath;
        UiController.instance.healthBar.value = currentHeath;
        UiController.instance.healthText.text = currentHeath.ToString() + " / " + maxHeath.ToString();    
    }
}
