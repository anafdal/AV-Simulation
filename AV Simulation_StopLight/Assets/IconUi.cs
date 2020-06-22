using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IconUi : MonoBehaviour
{
    public static void ChangeIcon(float distance, NavMeshAgent agent)//for stop signs and passerbys
    {
        IconDetect detect=agent.gameObject.GetComponent<IconDetect>();

        {
            
                if (40 < distance && distance < 100 && agent.velocity != Vector3.zero)
                {
                    detect.imValue = 1;

                }
                else if (agent.velocity == Vector3.zero && distance <= 40)
                {
                    detect.imValue = 2;
                    //Debug.Log(imValue);
                }
                else if (agent.isStopped == false && distance <= 20)
                {
                    detect.imValue = 3;

                }
            
           
           
        }
       

    }

    public static void StopIcon(NavMeshAgent agent)//only running icons
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

        detect.imValue = 0;

    }




   public static void ChangeIcon2(float distance, bool red, bool value2, NavMeshAgent agent)//for stoplights
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

       
            if (40 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                detect.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 40 && red == true)
            {
                detect.imValue = 2;
                //Debug.Log(imValue);
            }
       
   

    }

    public static bool StopIcon2(bool value2, NavMeshAgent agent)//for stoplights
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

        if (value2 == true)//this agent has already been stopped
        {
            if (agent.isStopped == false)
            {
                detect.imValue = 3;
                value2 = false;             
            }

           
        }

        return value2;
    }
       
}
