using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
 

    [SerializeField]
    DynamicJoystick joystick;

    [SerializeField]
    Transform directionYouGoTowards; //bila de desubtul playerului

    [SerializeField]
    Animator anim;

    [SerializeField]
    float Speed;

    bool Movement;
 

    private void Update()
    {
       /* Debug.Log(transform.eulerAngles);*/
    }

    void FixedUpdate()
    {
        

        if (joystick.Horizontal > 0 || joystick.Horizontal < 0 || joystick.Vertical > 0 || joystick.Vertical > 0)
        {
            directionYouGoTowards.position = new Vector3(joystick.Horizontal + transform.position.x, 3.2f, joystick.Vertical + transform.position.z);

            transform.LookAt(new Vector3(directionYouGoTowards.position.x, 0, directionYouGoTowards.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);


            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            if (anim.GetBool("isRunning") != true)
            {
                anim.SetBool("isRunning", true);
            }            
            Movement = true;
            directionYouGoTowards.gameObject.SetActive(true);
            directionYouGoTowards.position = new Vector3(joystick.Horizontal + transform.position.x, 3.2f, joystick.Vertical + transform.position.z);
        }
        else if (Movement == true)
        {
            anim.SetBool("isRunning", false);        
            Movement = false;
            directionYouGoTowards.gameObject.SetActive(false);
            changePlayerRot();
        }

       
    }

 private void changePlayerRot()
    {
        transform.eulerAngles = new Vector3(0, directionYouGoTowards.eulerAngles.y, directionYouGoTowards.eulerAngles.z);
    }

}
