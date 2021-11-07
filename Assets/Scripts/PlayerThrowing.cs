using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
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

    [SerializeField]
    bool Shoot;

    [SerializeField]
    GameObject Dynamite;
    [SerializeField]
    ParticleSystem ShootingParticles;

    [SerializeField]
    Transform PlayerSpine;
    [SerializeField]
    Transform PlayerSpineChild;

    [SerializeField]
    Animator anim;


    //throw
    [SerializeField]
    float LinePower_Y;
    [SerializeField]
    public Vector3[] BulletPoints; //tells the bullet the path that it should travel 
    //throw



    private float attackDelayTimer = 0.1f; // l-am creat pentru ca atunci cand dai drumul la AttackJoystick nu puteam instanta gloante pentru ca se inchidea imediat

    //shooting charges
    [SerializeField]
    GameObject charge1;
    [SerializeField]
    GameObject charge2;
    [SerializeField]
    GameObject charge3;

    private float shootingChargingTime = 2f;
    private float shootingCharges = 3;

    void Start()
    {
        LR.positionCount = 10;
        BulletPoints = new Vector3[9];
    }

    void Update()
    {

        if (Mathf.Abs(AttackJoystick.Horizontal) > 0.1f || Mathf.Abs(AttackJoystick.Vertical) > 0.1f)
        {
            if (LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
                Shoot = true;
            }

            AttackLookAt.position = new Vector3(AttackJoystick.Horizontal + Player.transform.position.x, 3.5f, AttackJoystick.Vertical + Player.transform.position.z);
            transform.position = new Vector3(Player.position.x, 5f, Player.position.z);
            transform.LookAt(new Vector3(AttackLookAt.position.x, 0, AttackLookAt.position.z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            LR.SetPosition(0, new Vector3(transform.position.x, 4f, transform.position.z));

            for (int i=1; i<10; i++)
            {
                LR.SetPosition(i, new Vector3(LR.GetPosition(i - 1).x + AttackJoystick.Horizontal, i==1? 5.5f : Mathf.Cos(LinePower_Y * (i* 0.1f)) * (i*4f), LR.GetPosition(i - 1).z + AttackJoystick.Vertical));
                BulletPoints[i - 1] = LR.GetPosition(i);
            }
         

        }
        else
        {
            LR.gameObject.SetActive(false);

            if (attackDelayTimer > 0)
            {
                attackDelayTimer -= Time.deltaTime;
            }
            else
            {
                attackDelayTimer = 0.1f;
                Shoot = false;
            }
        }



        if (shootingCharges < 3)
        {
            shootingChargingTime -=  Time.deltaTime;

            if(shootingChargingTime < 0)
            {
                shootingCharges += 1;
                shootingChargingTime = 2f;
            }

            if (shootingCharges < 0) shootingCharges = 0;
        }

        if (shootingCharges == 3)
        {
            charge1.SetActive(true);
            charge2.SetActive(true);
            charge3.SetActive(true);
        }
        else if(shootingCharges == 2)
        {
            charge1.SetActive(true);
            charge2.SetActive(true);
            charge3.SetActive(false);
        }
        else if(shootingCharges == 1)
        {
            charge1.SetActive(true);
            charge2.SetActive(false);
        }
        else
        {
            charge1.SetActive(false);
            charge2.SetActive(false);
            charge3.SetActive(false);
        }

    }
   public  IEnumerator Throwing()
    {
        if (Shoot)
        {        
            PlayerSpine.LookAt(AttackLookAt);

            anim.SetTrigger("Shoot");

            PlayerSpine.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Instantiate(Dynamite, AttackLookAt.transform.position, transform.rotation);
            Instantiate(ShootingParticles, AttackLookAt.transform.position, transform.rotation);
            Shoot = false;         
            yield return new WaitForSeconds(1f);
            PlayerSpine.localRotation = PlayerSpineChild.localRotation;
        }
    }          
  
}
