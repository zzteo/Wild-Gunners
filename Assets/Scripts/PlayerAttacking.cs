using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField]
    LineRenderer LR;

    [SerializeField]
    Joystick AttackJoystick;

    [SerializeField]
    Transform AttackLookAt;

    [SerializeField]
    public float TrailDistance = 1;

    [SerializeField]
    Transform Player;

    RaycastHit hitInfo;

    bool Shoot;

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform gunEndPoint;
    void FixedUpdate()
    {
        if(Mathf.Abs(AttackJoystick.Horizontal) > 0.3f || Mathf.Abs(AttackJoystick.Vertical) > 0.3f)
        {
            if(LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
            }

            transform.position = new Vector3(Player.position.x, 5f, Player.position.z);

            AttackLookAt.position = new Vector3(AttackJoystick.Horizontal + Player.transform.position.x, 5, AttackJoystick.Vertical + Player.transform.position.z);

            transform.LookAt(new Vector3(AttackLookAt.position.x, 0, AttackLookAt.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            LR.SetPosition(0, transform.position /*new Vector3 (transform.position.x, 5f, transform.position.z)*/);

            if(Physics.Raycast(Player.transform.position, transform.forward, out hitInfo, TrailDistance))
            {
                LR.SetPosition(1, hitInfo.point);
            }
            else
            {
                LR.SetPosition(1, transform.position + transform.forward * TrailDistance);

                LR.SetPosition(1, new Vector3(LR.GetPosition(1).x, 5, LR.GetPosition(1).z));
            }

            if (Shoot == false)
            {
                Shoot = true;
            }
            else if (Shoot && Input.GetMouseButtonUp(0))
            {
                Debug.Log("SHOOT");//It doesn't show for some reason 
                Instantiate(bullet, transform.position, transform.rotation);
                Shoot = false;
            }
            else if ( Shoot && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Ended)
                {
                   /* Instantiate(bullet, gunEndPoint.transform.position, transform.rotation);*/
                    Debug.Log("shoot");
                    Shoot = false;
                }
            }
            else if(Mathf.Abs(AttackJoystick.Horizontal) <0.3f || Mathf.Abs(AttackJoystick.Vertical) < 0.3f && LR.gameObject.activeInHierarchy == true)
            {
                LR.gameObject.SetActive(false);
                Shoot = false;
            }
        }
        else
        {
            LR.gameObject.SetActive(false);
            Shoot = false;
        }
       /* Debug.Log(Shoot);*/
    }
}
