using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    public List<BossController> listBosss;
    public GameObject levelExit;

    public int currentHealth;
    public int maxBossHealth;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(BossController boss in listBosss)
        {
            maxBossHealth += boss.currentHealth;
        }
        UiController.instance.bossHealthBar.maxValue = maxBossHealth;
        UiController.instance.bossHealthBar.value = maxBossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = 0;
        foreach (BossController boss in listBosss)
        {
            currentHealth += boss.currentHealth;
        }
        UiController.instance.bossHealthBar.value = currentHealth;


        if (listBosss.Count == 0)
        {
            levelExit.SetActive(true);
            UiController.instance.bossHealthBar.gameObject.SetActive(false);
        }

    }
}
