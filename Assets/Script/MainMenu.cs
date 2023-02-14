using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;
    public float timeToSart;
    private float timeCouter;

    private bool start;
    private bool startMultiplayer;

    public Animator anim;
    public GameObject[] others;

    public GameObject deletePanel;
    public CharacterSelector[] characterDelete;

    // Start is called before the first frame update
    void Start()
    {
        timeCouter = timeToSart;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(timeCouter > 0)
            {
                timeCouter -= Time.deltaTime;
                if(timeCouter <= 0)
                {
                    SceneManager.LoadScene(levelToLoad);
                }
            }
        }
        else if(startMultiplayer)
        {
            if (timeCouter > 0)
            {
                timeCouter -= Time.deltaTime;
                if (timeCouter <= 0)
                {
                    SceneManager.LoadScene("Loading Multiplayer");
                }
            }
        }
    }

    public void StartGame()
    {
        start = true;
        anim.SetTrigger("Start");
        foreach(GameObject other in others)
        {
            other.SetActive(false);
        }
    }

    public void StartMultiplayer()
    {
        startMultiplayer = true;
        anim.SetTrigger("Start");
        foreach (GameObject other in others)
        {
            other.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }

    public void ComfirmDelete()
    {
        deletePanel.SetActive(false);

        foreach(CharacterSelector theChar in characterDelete)
        {
            PlayerPrefs.SetInt(theChar.playerToSpawn.name, 0);
        }
    }

}
