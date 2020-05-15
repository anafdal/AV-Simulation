using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{//found in car(..)


    //settings private
    public NavMeshAgent agent;
    private Vector3 stopdestination;//original destination/target
    private bool stoptime_Car1 = true;//needed so car doesnt have to wait right after person has moved the crosswalk
    private bool stoptime_Car2 = true;//needed so car doesnt have to wait right after person has moved the crosswalk
    private bool value = false;

    //setting public
    public static bool stop = false;
    public float time = 5.0f;
    public float stopDistance_Car = 20.0f;//distance to stop behind another 
    public float stopDistance_Stop = 50.0f;//distance to stop behind stoplines or applied stoplights
    public float stopDistance_Person = 70.0f;//distance to stop behind person
    

    //raycast
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    public float maxDistance = 100.0f;//raycast can detect anything with 100 units. In this simulation 100 units=10 meters.
    RaycastHit raycastHit;//hit
    GameObject hit;

    void Start()
    {
        agent = agent.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
       

        if (transform.gameObject.activeInHierarchy == true)//only when car is active
        {
            Raycasting();

            StopCar();

        }

    }


    private void Raycasting()
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
            //Debug.Log(agent.transform.name + " " + hit.transform.tag);

            if (hit.transform.tag == "Stop")
            {//might need to make separate value for each of this, where you separate each thing and stop tag

                stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects

                ChangeIcon(agent.name, distance);


            }


        }
        else//good here
        {


            StopIcon(agent.name);

        }
    }
    private void StopCar()//all the stopping decisions are made here
    {
        if (stop == true)//if raycast does encounter anything
        {

            if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
            {
                StopSign();

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
                StopLight();

            }


            stop = false;//nothing is being detected by raycast anymore


        }
        else if (stop == false)//if nothing is being detected car will continue on its path
        {

            //agent.isStopped = false;
            agent.SetDestination(agent.steeringTarget);
        }
    }

    private void StopLight()//for the stoplights only
    {
        stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
        GameObject stoplight = hit.transform.GetChild(1).gameObject;
        //child 2 is red light
        //child 3 is green light

        Light red = stoplight.transform.Find("red").GetComponent<Light>();
        Light green = stoplight.transform.Find("green").GetComponent<Light>();


        float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance


        if (red.enabled == true)//if red is on
        {
            value = true;

            if (distance < stopDistance_Stop)
            {

                agent.isStopped = true;//agent will stop


            }
        }

        else if (green.enabled == true)//if green is on
        {

            agent.isStopped = false;//agent will move

        }


        if (value == true)
        {
            ChangeIcon2(agent.name, distance, red.enabled);//only if light is red

        }
    }


    private void StopSign()//for the stopsigns only
    {
        stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
        float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects


        //stopping
        if (distance < stopDistance_Stop)
        {//if no one is crossing, continue usual routine
         //imValue = 2;////can cross


            agent.isStopped = true;


            //determine if there is person or not crossing
            if (Trigger1.needtoStop1 == true)//determine which stopline is it referring to if there is someone crossing
            {

                stoptime_Car1 = false;//stop at first stopline
                                      //Debug.Log("Here 2 " + stoptime_Car1);


            }
            else if (Trigger2.needtoStop2 == true)
            {

                stoptime_Car2 = false;//stop at second stopline
                                      //Debug.Log("Here 6" + stoptime_Car2);

            }

        }



        //restarting
        if (Trigger1.needtoStop1 == false && hit.transform.name == Trigger1.parent)
        {
            StartCoroutine(CarCoroutine1());

        }
        else if (Trigger2.needtoStop2 == false && hit.transform.name == Trigger2.parent)
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
            // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            
        }
        
        agent.isStopped = false;
        
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

        
        agent.isStopped = false;
        
    }


    private void StopDecision(float distance,float stopDistance)//for passebys and cars only
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


/////////////////////////////////////////////////////////////////////////////////////////////////////////put this in another script


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

        if (carName == "Car (1)")
        {
            if (35 < distance && distance < 100 && agent.isStopped == false && red==true)
            {
                IconDetect1.imValue = 1;

            }
            else if (agent.isStopped == true && distance <= 35  && red==true)
            {
                IconDetect1.imValue = 2;
                //Debug.Log(imValue);
            }
            else if (agent.isStopped == false && distance <= 20 && red==false)
            {
                IconDetect1.imValue = 3;
                value = false;
            }
            
        }
        else if (carName == "Car (2)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect2.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (3)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect3.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (4)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect4.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (5)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect5.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (6)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect6.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (7)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect7.imValue = 3;
                value = false;
            }

        }
        else if (carName == "Car (8)")
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
            else if (agent.isStopped == false && distance <= 20 && red == false)
            {
                IconDetect8.imValue = 3;
                value = false;
            }

        }

    }



}



 


