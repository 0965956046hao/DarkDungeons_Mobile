using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMultiplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> listPlayer = new List<GameObject>();
    public static SpawnMultiplayer instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in listPlayer)
            if (player == null)
                listPlayer.Remove(player);
        if(ListDeath.instance)
        {
            if(listPlayer.Count > 1)
            {
                foreach (GameObject player in listPlayer)
                    if (player.GetComponent<PlayerController>().view.IsMine == false)
                    {
                        Destroy(player);
                    }    
                       
            }    
            
        }
        if (AllDeathFlags.instance)
        {
            AudioManager.instance.PlayGameOver();
            UiController.instance.deathScreen.SetActive(true);
        }
    }
}
