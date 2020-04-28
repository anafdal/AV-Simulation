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
    public float stopDistance_Car = 20.0f;//distance to stop behind another 
    public float stopDistance_Stop = 50.0f;//distance to stop behind stoplines or applied stoplights
    public float stopDistance_Person = 70.0f;//distance to stop behind person

    //raycast
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    public float maxDistance = 100.0f;//raycast can detect anything with 100 units. In this simulation 100 units=10 meters.
    RaycastHit raycastHit;//hit
    GameObject hit;


    //used for Images
    public static int imValue=0;

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

                 if (hit.transform.tag == "Stop" || hit.transform.tag == "Protector" || hit.transform.tag == "Stoplight") {//might need to make separate value for each of this, where you separate each thing and stop tag

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects

                    if (20< distance && distance<70 && agent.isStopped==false)
                    {
                        imValue = 1;
                        
                    }
                    else if (agent.isStopped==true && distance<=20)
                    {
                        imValue = 2;
                        Debug.Log(imValue);
                    }
                    else if (agent.isStopped == false && distance <= 20)
                    {
                        imValue = 3;
                    }
                
                 }
                else
                {
                    imValue = 0;
                }
          
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
                    if (distance < stopDistance_Stop){//if no one is crossing, continue usual routine
                        //imValue = 2;////can cross


                        agent.isStopped = true;

                       
                        //determine if there is person or not crossing
                        if (Trigger1.needtoStop1 == true && hit.transform.name== "Stopline (2)(Stop)")//determine which stopline is it referring to if there is someone crossing
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
                else if (hit.transform.tag == "Car")//detects other car in front
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    float distance = Vector3.Distance(transform.position, stopdestination);// calculate distance between objects


                    if (distance < stopDistance_Car)//stop the agnet behind the other car at this distance
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
                else if (hit.transform.tag == "Protector")//detect a person
                {
                    //imValue = 2;//can cross

                    float distance = Vector3.Distance(transform.position, hit.transform.position);//calculate distance between objects


                    if (distance < stopDistance_Person)//stop at this distance
                    {

                        agent.velocity = Vector3.zero;
                        Debug.Log("Stop Here");
                         

                    }
                    else
                    {
                        //imValue = 3;
                        agent.SetDestination(agent.steeringTarget);//resume path once person moves away
                     
                    }


                }

                else if (hit.transform.tag == "Stoplight")//encounters stoplight
                {
                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    GameObject stoplight = hit.transform.GetChild(1).gameObject;
                    //child 2 is red light
                    //child 3 is green light

                    Light red = stoplight.transform.Find("red").GetComponent<Light>();
                    Light green = stoplight.transform.Find("green").GetComponent<Light>();

                    if (red.enabled == true)//if red is on
                    {

                        float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance
                        if (distance < stopDistance_Stop)
                        {
                           //imValue = 2;
                            agent.isStopped = true;//agent will stop


                        }
                    }

                    else if (green.enabled == true)//if green is on
                    {
                        //imValue = 3;
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



    IEnumerator CarCoroutine1()//wait for ... seconds before car becomes active
    {
        if (stoptime_Car1 == false)
        {
            yield return new WaitForSeconds(1.0f);
            //imValue = 3;
        }
        else
        {
            
            yield return new WaitForSeconds(time);
            // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            //imValue = 3;
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

}



 


