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
    private float previous1;
    private float previous2;

    [SerializeField]
    private LayerMask layerMask=new LayerMask();
    public float maxDistance = 100.0f;//10 meters
    RaycastHit raycastHit;//hit
    GameObject hit;

   void Start()
    {
        agent = agent.GetComponent<NavMeshAgent>();
        previous1 = agent.acceleration;
        previous2 = agent.speed; ;
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
                Debug.Log(hit.transform.tag);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Debug.Log(agent + "in " + stop);

            if (stop== true)//if raycast encounters anything
            {

                if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded

                    Stop_Car(distance);

                    if (Trigger1.needtoStop1 == true && hit.name== "Stopline(2)(Stop)")
                    {
                        agent.isStopped = true;
                        
                    }
                    else if(Trigger1.needtoStop1 == true && hit.name == "Stopline (6)(Stop)")
                    {
                        agent.isStopped = true;
                    }
                    else 
                        StartCoroutine(ExampleCoroutine());

                    
                    

                }
                else if (hit.transform.tag == "Car")//detects other car in front
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    float distance = Vector3.Distance(transform.position, stopdestination);//distance between objects

                    //Stop_Car(distance);
                    //stop = false;
                    if (distance < 30.0f)
                    {

                        agent.velocity = Vector3.zero;



                    }
                    else
                    {
                        agent.SetDestination(agent.steeringTarget);

                    }



                }
                else if (hit.transform.tag == "Player")//detect the person
                {
                    //agent.acceleration = 70000000000000;
                    //stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place

                    float distance = Vector3.Distance(transform.position, hit.transform.position);//distance between objects


                    if (distance < 30.0f)
                    {

                        agent.velocity = Vector3.zero;
                      
                     

                    }
                    else
                    {
                        agent.SetDestination(agent.steeringTarget);

                    }


                }

                else if (hit.transform.tag == "Stoplight")//encounters stoplight
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    GameObject stoplight = hit.transform.GetChild(1).gameObject;
                    //child 2 is red light
                    //child 3 is green light

                    Light red = stoplight.transform.Find("red").GetComponent<Light>();
                    Light green = stoplight.transform.Find("green").GetComponent<Light>();

                    if (red.enabled == true)
                    {

                        float distance = Vector3.Distance(transform.position, stopdestination);//if path is still being decided and information has not loaded
                        if (distance < 60.0f)//5 meters
                        {
                            agent.isStopped = true;


                        }
                    }

                    else if (green.enabled == true)
                    {
                        agent.isStopped = false;

                    }


                }

                stop = false;
               
            }
            else if(stop==false)
            {
               
                agent.isStopped = false;
                
                agent.SetDestination(agent.steeringTarget);

                //Debug.Log(agent+": "+agent.isOnNavMesh);
                //transform.LookAt(agent.steeringTarget);
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
        if (distance < 60.0f)//5 meters
        {


            agent.isStopped = true;
           

        }
        else
        {
            agent.isStopped = false;
          
        }
    }

}



 


