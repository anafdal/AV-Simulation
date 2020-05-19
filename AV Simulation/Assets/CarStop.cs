using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{//found in car(..)

    public NavMeshAgent agent;
    private Vector3 stopdestination;//original destination/target
    private bool stoptime_Car1 = true;//needed so car doesnt have to wait right after person has moved the crosswalk
     private bool stoptime_Car2 = true;//needed so car doesnt have to wait right after person has moved the crosswalk

    //settings
    public static bool stop = false;
    public float time = 5.0f;
    public float stopDistance_Car = 30.0f;//distance to stop behind another 
    public float stopDistance_Stop = 50.0f;//distance to stop behind stoplines or applied stoplights
    public float stopDistance_Person = 70.0f;//distance to stop behind person
    private bool value1 = false;
  
    

    //raycast
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    public float maxDistance = 100.0f;//raycast can detect anything with 100 units. In this simulation 100 units=10 meters.
    RaycastHit raycastHit;//hit
    GameObject hit;


    //used for Images
    //public static int imValue=0;

    void Start()
    {
        agent = agent.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Raycast

        if (transform.gameObject.activeInHierarchy == true)//only when car is active
        {

            Vector3 origin = new Vector3(transform.position.x, -2.0f, transform.position.z);//origin of raycast from center of cube
            Vector3 direction = transform.forward;//direction of raycast

            Ray ray = new Ray(origin, direction);//car raycast

            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {

                stop = true;//has encountered object of interest
                hit = raycastHit.transform.gameObject;

                //hit.GetComponent<Renderer>().material.color = Color.red;//change color
                //Debug.DrawRay(origin, direction * maxDistance, Color.red);//draw it out
                // Debug.Log(agent.transform.name+" "+hit.transform.tag);

                 if (hit.transform.tag == "Stop") {//might need to make separate value for each of this, where you separate each thing and stop tag

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects

                    ChangeIcon(agent.name, distance);


                 }


            }
            else//good here
            {

                
                StopIcon(agent.name);                  
               
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Stopping Procedure


            //Debug.Log(agent + "in " + stop);

            if (stop == true)//if raycast does encounter anything
            {

                if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
                {

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects


                    //stopping
                    if (distance < stopDistance_Stop)
                    {//if no one is crossing, continue usual routine
                        //imValue = 2;////can cross


                        agent.isStopped = true;


                        //determine if there is person or not crossing
                        if (Trigger1.needtoStop1 == true && hit.transform.name == "Stopline (2)(Stop)")//determine which stopline is it referring to if there is someone crossing
                        {

                            stoptime_Car1 = false;//stop at first stopline
                                                  //Debug.Log("Here 2 " + stoptime_Car1);
                           


                        }
                        else if (Trigger2.needtoStop2 == true && hit.transform.name == "Stopline (6)(Stop)")
                        {

                            stoptime_Car2 = false;//stop at second stopline
                                                  //Debug.Log("Here 6" + stoptime_Car2);

                        }

                    }

                    CarDecision();

                }
                else if (hit.transform.tag == "Car")//detects other car in front
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);// calculate distance between objects

                    StopDecision(distance, stopDistance_Car);

                }
                else if (hit.transform.tag == "Protector")//detect a person
                {
                    

                    float distance = Vector3.Distance(transform.position, hit.transform.position);//calculate distance between objects
                    ChangeIcon(agent.name, distance);

                    StopDecision(distance, stopDistance_Person);
                    

                }

                else if (hit.transform.tag == "Stoplight")//encounters stoplight
                {
                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    GameObject stoplight = hit.transform.GetChild(1).gameObject;
                    //child 2 is red light
                    //child 3 is green light

                    Light red = stoplight.transform.Find("red light").GetComponent<Light>();
                    Light green = stoplight.transform.Find("green light").GetComponent<Light>();


                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance
                    

                    if (red.enabled == true)//if red is on
                    {
                        value1 = true;
                        ChangeIcon2(agent.name, distance, red.enabled);//only if light is red

                        if (distance < stopDistance_Stop)
                        {
                           
                            agent.isStopped = true;//agent will stop
                            

                        }
                    }

                    else if (green.enabled == true)//if green is on
                    {
                        StopIcon2(agent.name);
                        agent.isStopped = false;//agent will move
                        
                    }


                   


                }


                stop = false;//nothing is being detected by raycast anymore
              

            }
            else if (stop == false)//if nothing is being detected car will continue on its path
            {

                agent.isStopped = false;
                agent.SetDestination(agent.steeringTarget);

                //Debug.Log(agent+": "+agent.isOnNavMesh);
                //transform.LookAt(agent.steeringTarget);
            }

            //Debug.Log(agent + " out " + stop);

        }



    }

    private void CarDecision()
    {
        //restarting
        if (Trigger1.needtoStop1 == false && hit.transform.name == "Stopline (2)(Stop)")
        {
            StartCoroutine(CarCoroutine1());

        }
        else if (Trigger2.needtoStop2 == false && hit.transform.name == "Stopline (6)(Stop)")
        {

            StartCoroutine(CarCoroutine2());

        }

    }

    IEnumerator CarCoroutine1()//wait for ... seconds before car becomes active
    {
      
            if (stoptime_Car1 == false)
            {
                yield return new WaitForSeconds(1.0f);

            }
          else
          {
          
                yield return new WaitForSeconds(time);

          }

        if (Trigger1.needtoStop1 == false)//one last check
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }


    }

    IEnumerator CarCoroutine2()//wait for ... seconds before car becomes active
    {
       
        if (stoptime_Car2 == false)
        {
            yield return new WaitForSeconds(1.0f);
          
        }
        else
        {
            
            yield return new WaitForSeconds(time);

        }

        if (Trigger2.needtoStop2 == false)//one last check
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }

    }


    public void StopDecision(float distance,float stopDistance)
    {
        if (distance < stopDistance)//stop the agnet behind the other car at this distance
        {

            agent.velocity = Vector3.zero;
            agent.isStopped = true;

        }
        else
        {
            agent.SetDestination(agent.steeringTarget);//once car in front moves, resume path
            agent.isStopped = false;
        }


    }



    public void ChangeIcon(string carName,float distance)//for stop signs and passerbys
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
        else if(carName== "Car (2)")
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

    public void StopIcon(string carName)//no icons
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

    public void ChangeIcon2(string carName, float distance, bool red)//for stoplights
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

    public void StopIcon2(string carName)//for stoplights
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



 


