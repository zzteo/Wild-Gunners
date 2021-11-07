using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteThrow : MonoBehaviour
{
    PlayerAttackingV2 PA;

    [SerializeField]
    Vector3[] Points;

    Rigidbody RB;

    [SerializeField]
    float speed;

    bool Throw;

    int CurrentIndex;

    [SerializeField]
    ParticleSystem blood;
    [SerializeField]
    ParticleSystem hitObjects;


    private void Start()
    {
        PA = GameObject.Find("AttackTrail").GetComponent<PlayerAttackingV2>();

        Points = new Vector3[9];

        PA.BulletPoints.CopyTo(Points, 0);

        RB = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.LookAt(Points[CurrentIndex]);

        if (Throw)
        {
            
            transform.LookAt(Points[CurrentIndex]);
        }
        else if((Points[CurrentIndex] - transform.position).sqrMagnitude<0.2f)
        {
            if (CurrentIndex == 8)
            {
                RB.useGravity = true;
            }
            else
            {
                CurrentIndex++;

                transform.LookAt(Points[CurrentIndex]);
            }
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("are player Script");
           /* Destroy(this.gameObject);
            Instantiate(blood, this.transform.position, this.transform.rotation);*/

            other.GetComponent<PlayerScript>().TakeDamage(50);
        }
        else if (other.gameObject.tag == "Map")
        {
            Destroy(this.gameObject);
            Instantiate(hitObjects, this.transform.position, this.transform.rotation);
        }
        /* else if (other.gameObject.tag == "Player")
         {
             Destroy(this.gameObject);
             Instantiate(hitObjects, this.transform.position, this.transform.rotation);
         }*/
    }
}
