using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    public int damage;
    public Transform theTF;

    public bool normalActack;

    public bool poisonSpell;
    public float poisonCoolDown;
    private float poisonCounter;

    public bool poisonIce;
    public float poisonIceCoolDown;
    private float poisonIceCounter;
    public float speedDeclare;
    private float iceLeft;
    public float iceCounter;

    public bool boom;
    public float timeToBoom;
    private float timeBoomCounter;
    public float timeWait;
    private float waitCouter;
    public GameObject boomEffect;
    public Transform explodePoint;

    // Start is called before the first frame update
    void Start()
    {
        //poisonIceCounter = poisonIceCoolDown;
        iceLeft = iceCounter;
        timeBoomCounter = timeToBoom;
        waitCouter = timeWait;
    }

    // Update is called once per frame
    void Update()
    {
        if(boom)
        {
            timeBoomCounter -= Time.deltaTime;

            if(timeBoomCounter <=0)
            {
                Destroy(gameObject);
                Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                AudioManager.instance.PlaySFX(22);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (normalActack)
            {
                other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }

            if (poisonIce)
            {
                poisonIceCounter -= Time.deltaTime;

                if (poisonIceCounter <= 0)
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, true);
                    if (iceLeft > 0)
                    {
                        iceLeft -= Time.deltaTime;
                    }
                    else
                    {
                        poisonIceCounter = poisonIceCoolDown;
                        iceLeft = iceCounter;
                    }
                }
                else
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, false);
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
            }
        }
        if (other.tag == "Boss")
        {
            if (normalActack)
            {
                other.gameObject.GetComponent<BossController>().TakeDamage(damage);
            }

            if (poisonIce)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            if (normalActack)
            {
                other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }

            if (poisonIce)
            {
                poisonIceCounter -= Time.deltaTime;

                if (poisonIceCounter <= 0)
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, true);
                    if (iceLeft > 0)
                    {
                        iceLeft -= Time.deltaTime;
                    }
                    else
                    {
                        poisonIceCounter = poisonIceCoolDown;
                        iceLeft = iceCounter;
                    }
                }
                else
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, false);
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
                else
                    other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }
        }
        if (other.tag == "Boss")
        {
            if (normalActack)
            {
                other.gameObject.GetComponent<BossController>().TakeDamage(damage);
            }

            if (poisonIce)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" )
        {


            if (normalActack)
            {
                other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }

            if (poisonIce)
            {
                poisonIceCounter -= Time.deltaTime;

                if (poisonIceCounter <= 0)
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, true);
                    if (iceLeft > 0)
                    {
                        iceLeft -= Time.deltaTime;
                    }
                    else
                    {
                        poisonIceCounter = poisonIceCoolDown;
                        iceLeft = iceCounter;
                    }
                }
                else
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, false);
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
                else
                    other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }
        }
        if (other.gameObject.tag == "Boss")
        {
            if (normalActack)
            {
                other.gameObject.GetComponent<BossController>().TakeDamage(damage);
            }

            if (poisonIce)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {


            if (normalActack)
            {
                other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }

            if (poisonIce)
            {
                poisonIceCounter -= Time.deltaTime;

                if (poisonIceCounter <= 0)
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, true);
                    if (iceLeft > 0)
                    {
                        iceLeft -= Time.deltaTime;
                    }
                    else
                    {
                        poisonIceCounter = poisonIceCoolDown;
                        iceLeft = iceCounter;
                    }
                }
                else
                {
                    other.gameObject.GetComponent<EnemiesController>().Slowing(speedDeclare, false);
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
                else
                    other.gameObject.GetComponent<EnemiesController>().BeDamage(damage, this.theTF);
            }
        }
        if (other.gameObject.tag == "Boss")
        {
            if (normalActack)
            {
                other.gameObject.GetComponent<BossController>().TakeDamage(damage);
            }

            if (poisonIce)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }

            if (poisonSpell)
            {
                poisonCounter -= Time.deltaTime;

                if (poisonCounter <= 0)
                {
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        poisonCounter = poisonCoolDown;
                    }
                }
            }
            else
            {
                if (boom)
                {
                    waitCouter -= Time.deltaTime;

                    if (waitCouter <= 0)
                    {
                        other.gameObject.GetComponent<BossController>().TakeDamage(damage);
                        Destroy(gameObject);
                        Instantiate(boomEffect, explodePoint.position, explodePoint.rotation);
                        AudioManager.instance.PlaySFX(22);
                    }
                }
            }
        }

    }
}
