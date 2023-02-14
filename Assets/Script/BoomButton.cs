using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BoomButton : MonoBehaviour,IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerController minePlayer;
    public static BoomButton instance;

    public float spellCoolDown;
    private float spellCounter;
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (spellCounter <= 0)
        {
            minePlayer.shouldUseBoonSpell = true;
            spellCounter = spellCoolDown;
        }


        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spellCounter > 0)
        {
            spellCounter -= Time.deltaTime;
        }
    }
}
