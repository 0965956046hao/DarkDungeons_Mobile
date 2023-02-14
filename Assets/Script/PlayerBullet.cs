using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public GameObject impactEffect;
    

    public int bulletDamage;

    public bool shutgonBullet;
    public float timeMove;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
        if (shutgonBullet)
        {
            if (timeMove > 0)
            {
                timeMove -= Time.deltaTime;
                if (timeMove <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
            Destroy(gameObject);

            if(other.tag == "Boss")
            {
                other.GetComponent<BossController>().TakeDamage(bulletDamage);
                Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
                Instantiate(BossController.instance.bloodingEffect, transform.position, transform.rotation); 
            }

            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemiesController>().BeDamage(bulletDamage, this.transform);
            }
            else
            {
                AudioManager.instance.PlaySFX(4);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
    }

}
