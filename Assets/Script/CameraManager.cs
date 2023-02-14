using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject permannent, move;

    public CameraController withRoom, withPlayer;

    [HideInInspector]
    public bool moveWithPlayerActive;
    
    public bool moveWithPlayer;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        moveWithPlayerActive = moveWithPlayer;
        if(moveWithPlayerActive)
        {
            move.SetActive (true);
            permannent.SetActive(false);
        }
        else
        {
            move.SetActive(false);
            permannent.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            withPlayer.DeactivateBigMap();
            withRoom.DeactivateBigMap();
            if (moveWithPlayer)
            {
                moveWithPlayer = false;
                moveWithPlayerActive = moveWithPlayer;
            }
            else
            {
                moveWithPlayer = true;
                moveWithPlayerActive = moveWithPlayer;
            }  
        }
        
        if (moveWithPlayerActive)
        {
            move.SetActive(true);
            permannent.SetActive(false);
        }
        else
        {
            move.SetActive(false);
            permannent.SetActive(true);
        }
    }

}
