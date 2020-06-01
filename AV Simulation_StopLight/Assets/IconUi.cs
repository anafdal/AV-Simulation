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
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                detect.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
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

    public static void StopIcon(NavMeshAgent agent)//no icons
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

        detect.imValue = 0;

    }

    public static void ChangeIcon2(float distance, bool red, bool value1, NavMeshAgent agent)//for stoplights
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

        if ( value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                detect.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                detect.imValue = 2;
                //Debug.Log(imValue);
            }
        }
   

    }

    public static void StopIcon2(bool value1, NavMeshAgent agent)//for stoplights
    {
        IconDetect detect = agent.gameObject.GetComponent<IconDetect>();

        if (value1 == true)
        {
            if (agent.isStopped == false)
            {
                detect.imValue = 3;
                value1 = false;
            }
        }
    }
       
}
