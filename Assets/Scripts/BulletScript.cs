using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    PlayerAttackingV2 PA;

    [SerializeField]
    AudioClip gotShot;

    [SerializeField]
    ParticleSystem blood;
    [SerializeField]
    ParticleSystem hitObjects;



    Vector3 BulletEndDistance;

    [SerializeField]
    float speed;
    void Start()
    {
     
        PA = GameObject.Find("AttackTrail").GetComponent<PlayerAttackingV2>();
        BulletEndDistance = transform.position + transform.forward * PA.TrailDistance;
    }

    // Update is called once per frame
    void Update()
    {        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
       
        if (transform.position == BulletEndDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if(other.gameObject.tag == "Enemy") {
            Destroy(this.gameObject); 

        }*/
       if(other.GetComponent<PlayerScript>() != null)
        {
            Debug.Log("are player Script");
            Destroy(this.gameObject);
            Instantiate(blood, this.transform.position, this.transform.rotation);
           
            other.GetComponent<PlayerScript>().TakeDamage(20);
           
        }
        else if (other.gameObject.tag == "Map")
        {
            Destroy(this.gameObject);
            Instantiate(hitObjects, this.transform.position, this.transform.rotation);
        }
    }
}
