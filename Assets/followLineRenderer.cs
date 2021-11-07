using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followLineRenderer : MonoBehaviour
{
    PlayerAttackingV2 PA;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.Find("AttackTrail").GetComponent<PlayerAttackingV2>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PA.BulletPoints[8];
    }
}
