using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public float timeMove;

    // Start is called before the first frame update
    void Start()
    {
        timeMove = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(transform.right.x * Time.deltaTime * speed, transform.right.y * Time.deltaTime * speed, 0f);
        theRB.velocity = transform.right * speed;
        if (timeMove > 0)
        {
            timeMove -= Time.deltaTime;
            if (timeMove <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            Destroy(gameObject);


            if (other.tag == "Player" && other.GetComponent<PlayerController>().view.IsMine)
            {
                PlayerHeathController.instance.DamagePlayer();
            }
            else
            {
                AudioManager.instance.PlaySFX(4);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
