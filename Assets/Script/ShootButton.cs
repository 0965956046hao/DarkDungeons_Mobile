using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerController minePlayer;
    public static ShootButton instance;
    public bool click;
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        minePlayer.mineGun.shouldShoot = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        click = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        minePlayer.mineGun.shouldShoot = false;
        transform.localScale = Vector3.one;
        click = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
