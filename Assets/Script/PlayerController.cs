using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;
    private Vector2 shootInput;
    public Rigidbody2D theRB;

    public Transform gunArm;

    public Animator anim;

    public GameObject targetEnemy;

    public PhotonView view;

    /*public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShot;
    private float shotCounter;*/

    public GameObject poisonSpell;
    public GameObject iceSpell;
    public GameObject boom;
    public FloatingJoystick joyStick;
    public FixedJoystick shootJoyStick;

    public List<GameObject> enemies = new List<GameObject>();

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = 0.5f, dashCooldown = 1f, dashInvincbility = .5f ;
    private float dashCoolCouter;
    [HideInInspector]
    public float dashCounter;

    public float spellCoolDown;
    private float spellCounter;

    private Camera theCam;
    public GameObject mainCam;

    public SpriteRenderer bodySR;

    public bool canMove = true;

    [HideInInspector]
    public bool spiteResistance = false;
    public bool poisonResistance = false;
    public bool iceResistance = false;

    [HideInInspector]
    public bool poison = false;
    [HideInInspector]
    public bool ice = false;

    //get effect poison
    public float timePoisonEffect = 5;
    private float timePoisonEffectCounter;
    public float poisonEffectCoolDown = 1;
    private float poisonEffectCounter;

    //get effect ice
    public float timeIceEffect = 4;
    private float timeIceEffectCounter;
    public float speedEffect;
    private float speedCounter;

    
    public float angle;

    public List<Guns> avalibleGuns = new List<Guns>();
    [HideInInspector]
    public int currentGun;
    public Guns mineGun;
    public bool shouldash;
    public bool shouldUseIceSpell;
    public bool shouldUsePosionSpell;
    public bool shouldUseBoonSpell;

    public bool switchGun;

    public bool ismine;
    public bool isMasterPlayer;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public int health;

    public float invincCount;

    public bool testHeathActive;
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        activeMoveSpeed = moveSpeed;
        timePoisonEffectCounter = timePoisonEffect;
        timeIceEffectCounter = timeIceEffect;
        speedCounter = speedEffect;
        joyStick = FloatingJoystick.instance;
        shootJoyStick = FixedJoystick.instance;
        view = GetComponent<PhotonView>();
        mineGun = avalibleGuns[0];
        UiController.instance.currentGun.sprite = avalibleGuns[currentGun].gunUi;
        UiController.instance.gunText.text = avalibleGuns[currentGun].weaponName;
        if (view.IsMine)
        {
            FixedJoystick.instance.minePlayer = this;
            DashButton.instance.minePlayer = this;
            IceSpellButton.instance.minePlayer = this;
            PosionButton.instance.minePlayer = this;
            BoomButton.instance.minePlayer = this;
        }
        else
        {
            ismine = false;
            mainCam.SetActive(false);
        }
        angle = 0;
        SpawnMultiplayer.instance.listPlayer.Add(this.gameObject);
        if(PhotonNetwork.IsMasterClient)
        {
            if (view.IsMine)
                isMasterPlayer = true;
            else
                isMasterPlayer = false;
        }    
        else
        {
            if (view.IsMine)
                isMasterPlayer = false;
            else
                isMasterPlayer = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        if (view.IsMine)
        {
            ismine = true;
            if (canMove && !LevelManager.instance.isPaused)
            {
                moveInput.x = joyStick.Horizontal;
                moveInput.y = joyStick.Vertical;
                shootInput.x = shootJoyStick.Horizontal;
                shootInput.y = shootJoyStick.Vertical;

                //PC 
                /*moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");*/

                moveInput.Normalize();

                //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

                theRB.velocity = moveInput * activeMoveSpeed;

                Vector3 mousePos = Input.mousePosition;
                Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

                //PC
                /*if (mousePos.x < screenPoint.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    gunArm.localScale = new Vector3(-1f, -1f, 1f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    gunArm.localScale = Vector3.one;
                }*/

                // rotate gunarm
                /*Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
                angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

                gunArm.rotation = Quaternion.Euler(0, 0, angle);*/

                //mobile
                if (enemies.Count > 0)
                {
                    targetEnemy = FindTarget(enemies);

                    Vector2 offset = new Vector2(targetEnemy.transform.position.x - transform.position.x, targetEnemy.transform.position.y - transform.position.y);
                    angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

                    gunArm.rotation = Quaternion.Euler(0, 0, angle);
                    if (targetEnemy.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        gunArm.localScale = new Vector3(-1f, -1f, 1f);
                    }
                    else
                    {
                        transform.localScale = Vector3.one;
                        gunArm.localScale = Vector3.one;
                    }
                }
                else
                {
                    if (shootJoyStick.isClicked)
                    {
                        Vector2 offset = new Vector2(shootInput.x, shootInput.y);
                        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

                        gunArm.rotation = Quaternion.Euler(0, 0, angle);
                        if (shootInput.x < 0)
                        {
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                            gunArm.localScale = new Vector3(-1f, -1f, 1f);
                        }
                        else
                        {
                            transform.localScale = Vector3.one;
                            gunArm.localScale = Vector3.one;
                        }
                    }
                    else
                    {
                        if (moveInput.x >= 0)
                        {
                            gunArm.rotation = Quaternion.Euler(0, 0, 0);
                            transform.localScale = Vector3.one;
                            gunArm.localScale = Vector3.one;
                        }
                        else if (moveInput.x < 0)
                        {
                            gunArm.rotation = Quaternion.Euler(0, 0, 180);
                            transform.localScale = new Vector3(-1f, 1f, 1f);
                            gunArm.localScale = new Vector3(-1f, -1f, 1f);
                        }
                    }
                }


                /*if (Input.GetMouseButtonDown(0))
                {
                    AudioManager.instance.PlaySFX(12);
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShot;
                }

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

                if (Input.GetKeyDown(KeyCode.Space) || shouldash)
                {
                    if (dashCoolCouter <= 0 && dashCounter <= 0)
                    {
                        activeMoveSpeed = dashSpeed;
                        dashCounter = dashLength;
                        anim.SetTrigger("dash");
                        AudioManager.instance.PlaySFX(8);
                        PlayerHeathController.instance.MakeInvicible(dashInvincbility);
                        Debug.Log("dashed");
                        shouldash = false;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) || shouldUsePosionSpell)
                {
                    if (spellCounter <= 0)
                    {
                        AudioManager.instance.PlaySFX(21);
                        //Instantiate(poisonSpell, theCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), transform.rotation);
                        //Moblie
                        targetEnemy = FindTarget(enemies);
                        if (targetEnemy)
                            PhotonNetwork.Instantiate(poisonSpell.name, targetEnemy.transform.position + new Vector3(0f, 0f, 10f), transform.rotation);
                        else
                            PhotonNetwork.Instantiate(poisonSpell.name, transform.position + new Vector3(5f, 0f, 10f), transform.rotation);
                        shouldUsePosionSpell = false;
                        //
                        spellCounter = spellCoolDown;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) || shouldUseIceSpell)
                {
                    if (spellCounter <= 0)
                    {
                       
                        AudioManager.instance.PlaySFX(21);
                        //Instantiate(iceSpell, theCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), transform.rotation);//PC
                        //Moblie
                        targetEnemy = FindTarget(enemies);
                        if (targetEnemy)
                            PhotonNetwork.Instantiate(iceSpell.name, targetEnemy.transform.position + new Vector3(0f, 0f, 10f), transform.rotation);
                        else
                            PhotonNetwork.Instantiate(iceSpell.name, transform.position + new Vector3(5f, 0f, 10f), transform.rotation);
                        shouldUseIceSpell = false;
                        //
                        spellCounter = spellCoolDown;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) || shouldUseBoonSpell)
                {
                    if (spellCounter <= 0)
                    {
                        AudioManager.instance.PlaySFX(4);
                        //Instantiate(boom, theCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f), transform.rotation);
                        //Moblie
                        targetEnemy = FindTarget(enemies);
                        if (targetEnemy)
                            PhotonNetwork.Instantiate(boom.name, targetEnemy.transform.position + new Vector3(0f, 0f, 10f), transform.rotation);
                        else
                            PhotonNetwork.Instantiate(boom.name, transform.position + new Vector3(5f, 0f, 10f), transform.rotation);
                        shouldUseBoonSpell = false;
                        //
                        spellCounter = spellCoolDown;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Tab) || switchGun)
                {
                    if (avalibleGuns.Count > 0)
                    {
                        currentGun++;
                        if (currentGun >= avalibleGuns.Count)
                        {
                            currentGun = 0;
                        }
                        SwitchGun();
                        switchGun = false;
                    }
                    else
                    {
                        Debug.LogError("Has no gun!");
                    }
                }

                if (spellCounter > 0)
                {
                    spellCounter -= Time.deltaTime;
                }

                if (dashCounter > 0)
                {
                    dashCounter -= Time.deltaTime;
                    if (dashCounter <= 0)
                    {
                        activeMoveSpeed = moveSpeed;
                        dashCoolCouter = dashCooldown;
                    }
                }

                if (dashCooldown > 0)
                {
                    dashCoolCouter -= Time.deltaTime;
                }


                if (moveInput != Vector2.zero)
                {
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
            }
            else
            {
                theRB.velocity = Vector2.zero;
                anim.SetBool("isMoving", false);
            }

            if (poison)
            {
                if (timePoisonEffectCounter > 0)
                {
                    timePoisonEffectCounter -= Time.deltaTime;
                    poisonEffectCounter -= Time.deltaTime;
                    if (poisonEffectCounter <= 0)
                    {
                        PlayerHeathController.instance.DamagePlayer();
                        poisonEffectCounter = poisonEffectCoolDown;
                    }
                }
                else
                {
                    poison = false;
                    timePoisonEffectCounter = timePoisonEffect;
                }
            }

            if (ice)
            {
                if (timeIceEffectCounter > 0)
                {
                    timeIceEffectCounter -= Time.deltaTime;
                    activeMoveSpeed = speedCounter;
                    poisonEffectCounter -= Time.deltaTime;
                    if (poisonEffectCounter <= 0)
                    {
                        PlayerHeathController.instance.DamagePlayer();
                        speedCounter += 0.5f;
                        poisonEffectCounter = poisonEffectCoolDown;
                    }
                }
                else
                {
                    ice = false;
                    timeIceEffectCounter = timeIceEffect;
                    activeMoveSpeed = moveSpeed;
                    speedCounter = speedEffect;
                }
            }
        }
    }

    public void SwitchGun()
    {
        foreach(Guns theGun in avalibleGuns)
        {
            theGun.gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySFX(6);
        avalibleGuns[currentGun].gameObject.SetActive(true);
        mineGun = avalibleGuns[currentGun];
        UiController.instance.currentGun.sprite = avalibleGuns[currentGun].gunUi;
        UiController.instance.gunText.text = avalibleGuns[currentGun].weaponName;
    }

    public GameObject FindTarget(List<GameObject> enemies)
    {
        if (enemies.Count > 0)
        {
            targetEnemy = enemies[0];
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
                if (Vector2.Distance(enemies[i].transform.position, transform.position) < Vector2.Distance(targetEnemy.transform.position, transform.position))
                {
                    targetEnemy = enemies[i];
                }
            }
        }
        return targetEnemy;
    }

}
