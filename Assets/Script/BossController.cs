using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public static BossController instance;
    public BossAction[] actions;

    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDiretion;
    public Rigidbody2D theRB;

    public bool cirleShot;
    public Transform theTF;
    private float angle;

    public int currentHealth;

    public GameObject deathEffect, hitEffect, bloodingEffect;

    public BossSequence[] sequences;
    public int currentSequece;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actions = sequences[currentSequece].actions;
        actionCounter = actions[currentAction].actionLenght;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;

            //move
            moveDiretion = Vector2.zero;
            if(actions[currentAction].shouldMove)
            {
                if(actions[currentAction].shouldChasePlayer)
                {
                    moveDiretion = PlayerController.instance.transform.position - transform.position;
                    moveDiretion.Normalize();
                }

                if(actions[currentAction].moveToPoint)
                {
                    moveDiretion = actions[currentAction].pointToMoveTo.position - transform.position;
                    moveDiretion.Normalize();
                }
            }

            theRB.velocity = moveDiretion * actions[currentAction].moveSpeed;

            //shoting
            if(actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    if(cirleShot)
                    {
                        angle += 10;
                        theTF.eulerAngles = new Vector3(0 ,0 , theTF.rotation.z + angle);
                    }
                    shotCounter = actions[currentAction].timeBetweenShots;

                    foreach(Transform t in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        }
        else
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLenght;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        AudioManager.instance.PlaySFX(2);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            BossManager.instance.listBosss.Remove(this);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        else
        {
            if (currentHealth <= sequences[currentSequece].endSequeceHealth && currentSequece < sequences.Length - 1)
            {
                currentSequece++;
                actions = sequences[currentSequece].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLenght;
            }
        }

    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]

    public float actionLenght;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;
         
}

[System.Serializable]
public class BossSequence
{
    [Header("Sequece")]
    public BossAction[] actions;

    public int endSequeceHealth;
}
