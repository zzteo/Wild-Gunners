using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float Zoffset;

    //borders of the map
    [SerializeField]
    float topBorder;
    [SerializeField]
    float bottomBorder;

    void Update()
    {
        //Camera's position
        Vector3 startPos = transform.position;
        //player's position

        //moves the camera to the player smoothly
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y, Player.transform.position.z + Zoffset);

        transform.position = Vector3.Lerp(startPos, endPos, 1.2f * Time.deltaTime);

        //checks for the map's borders and stops camera when it reaches them
       if(transform.position.z > topBorder)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBorder);
        }
       else if(transform.position.z < bottomBorder)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBorder);
        }

    }
}
