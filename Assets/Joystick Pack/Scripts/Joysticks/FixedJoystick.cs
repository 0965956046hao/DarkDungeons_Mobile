using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    public PlayerController minePlayer;
    public static FixedJoystick instance;
    public bool isClicked;

    protected override void Start()
    {
        instance = this;
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        minePlayer.mineGun.shouldShoot = true;
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
        base.OnPointerDown(eventData);
        isClicked = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        minePlayer.mineGun.shouldShoot = false;
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        base.OnPointerUp(eventData);
        isClicked = false;
    }
}