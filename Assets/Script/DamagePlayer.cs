using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool spite, poison, ice, enemyActack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerController>().view.IsMine)
        {
            if(enemyActack)
            {
                PlayerHeathController.instance.DamagePlayer();
            }
            if(spite)
            {
                if (other.GetComponent<PlayerController>().spiteResistance == false)
                {
                    PlayerHeathController.instance.DamagePlayer();
                }
            }
            if(poison)
            {
                if (other.GetComponent<PlayerController>().poisonResistance == false)
                {
                    other.GetComponent<PlayerController>().poison = true;
                }
            }
            if (ice)
            {
                if (other.GetComponent<PlayerController>().iceResistance == false)
                {
                    other.GetComponent<PlayerController>().ice = true;
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerController>().view.IsMine)
        {
            if (enemyActack)
            {
                PlayerHeathController.instance.DamagePlayer();
            }
            if (spite)
            {
                if (other.GetComponent<PlayerController>().spiteResistance == false)
                {
                    PlayerHeathController.instance.DamagePlayer();
                }
            }
            if (poison)
            {
                if (other.GetComponent<PlayerController>().poisonResistance == false)
                {
                    other.GetComponent<PlayerController>().poison = true;
                }
            }
            if (ice)
            {
                if (other.GetComponent<PlayerController>().iceResistance == false)
                {
                    other.GetComponent<PlayerController>().ice = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().view.IsMine)
        {
            if (enemyActack)
            {
                PlayerHeathController.instance.DamagePlayer();
            }
            if (spite)
            {
                if (other.gameObject.GetComponent<PlayerController>().spiteResistance == false)
                {
                    PlayerHeathController.instance.DamagePlayer();
                }
            }
            if (poison)
            {
                if (other.gameObject.GetComponent<PlayerController>().poisonResistance == false)
                {
                    other.gameObject.GetComponent<PlayerController>().poison = true;
                }
            }
            if (ice)
            {
                if (other.gameObject.GetComponent<PlayerController>().iceResistance == false)
                {
                    other.gameObject.GetComponent<PlayerController>().ice = true;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().view.IsMine)
        {
            if (enemyActack)
            {
                PlayerHeathController.instance.DamagePlayer();
            }
            if (spite)
            {
                if (other.gameObject.GetComponent<PlayerController>().spiteResistance == false)
                {
                    PlayerHeathController.instance.DamagePlayer();
                }
            }
            if (poison)
            {
                if (other.gameObject.GetComponent<PlayerController>().poisonResistance == false)
                {
                    other.gameObject.GetComponent<PlayerController>().poison = true;
                }
            }
            if (ice)
            {
                if (other.gameObject.GetComponent<PlayerController>().iceResistance == false)
                {
                    other.gameObject.GetComponent<PlayerController>().ice = true;
                }
            }
        }
    }
}
