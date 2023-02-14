using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsPickup : MonoBehaviour
{
    public float waitToColected = .5f;
    public Guns theGun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToColected > 0)
        {
            waitToColected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToColected <= 0)
        {
            bool hasGun = false;
            foreach(Guns gunToCheck in other.gameObject.GetComponent<PlayerController>().avalibleGuns)
            {
                if(theGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if(!hasGun)
            {
                Guns gunClone = Instantiate(theGun);
                gunClone.transform.parent = other.gameObject.GetComponent<PlayerController>().gunArm;
                gunClone.transform.position = other.gameObject.GetComponent<PlayerController>().gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                other.gameObject.GetComponent<PlayerController>().avalibleGuns.Add(gunClone);
                other.gameObject.GetComponent<PlayerController>().currentGun = other.gameObject.GetComponent<PlayerController>().avalibleGuns.Count -1;
                other.gameObject.GetComponent<PlayerController>().SwitchGun();
            }
            AudioManager.instance.PlaySFX(6);
            Destroy(gameObject);
        }
    }
}
