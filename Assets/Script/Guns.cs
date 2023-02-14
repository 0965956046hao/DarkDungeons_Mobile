using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Guns : MonoBehaviour
{

    public GameObject bulletToFire;
    public Transform[] firePoints;

    public float timeBetweenShot;
    private float shotCounter;

    public bool machineGun;

    public string weaponName;
    public Sprite gunUi;

    public int itemCost;
    public Sprite gunShopSprite;

    public static Guns Instance;
    public bool shouldShoot;

    public PhotonView view;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        shouldShoot = false;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if(shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                //PC
                /*if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    AudioManager.instance.PlaySFX(12);
                    
                    if(machineGun)
                    {
                        foreach (Transform firePoint in firePoints)
                        {
                            float i = Random.Range(-10, 10);
                            PlayerController.instance.gunArm.rotation = Quaternion.Euler(0, 0, PlayerController.instance.angle + i);
                            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        }
                    }
                    else
                    {
                        foreach (Transform firePoint in firePoints)
                        {
                            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        }
                    }
                    shotCounter = timeBetweenShot;
                }*/

                //MOBILE
                    Shoot();
                /*
                if (Input.GetMouseButton(0))
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        AudioManager.instance.PlaySFX(12);
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        shotCounter = timeBetweenShot;
                    }
                }*/
            }
        }
    }

    public void Shoot()
    {
        /*if(view.IsMine)
        {*/
            if (shouldShoot)
            {
                AudioManager.instance.PlaySFX(12);

                if (machineGun)
                {
                    foreach (Transform firePoint in firePoints)
                    {
                        float i = Random.Range(-10, 10);
                        foreach(GameObject player in SpawnMultiplayer.instance.listPlayer)
                        {   
                            if(player.GetComponent<PlayerController>().view.IsMine)
                                player.GetComponent<PlayerController>().gunArm.rotation = Quaternion.Euler(0, 0, player.GetComponent<PlayerController>().angle + i);
                    }    
                        
                        //Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        PhotonNetwork.Instantiate(bulletToFire.name, firePoint.position, firePoint.rotation);

                    }
                }
                else
                {
                    foreach (Transform firePoint in firePoints)
                    {
                        //Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        PhotonNetwork.Instantiate(bulletToFire.name, firePoint.position, firePoint.rotation);
                    }
                }
                shotCounter = timeBetweenShot;
            }
        //}
    }
}
