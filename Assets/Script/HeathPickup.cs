using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPickup : MonoBehaviour
{

    public int minHeath;
    public int maxHeath;
    private int healAmount;

    public float waitToColected = .5f;

    // Start is called before the first frame update
    void Start()
    {
        healAmount = Random.Range(minHeath, maxHeath);
    }

    // Update is called once per frame
    void Update()
    {
       if(waitToColected > 0)
        {
            waitToColected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        other.GetComponent<PlayerController>().health += healAmount;
        if (other.tag == "Player" && waitToColected <= 0 && other.GetComponent<PlayerController>().view.IsMine)
        { 
            PlayerHeathController.instance.HealPlayer(healAmount);
            AudioManager.instance.PlaySFX(7); 
            Destroy(gameObject);
        }
    }
}
