using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private Vector3 target = new Vector3(-419.68f, -20.01f, 1279.55f);

    private void Update()
    {
        transform.RotateAround(target, Vector3.up, 6 * Time.deltaTime);
    }
}
