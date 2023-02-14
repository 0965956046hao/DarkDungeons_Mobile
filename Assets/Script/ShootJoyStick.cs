using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootJoyStick : FixedJoystick
{
    public PlayerController minePlayer;
    public static ShootJoyStick instance;
    public bool isClicked;

    protected override void Start()
    {
        instance = this;
        base.Start();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        minePlayer.mineGun.shouldShoot = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        base.OnPointerDown(eventData);
        isClicked = true;
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        minePlayer.mineGun.shouldShoot = false;
        transform.localScale = Vector3.one;
        base.OnPointerUp(eventData);
    }
}
