using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 stopdestination;//original destination/target

    public static bool stop = false;
    public float time = 4.0f;
     // private Light red;
    //private Light green;


    [SerializeField]
    private LayerMask layerMask=new LayerMask();
    public float maxDistance = 100.0f;//10 meters
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

            Vector3 origin = new Vector3(transform.position.x, 0.0f, transform.position.z);//origin of raycast from center of cube
            Vector3 direction = transform.forward;//direction of raycast

            Ray ray = new Ray(origin, direction);//car raycast

            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {

                stop = true;//has encountered stopline
                hit = raycastHit.transform.gameObject;

                //hit.GetComponent<Renderer>().material.color = Color.red;//change color
                Debug.DrawRay(origin, direction * maxDistance, Color.red);//draw it out
                //Debug.Log(hit.name);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Debug.Log(agent + "in " + stop);

            if (stop== true)//if raycast encounters anything
            {

               if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded

                                Stop_Car(dist1);
                                StartCoroutine(ExampleCoroutine());
                                //stop = false;
                                                
                }
              else if (hit.transform.tag == "Car")//detects other car in front
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    float distance1 = Vector3.Distance(transform.position, stopdestination);//distance between objects

                        Stop_Car(distance1);
                        //stop = false;
                                    
                }
              else if (hit.transform.tag == "Stoplight")//encounters stoplight
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    GameObject stoplight= hit.transform.GetChild(1).gameObject;
                    //child 2 is red light
                    //child 3 is green light

                    Light red = stoplight.transform.Find("red").GetComponent<Light>();
                    Light green= stoplight.transform.Find("green").GetComponent<Light>();

                    if (red.enabled == true)
                    {

                        float dist1 = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                        if (dist1 < 60.0f)//5 meters
                        {
                            agent.isStopped = true;
                            //stop = false;


                        }
                    }
             
                else if (green.enabled == true)
                    {
                        agent.isStopped = false;
                        //stop = false;                      
                    }


                }

                stop = false;
            }
            else if(stop==false)
            {
               
                agent.isStopped = false;
                //transform.LookAt(agent.steeringTarget);
                agent.SetDestination(agent.steeringTarget);
            
                //Debug.Log(agent+": "+agent.isOnNavMesh);
                
            }

           //Debug.Log(agent + " out " + stop);

        }

        

    }




    IEnumerator ExampleCoroutine()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
       // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
      
            agent.isStopped = false;
            //agent.SetDestination(agent.steeringTarget);
        
    }


    void Stop_Car(float distance)
    {
        if (distance < 50.0f)//5 meters
        {


            agent.isStopped = true;
           

        }
        else
        {
            agent.isStopped = false;
          
        }
    }

}



 


