using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    //public Camera permannent, move;

    public Camera mainCamera, bigMapCammera;

    private bool bigMapActive;


    public bool activeBigMap;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.M) || activeBigMap)
        {
            if(!bigMapActive)
            {
                ActivateBigMap();
                activeBigMap = false;
            } else
            {
                DeactivateBigMap();
                activeBigMap = false;
            }
        }
    }

    public void ActivateBigMap()
    {
        if(!LevelManager.instance.isPaused)
        {
            bigMapActive = true;

            bigMapCammera.enabled = true;
            mainCamera.enabled = false;
            Time.timeScale = 0f;
            UiController.instance.mapDisplay.SetActive(false);
            UiController.instance.bigMapText.SetActive(true);
        }
       
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = false;

            bigMapCammera.enabled = false;
            mainCamera.enabled = true;
            Time.timeScale = 1f;
            UiController.instance.mapDisplay.SetActive(true);
            UiController.instance.bigMapText.SetActive(false);
        }
    }
}

