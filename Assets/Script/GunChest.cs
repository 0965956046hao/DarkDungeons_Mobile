using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunChest : MonoBehaviour
{

    public GunsPickup[] potentialGuns;

    public SpriteRenderer theSR;
    public Sprite chestOpen;

    private bool canOpen, isOpen;    

    public GameObject notification;

    public Transform spwanPoint;

    public float scaleSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && !isOpen)
        {
            if(Input.GetKeyDown(KeyCode.E) || FixedJoystick.instance.isClicked)
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);

                PhotonNetwork.Instantiate(potentialGuns[gunSelect].name, spwanPoint.transform.position, spwanPoint.rotation);

                theSR.sprite = chestOpen;

                isOpen = true;

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }

        if(isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            notification.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);
            canOpen = false;
        }
    }
}
