using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnlock : MonoBehaviour
{

    private bool canUnclock;


    public GameObject message;

    public CharacterSelector[] characterSelects;
    private CharacterSelector playerToUnclock;

    public SpriteRenderer cagedSR;

    // Start is called before the first frame update
    void Start()
    {
        playerToUnclock = characterSelects[Random.Range(0, characterSelects.Length)];

        cagedSR.sprite = playerToUnclock.playerToSpawn.bodySR.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUnclock)
        {
            if (Input.GetKeyDown(KeyCode.E) || FixedJoystick.instance.isClicked)
            {
                PlayerPrefs.SetInt(playerToUnclock.playerToSpawn.name, 1);

                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnclock = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnclock = false;
            message.SetActive(false);
        }
    }
}
