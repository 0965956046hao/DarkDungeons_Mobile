using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerController minePlayer;
    public static DashButton instance;
    public float dashCoolCouter;
    [HideInInspector]
    public float dashCounter;
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(dashCounter <= 0)
        {
            minePlayer.shouldash = true;
            dashCounter = dashCoolCouter;
        }
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
        }
    }
}
