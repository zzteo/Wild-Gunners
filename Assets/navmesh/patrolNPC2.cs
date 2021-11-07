using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolNPC2 : NPCbase
{
    GameObject[] waypoints;
    int currentWP;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint2");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex); 
        currentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0) return;

        //check if the player is close to the waypoint he is heading to
        if(Vector3.Distance(waypoints[currentWP].transform.position,
                                NPC.transform.position) < accuracy)
        {
            currentWP++;
            if(currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }
        //calculate the direction in which we want the player to move to
        var direction = waypoints[currentWP].transform.position - NPC.transform.position;
        //slowly turn the player towards the waypoint
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                Quaternion.LookRotation(direction),
                                rotSpeed * Time.deltaTime);
        //pushing the player forward on his z axis, which is his forward facing axis
        NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
