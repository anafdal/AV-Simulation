using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{
    public NavMeshAgent agent;
   


    private Vector3 stopdestination;//original destination/target
    public Light green;//stoplight go
    public Light red;//stoplight stop
    bool stop = false;
    public float time = 4.0f;


    [SerializeField]
    private LayerMask layerMask;
    public float maxDistance = 100.0f;//10 meters
    RaycastHit raycastHit;//hit
    GameObject hit;



    void Update()
    {
        

        if (transform.gameObject.activeInHierarchy == true)//only when car is active
        {

           Vector3 origin = new Vector3(transform.position.x, 0.0f, transform.position.z);//origin of raycast from center of cube
            Vector3 direction = transform.forward;//direction of raycast

            Ray ray = new Ray(origin, direction);//car raycast

            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {

                stop = true;//has encountered stopline
                hit = raycastHit.transform.gameObject;

                hit.GetComponent<Renderer>().material.color = Color.red;//change color
                Debug.DrawRay(origin, direction * maxDistance, Color.red);//draw it out
                //Debug.Log(hit.name);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




            if (stop == true)//if raycast encounters anything
            {

                if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    if (agent.destination != stopdestination)
                    {

                        if (!agent.pathPending)
                        {
                            float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                            if (dist1 < 50.0f)//5 meters
                            {


                                agent.isStopped = true;
                                StartCoroutine(ExampleCoroutine());
                            }
                        }
                        else
                        {
                            float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                            if (dist1 < 10.0f)//time it takes to stop completely
                            {

                                agent.isStopped = true;
                                StartCoroutine(ExampleCoroutine());

                            }
                        }

                    }
          
                }
                else if (hit.transform.tag == "Car")//detects other car in front
                {


                    float distance1 = Vector3.Distance(transform.position, hit.transform.position);//distance between objects

                    if (!agent.pathPending)
                    {

                        if (distance1 < 50.0f)//5 meters
                        {


                            agent.isStopped = true;


                        }

                        else
                        {

                            agent.isStopped = false;

                        }
                    }
                    else
                    {

                        if (distance1 < 50.0f)//5 meters
                        {


                            agent.isStopped = true;


                        }

                        else
                        {

                            agent.isStopped = false;

                        }


                    }
                  

                }
                else if(hit.transform.tag == "Stoplight")//encounters stoplight
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    if (red.enabled == true)
                    {
                        if (!agent.pathPending)
                        {
                            float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                            if (dist1 < 50.0f)//5 meters
                            {


                                agent.isStopped = true;
                               
                            }
                        }
                        else
                        {
                            float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                            if (dist1 < 10.0f)//time it takes to stop completely
                            {

                                agent.isStopped = true;
                                

                            }
                        }
                    }
                    else if (green.enabled == true)
                    {
                        agent.isStopped = false;
                    }
                }


                stop = false;
            }
            else
                agent.isStopped = false;
                agent.SetDestination(agent.steeringTarget);
         

        }


    }




    IEnumerator ExampleCoroutine()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
       // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        if (stop ==false)
        {
            agent.isStopped = false;
        }
    }

}



 


