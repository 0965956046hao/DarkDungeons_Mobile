using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemiesController : MonoBehaviour
{
    public static EnemiesController instance;

    public Rigidbody2D theRB;
    public float moveSpeed;
    public bool isActiveActack;

    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Run Away")]
    public bool shouldRunAway;
    public float rangeRunAway;
    
    [Header("Wander")]
    public bool shouldWander;
    public float wanderLenght, pauseLengt;
    private float wanderCounter, pauseCouter;
    private Vector3 wanderDirection;


    public Animator anim;

    public int heath = 150;

    public GameObject deadSplatters;
    public GameObject bloodingEffect;
    public GameObject hitEnemyEffect;

    
    [Header("Shoot Enemy")]
    public bool shouldShoot;
    public bool rotationWeapon;

    [Header("Witch Enemy")]
    public bool witch;
    public float timeBewentAtack = 0.5f;
    private float atack2TimeCounter;

    [Header("Rush Enemy")]
    public bool rush;
    public float rushRange;
    //[HideInInspector]
    public float activeMoveSpeed;
    public float rushSpeed = 8f, rushLength = 0.5f, rushCooldown = 1f;
    private float rushCoolCouter;
    [HideInInspector]
    public float rushCounter;

    [Header("Knight Enemy")]
    public bool knight;
    public float swingRange;
    public float swingCoolDown;
    private float swingCouter;

    [Header("Shooting Staion")]
    public bool shootingStation;
    public float timetoActack3 ;
    private float actack3Counter;
    public int maxBullet;
    public GameObject slowBullet;

    [Header("Zombie Enemy")]
    public bool zombie;
    public GameObject deadWalking;

    [Header("Walking Dead")]
    public bool walkingDead;
    public float timeToExplode;
    private float explodeCounter;
    public GameObject poisonArea;
    public GameObject iceEffect;


    public GameObject bullet;
    public Transform[] firePoint;

    public float fireRate;
    private float fireCounter;

    public Transform weaponArm;

    public float fireRange;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    public List<GameObject> listPlayer = new List<GameObject>();
    public GameObject targetPlayer;

    public PhotonView view;
    private bool isAddList;
    public bool activeInHierarchy;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        fireCounter = fireRate;
        
        if(walkingDead)
        {
            isActiveActack = true;
            listPlayer = LevelManager.instance.listPlayer;
        }    
        else
            isActiveActack = false;
        atack2TimeCounter = timeBewentAtack;
        activeMoveSpeed = moveSpeed;
        swingCouter = swingCoolDown;
        explodeCounter = timeToExplode;
        actack3Counter = timetoActack3;
        if (shouldWander)
        {
            pauseCouter = pauseCouter = Random.Range(pauseLengt * 0.75f, pauseLengt * 1.25f);
        }
        targetPlayer = PlayerController.instance.gameObject;
        view = GetComponent<PhotonView>();
          
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in listPlayer)
        {
            if (player == null)
                listPlayer.Remove(player);
            if (player.GetComponent<PlayerController>().gameObject.activeInHierarchy)
                activeInHierarchy = true;

        }

        if(activeInHierarchy)
        {

            //enemies move
            //Debug.Log(Vector3.Distance(transform.position, PlayerController.instance.transform.position));
            moveDirection = Vector3.zero;
            targetPlayer = FindTarget(listPlayer);
            if (Vector3.Distance(transform.position, targetPlayer.transform.position) < rangeToChasePlayer && shouldChasePlayer == true && isActiveActack)
            {
                moveDirection = targetPlayer.transform.position - transform.position;
            }
            else
            {
                //wander
                if(shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection;
                        if(wanderCounter <= 0)
                        {
                            pauseCouter = Random.Range(pauseLengt * 0.75f, pauseLengt * 1.25f);
                        }
                    }

                    if(pauseCouter > 0)
                    {
                        pauseCouter -= Time.deltaTime;
                        if(pauseCouter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLenght * 0.75f, wanderLenght * 1.25f);
                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }
            }

            //move away
            if (shouldRunAway && Vector3.Distance(transform.position, targetPlayer.transform.position) <= rangeRunAway && isActiveActack)
            {
                moveDirection = transform.position - targetPlayer.transform.position;
            }

            moveDirection.Normalize();
            theRB.velocity = moveDirection * activeMoveSpeed;


            if (isActiveActack)
            {
                // rotation weapon & rotation body
                if (rotationWeapon)
                {
                    Vector2 offset = new Vector2(targetPlayer.transform.position.x - transform.position.x, targetPlayer.transform.position.y - transform.position.y);
                    float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

                    weaponArm.rotation = Quaternion.Euler(0, 0, angle);
                    if (targetPlayer.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        weaponArm.localScale = new Vector3(1f, -1f, 1f);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        weaponArm.localScale = new Vector3(-1f, 1f, 1f);
                    }
                }
                else
                {
                    // witch is special
                    if (witch)
                    {
                        if (targetPlayer.transform.position.x < transform.position.x)
                        {
                            transform.localScale = Vector3.one;
                            foreach (Transform firepoint in firePoint)
                            {
                                firepoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                            }
                        }
                        else
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                            foreach (Transform firepoint in firePoint)
                            {
                                firepoint.rotation = Quaternion.Euler(0f, 0f, 360f);
                            }
                        }
                    }
                    else
                    {
                        if (targetPlayer.transform.position.x < transform.position.x)
                        {
                            transform.localScale = Vector3.one;
                        }
                        else
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                    }
                }





                //shooting
                if (shouldShoot)
                {
                    if (Vector3.Distance(transform.position, targetPlayer.transform.position) < fireRange)
                    {

                        // fire
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0 && firePoint.Length != 0)
                        {
                            if (witch)
                            {
                                //shouldChasePlayer = false;
                                shouldRunAway = false;

                                weaponArm.position = new Vector3(weaponArm.position.x, weaponArm.position.y + 0.3f, weaponArm.position.z);
                                weaponArm.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            }

                            for (int i = 0; i < firePoint.Length; i++)
                            {
                                AudioManager.instance.PlaySFX(12);
                                PhotonView.Instantiate(bullet, firePoint[i].position, firePoint[i].rotation);
                                fireCounter = fireRate;
                            }
                        }
                        if (witch || shootingStation)
                        {
                            atack2TimeCounter -= Time.deltaTime;
                            if (atack2TimeCounter <= 0)
                            {

                                if (firePoint.Length != 0)
                                {
                                    for (int i = 0; i < firePoint.Length; i++)
                                    {
                                        AudioManager.instance.PlaySFX(12);
                                        PhotonView.Instantiate(bullet, firePoint[i].position, firePoint[i].rotation);
                                        atack2TimeCounter = timeBewentAtack;
                                        fireCounter = fireRate;
                                    }
                                }
                                weaponArm.position = new Vector3(weaponArm.position.x, weaponArm.position.y - 0.3f, weaponArm.position.z);
                                weaponArm.localScale = new Vector3(1f, 1f, 1f);
                                //shouldChasePlayer = true;
                                shouldRunAway = true;
                            }
                        }
                        if (shootingStation)
                        {
                            shouldRunAway = false;
                            actack3Counter -= Time.deltaTime;
                            if (actack3Counter <= 0)
                            {
                                for (int i = 0; i < maxBullet; i++)
                                {
                                    AudioManager.instance.PlaySFX(12);
                                    PhotonView.Instantiate(slowBullet, transform.position, transform.rotation);
                                    atack2TimeCounter = timeBewentAtack;
                                    fireCounter = fireRate;
                                    actack3Counter = timetoActack3;
                                }
                            }
                        }
                    }
                }

                //rush
                if (rush)
                {
                    if (Vector3.Distance(transform.position, targetPlayer.transform.position) < rushRange)
                    {

                        if (rushCoolCouter <= 0 && rushCounter <= 0)
                        {
                            activeMoveSpeed = rushSpeed;
                            rushCounter = rushLength;
                            anim.SetTrigger("rush");
                            AudioManager.instance.PlaySFX(8);
                        }

                        if (rushCounter > 0)
                        {
                            rushCounter -= Time.deltaTime;
                            if (rushCounter <= 0)
                            {
                                activeMoveSpeed = moveSpeed;
                                rushCoolCouter = rushCooldown;
                            }
                        }

                        if (rushCooldown > 0)
                        {
                            rushCoolCouter -= Time.deltaTime;
                        }
                    }
                }

                //knight
                if (knight && Vector3.Distance(transform.position, targetPlayer.transform.position) < swingRange)
                {
                    swingCouter -= Time.deltaTime;

                    if (swingCouter <= 0)
                    {
                        anim.SetTrigger("swing");
                        swingCouter = swingCoolDown;
                    }

                }


                //walking dead
                if (walkingDead)
                {
                    explodeCounter -= Time.deltaTime;

                    if (explodeCounter <= 0)
                    {
                        Destroy(gameObject);
                        PhotonView.Instantiate(deadSplatters, transform.position, Quaternion.Euler(0, 0, -90f));
                        PhotonView.Instantiate(poisonArea, transform.position, transform.rotation);
                    }
                }
            }

            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isChasing", true);
            }
            else
            {
                anim.SetBool("isChasing", false);
            }
        }
    }

    public void BeDamage( int damage , Transform bullet)
    {
        if(isActiveActack)
        {
            heath -= damage;

        AudioManager.instance.PlaySFX(2);

            PhotonView.Instantiate(hitEnemyEffect, transform.position, bullet.transform.rotation);
            PhotonView.Instantiate(bloodingEffect, transform.position, transform.rotation);
        
            if (heath <= 0)
            {
                AudioManager.instance.PlaySFX(1);

                //transform.rotation = Quaternion.Euler(0, 0, -90f);


                /*int selectedSplatter = Random.Range(0, deadSplatters.Length);

                int rotation = Random.Range(0, 4);

                Instantiate(deadSplatters[selectedSplatter], transform.position, Quaternion.Euler(0, 0, rotation * 90f));*/

                if (zombie)
                {
                    PhotonView.Instantiate(deadWalking, transform.position, transform.rotation);
                }
                else
                    PhotonView.Instantiate(deadSplatters, transform.position, Quaternion.Euler(0, 0, -90f));
            
                Destroy(gameObject);
                if(shouldDropItem)
                {
                    float dropChance = Random.Range(0f, 100f);

                    if(dropChance < itemDropPercent)
                    {
                        int randomItem = Random.Range(0, itemsToDrop.Length);

                        PhotonNetwork.Instantiate(itemsToDrop[randomItem].name, transform.position, transform.rotation);
                    }
                }

            }
        }
    }

    public void Slowing(float speedDeclare, bool active)
    {
        if (active == true)
        {
            activeMoveSpeed = speedDeclare;
            PhotonView.Instantiate(iceEffect, transform.position, transform.rotation);
        }
        else
            activeMoveSpeed = moveSpeed;
    }
    public GameObject FindTarget(List<GameObject> player)
    {
        if (player.Count > 0)
        {
            targetPlayer = player[0];
            for (int i = 0; i < player.Count; i++)
            {
                if (Vector2.Distance(player[i].transform.position, transform.position) < Vector2.Distance(targetPlayer.transform.position, transform.position))
                {
                    targetPlayer = player[i];
                }
            }
        }
        return targetPlayer;
    }

    public void AddPlayer(GameObject player)
    {
        foreach (GameObject inPlayer in listPlayer)
        {
            if (player ==inPlayer)
                isAddList = true;
            else
                isAddList = false;
        }
        if (isAddList == false)
        {
            listPlayer.Add(player);
        }
    }
}
