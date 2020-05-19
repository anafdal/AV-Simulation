using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{//script in car(..)
 //modified from https://docs.unity3d.com/Manual/nav-AgentPatrol.html

    public Transform[] points;//points you will use to move
    public NavMeshAgent agent;
    private int destPoint;
    private GameObject Startpoint;


    private void OnEnable()//this works only when car is enabled
    {
        agent = this.GetComponent<NavMeshAgent>();

        Startpoint = RecycleRoad.newStartObject;//if start is this go here if not go there

        agent.Warp(new Vector3(RecycleRoad.newStartObject.transform.position.x, 0.0f, RecycleRoad.newStartObject.transform.position.z));//warp into random startposition   
        agent.transform.rotation = Startpoint.transform.rotation;

        destPoint = CalculateNextDestination(Startpoint);//depended on where car starts from
    }


    void FixedUpdate()
    {
       
        if (!agent.pathPending && agent.remainingDistance < 0.5)//follow path
        {

            if (points[destPoint].gameObject.name == "Waypoint (1.0)" )
            {
             
                agent.Warp(points[destPoint].position);//warp the agent at waypoint(1) immediately

            }
            else if(points[destPoint].gameObject.name == "Waypoint (1.1)")
            {
                agent.Warp(points[destPoint].position);//warp the agent at waypoint(12) immediately
                agent.transform.rotation = points[destPoint].localRotation;
            }


            GoToNextPoint();

        }

    }


    void GoToNextPoint()//follow the waypoints
    {
        if (points.Length == 0)
        {
            return;
        }
        else
        {
                              
                agent.destination = points[destPoint].position;
                //Debug.Log(points[destPoint].gameObject.name);
                     
        }
        destPoint = (destPoint + 1) % points.Length;//goes through all of them

    }

    
    private int CalculateNextDestination(GameObject Startpoint)///calculate next destination relative to the current position; 
    {
        int destination=0;

       if(Startpoint.name== "Startpoint (1)")
        {
            destination = 4;//if car starts from Startpoint (1) got to next waypoint 

        }
       else if(Startpoint.name == "Startpoint (2)")
        {
            destination = 7;//if car starts from Startpoint (2) got to next waypoint 
        }

        return destination;
    }

  
}