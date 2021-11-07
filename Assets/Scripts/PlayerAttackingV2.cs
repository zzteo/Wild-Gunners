using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerAttackingV2 : MonoBehaviour
{
    [SerializeField]
    LineRenderer LR;

    [SerializeField]
    Joystick AttackJoystick;


    ///throw
    [SerializeField]
    Joystick SuperAttackJoystick;
    [SerializeField]
    float LinePower_Y;
    [SerializeField]
    public Vector3[] BulletPoints;
    [SerializeField]
    LineRenderer ThrowLR;

    ///  

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
    bool SuperAttack;

    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    ParticleSystem ShootingParticles;

    [SerializeField]
    Transform PlayerSpine;
    [SerializeField]
    Transform PlayerSpineChild;

    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject Dynamite;
    [SerializeField]
    AudioSource DynamiteExplosion;




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
        ThrowLR.positionCount = 10;
        BulletPoints = new Vector3[9];

        
    }

    void Update()
    {

        if (Mathf.Abs(AttackJoystick.Horizontal) > 0.3f || Mathf.Abs(AttackJoystick.Vertical) > 0.3f)
        {
            if (LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
                Shoot = true;
            }

            AttackLookAt.position = new Vector3(AttackJoystick.Horizontal + Player.transform.position.x, 3.5f, AttackJoystick.Vertical + Player.transform.position.z);
            transform.position = new Vector3(Player.position.x, 5f, Player.position.z);
            transform.LookAt(new Vector3(AttackLookAt.position.x, 0, AttackLookAt.position.z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

            LR.SetPosition(0, Player.transform.position);//unde incepe 
            if (Physics.Raycast(Player.transform.position, transform.forward, out hitInfo, TrailDistance))
            {
                LR.SetPosition(1, hitInfo.point);//unde se termina daca gaseste un obstacol
            }
            else
            {
                LR.SetPosition(1, transform.position + transform.forward * TrailDistance);//unde se termina in mod normal
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

        ///////Throwing//////////////////////////
        ///

        if (Mathf.Abs(SuperAttackJoystick.Horizontal) > 0.1f || Mathf.Abs(SuperAttackJoystick.Vertical) > 0.1f)
        {
            if (ThrowLR.gameObject.activeInHierarchy == false)
            {
                ThrowLR.gameObject.SetActive(true);
                SuperAttack = true;
            }

            AttackLookAt.position = new Vector3(SuperAttackJoystick.Horizontal + Player.transform.position.x, 3.5f, SuperAttackJoystick.Vertical + Player.transform.position.z);
            transform.position = new Vector3(Player.position.x, 5f, Player.position.z);
            transform.LookAt(new Vector3(AttackLookAt.position.x, 0, AttackLookAt.position.z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            ThrowLR.SetPosition(0, new Vector3(transform.position.x, 4f, transform.position.z));

            for (int i = 1; i < 10; i++)
            {
                ThrowLR.SetPosition(i, new Vector3(ThrowLR.GetPosition(i - 1).x + SuperAttackJoystick.Horizontal, i == 0 ? 5.5f: i == 1 ? 7.2f : i == 2 ? 10f : i==3? 11.82f: Mathf.Cos(LinePower_Y * (i * 0.1f)) * (i * 4f), ThrowLR.GetPosition(i - 1).z + SuperAttackJoystick.Vertical));
                BulletPoints[i - 1] = ThrowLR.GetPosition(i);
            }


        }
        else
        {
            ThrowLR.gameObject.SetActive(false);

            if (attackDelayTimer > 0)
            {
                attackDelayTimer -= Time.deltaTime;
            }
            else
            {
                attackDelayTimer = 0.1f;
                SuperAttack = false;
            }
        }

        /////////////////////////////////////

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
   public  IEnumerator Shooting()
    {
        if (Shoot && shootingCharges > 0)
        {
            //asdasdasdasdasd
            /*float transformRot = (transform.eulerAngles.y > 180) ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
            float PlayerHipRot = (PlayerHips.eulerAngles.y > 180) ? PlayerHips.eulerAngles.y - 360 : PlayerHips.eulerAngles.y;
            if (transformRot - PlayerHipRot > 90)
            {
                PlayerHips.eulerAngles = new Vector3(PlayerHips.eulerAngles.x, PlayerHips.eulerAngles.y + 90, PlayerHips.eulerAngles.z);
            }
            else if (transformRot - PlayerHipRot < -90)
            {
                PlayerHips.eulerAngles = new Vector3(PlayerHips.eulerAngles.x, PlayerHips.eulerAngles.y - 90, PlayerHips.eulerAngles.z);
            }*/

            //tried to smooth the rotation
            /* var targetRotation = Quaternion.LookRotation(AttackLookAt.transform.position - Player.transform.position);
             PlayerSpine.transform.rotation = Quaternion.Lerp(Player.transform.rotation, targetRotation, 1f);*/

            PlayerSpine.LookAt(AttackLookAt);

            anim.SetTrigger("Shoot");

            PlayerSpine.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Instantiate(Bullet, AttackLookAt.transform.position, transform.rotation);
            Instantiate(ShootingParticles, AttackLookAt.transform.position, transform.rotation);
            Shoot = false;
            shootingCharges -= 1;
            yield return new WaitForSeconds(1f);
            PlayerSpine.localRotation = PlayerSpineChild.localRotation;
        }
    }

    public IEnumerator SuperAttackThrowing()
    {
        if (SuperAttack)
        {
/*            Player.rotation = Quaternion.Euler(90, 90, 0);*/

            anim.SetTrigger("Throw");

            PlayerSpine.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Instantiate(Dynamite, AttackLookAt.transform.position + new Vector3(0,5,0), transform.rotation);
            Instantiate(ShootingParticles, AttackLookAt.transform.position, transform.rotation);
            DynamiteExplosion.Play();
            SuperAttack = false;
           
            yield return new WaitForSeconds(1f);
            PlayerSpine.localRotation = PlayerSpineChild.localRotation;
        }
    }
  
}
