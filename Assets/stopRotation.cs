using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopRotation : MonoBehaviour
{
 
    void Update()
    {
        this.transform.eulerAngles = new Vector3(0, 0, 0); 
    }
}
