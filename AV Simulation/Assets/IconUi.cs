using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IconUi : MonoBehaviour
{
    public static void ChangeIcon(string carName, float distance, NavMeshAgent agent)//for stop signs and passerbys
    {

        if (carName == "Car (1)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect1.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect1.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect1.imValue = 3;

            }
        }
        else if (carName == "Car (2)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect2.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect2.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect2.imValue = 3;
            }
        }
        else if (carName == "Car (3)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect3.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect3.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect3.imValue = 3;
            }

        }
        else if (carName == "Car (4)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect4.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect4.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect4.imValue = 3;
            }

        }
        else if (carName == "Car (5)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect5.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect5.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect5.imValue = 3;
            }

        }
        else if (carName == "Car (6)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect6.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect6.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect6.imValue = 3;
            }

        }
        else if (carName == "Car (7)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect7.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect7.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect7.imValue = 3;
            }

        }
        else if (carName == "Car (8)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false)
            {
                IconDetect8.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35)
            {
                IconDetect8.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20)
            {
                IconDetect8.imValue = 3;
            }

        }

    }

    public static void StopIcon(string carName)//no icons
    {

        if (carName == "Car (1)")
        {

            IconDetect1.imValue = 0;
        }
        else if (carName == "Car (2)")
        {
            IconDetect2.imValue = 0;

        }
        else if (carName == "Car (3)")
        {
            IconDetect3.imValue = 0;

        }
        else if (carName == "Car (4)")
        {
            IconDetect4.imValue = 0;

        }
        else if (carName == "Car (5)")
        {
            IconDetect5.imValue = 0;

        }
        else if (carName == "Car (6)")
        {
            IconDetect6.imValue = 0;

        }
        else if (carName == "Car (7)")
        {
            IconDetect7.imValue = 0;

        }
        else if (carName == "Car (8)")
        {
            IconDetect8.imValue = 0;

        }

    }

    public static void ChangeIcon2(string carName, float distance, bool red, bool value1, NavMeshAgent agent)//for stoplights
    {

        if (carName == "Car (1)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect1.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect1.imValue = 2;
                //Debug.Log(imValue);
            }
        }

        else if (carName == "Car (2)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect2.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect2.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (3)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect3.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect3.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (4)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect4.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect4.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (5)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect5.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect5.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (6)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect6.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect6.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (7)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect7.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect7.imValue = 2;
                //Debug.Log(imValue);
            }
        }
        else if (carName == "Car (8)" && value1 == true)
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red == true)
            {
                IconDetect8.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35 && red == true)
            {
                IconDetect8.imValue = 2;
                //Debug.Log(imValue);
            }
        }

    }

    public static void StopIcon2(string carName, bool value1, NavMeshAgent agent)//for stoplights
    {

        if (carName == "Car (1)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect1.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (2)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect2.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (3)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect3.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (4)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect4.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (5)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect5.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (6)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect6.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (7)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect7.imValue = 3;
                value1 = false;
            }
        }
        else if (carName == "Car (8)" && value1 == true)
        {
            if (agent.isStopped == false)
            {
                IconDetect8.imValue = 3;
                value1 = false;
            }
        }

    }
}
