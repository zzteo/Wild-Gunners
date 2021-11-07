using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    PlayerAttackingV2 PA;
    protected override void Start()
    {
        base.Start();
        PA = GameObject.Find("AttackTrail").GetComponent<PlayerAttackingV2>();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        PA.StartCoroutine("SuperAttackThrowing");
      
    }
}