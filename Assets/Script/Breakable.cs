using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    public bool poison, ice, boom;
    public GameObject poisonSpell;
    public GameObject iceSpell;
    public GameObject boomSpell;

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
        if(other.tag == "Bullet")
        {
            Broken();
            DropItems();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerController.instance.dashCounter > 0)
        {
            Broken();
            DropItems();
        }
        if (other.gameObject.tag == "Enemy" && EnemiesController.instance.rushCounter > 0)
        {
            Broken();
            DropItems();
        }
    }

    public void Broken()
    {
        Destroy(gameObject);

        AudioManager.instance.PlaySFX(0);

        for(int i = 0; i < maxPieces; i++)
        {
            int randomPiece = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }
        if(poison)
        {
            Instantiate(poisonSpell, transform.position, transform.rotation);
        }
        if(ice)
        {
            Instantiate(iceSpell, transform.position, transform.rotation);
        }
        if(boom)
        {
            Instantiate(boomSpell, transform.position, transform.rotation);
        }
    }

    public void DropItems()
    {
        if(shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);

            if(dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
}
