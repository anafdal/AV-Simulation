using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{//found in car(..)

    public NavMeshAgent agent;
    private Vector3 stopdestination;//original destination/target
    private bool stoptime_Car1 = true;//needed so car doesnt have to wait right after person has moved the crosswalk
    private bool stoptime_Car3 = true;
    private bool stoptime_Car4 = true;

    //settings
    public static bool stop = false;
    public float time = 5.0f;
    public float stopDistance_Car = 45.0f;//distance to stop behind another
    public float stopDistance_Stop = 50.0f;//distance to stop behind stoplines or applied stoplights
    public float stopDistance_Person = 30.0f;//distance to stop behind person
    private bool value1 = false;



    //raycast
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    public float maxDistance = 90.0f;//raycast can detect anything with 90 units
    RaycastHit raycastHit;//hit
    GameObject hit;


    void Start()
    {
        agent = agent.GetComponent<NavMeshAgent>();

        //test
       // color = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
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

                hit.GetComponent<Renderer>().material.color = Color.red;//change color
                Debug.DrawRay(origin, direction * maxDistance, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));//draw it out
                //Debug.Log(agent.transform.name+" "+hit.transform.tag);

                 if (hit.transform.tag == "Stop") {//might need to make separate value for each of this, where you separate each thing and stop tag

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects

                    IconUi.ChangeIcon(distance,agent);


                 }


            }
            else//good here
            {


                IconUi.StopIcon(agent);

            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Stopping Procedure


           
               //Debug.Log(Mathf.Sqrt(Vector3.Dot(agent.velocity, agent.velocity)));//use for Car 8
            

            //Debug.Log(agent + "in " + stop);

            if (stop == true)//if raycast does encounter anything
            {

                if (hit.gameObject.transform.tag == "Stop")//encounters stop line for simple stop sign
                {

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects
                    GameObject trigger = hit.transform.GetChild(1).gameObject;


                    //stopping
                    if (distance < stopDistance_Stop)
                    {//if no one is crossing, continue usual routine




                        agent.isStopped = true;
                        PedestrianCheck(trigger);

                       /* //determine if there is person or not crossing
                        if (Trigger1.needtoStop1 == true && hit.transform.name == "Stopline (2)(Stop)")//determine which stopline is it referring to if there is someone crossing
                        {

                            stoptime_Car1 = false;//stop at first stopline
                                                  //Debug.Log("Here 2 " + stoptime_Car1);



                        }
                        else if (Trigger2.needtoStop2 == true && hit.transform.name == "Stopline (6)(Stop)")
                        {

                            stoptime_Car2 = false;//stop at second stopline
                                                  //Debug.Log("Here 6" + stoptime_Car2);

                        }*/

                    }

                    CarDecision(trigger);


                }
                else if (hit.transform.tag == "Car1" || hit.transform.tag == "Car2")//detects other car in front
                {
                    stopdestination = hit.transform.GetChild(0).position;//psoition to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);// calculate distance between objects

                    StopDecision(distance, stopDistance_Car);

                }
                else if (hit.transform.tag == "Protector")//detect a person
                {


                    float distance = Vector3.Distance(transform.position, hit.transform.position);//calculate distance between objects
                    IconUi.ChangeIcon(distance, agent);

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
                        IconUi.ChangeIcon2(distance, red.enabled, value1, agent);//only if light is red
                       //Debug.Log(agent.name+" :"+distance);


                        StopLightTurn(distance);
                       //Debug.Log(agent.name + " hit");

                    }

                    else if (green.enabled == true)//if green is on
                    {
                        IconUi.StopIcon2(value1, agent);

                        CheckPedestrainCrossRoad(distance);
                    }

                }
                else if (hit.transform.tag == "Check")//prevent the agent froms stopping in the midddle of the crossroad if light turns red
                {
                   float distance = Vector3.Distance(transform.position, hit.transform.position);//calculate distance

                    if (distance < 20)
                    {
                        agent.isStopped = false;//agent will move
                        agent.SetDestination(agent.steeringTarget);
                    }



                    //Debug.Log(agent.name+": Hit");
                }


                stop = false;//nothing is being detected by raycast anymore


            }
            else if (stop == false)//if nothing is being detected car will continue on its path
            {

                agent.isStopped = false;
                agent.SetDestination(agent.steeringTarget);

                stoptime_Car1 = true;//need this here
                //Debug.Log(agent+": "+agent.isOnNavMesh);
                //transform.LookAt(agent.steeringTarget);
            }

            //Debug.Log(agent + " out " + stop);

        }



    }



    public void StopDecision(float distance, float stopDistance)//stops behinds other non-moving cars or pedestrians crossing the road
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

    private void PedestrianCheck(GameObject trigger) {///checks if they is a person walking on the pedestrian walk at the stop sign

        /* if (Trigger1.needtoStop1 == true)//determine which stopline is it referring to if there is someone crossing
         {

             stoptime_Car1 = false;//stop at first stopline
                                   //Debug.Log("Here 2 " + stoptime_Car1);
         }
         else if (Trigger2.needtoStop2 == true)
         {

             stoptime_Car2 = false;//stop at second stopline
                                   //Debug.Log("Here 6" + stoptime_Car2);

         }*/

        if (trigger.GetComponent<Trigger1>().needtoStop1 == true)
        {
            stoptime_Car1 = false;
        }


    }

    private void CarDecision(GameObject trigger)//decides if the car needs to wait for pedestrian or leave after pedestrian has crossed
    {
        //restarting
        if (trigger.GetComponent<Trigger1>().needtoStop1 == false)
        {
            StartCoroutine(CarCoroutine1(trigger));

        }
       /* else if (Trigger2.needtoStop2 == false && hit.transform.name == "Stopline (6)(Stop)")
        {

            StartCoroutine(CarCoroutine2());

        }*/

    }

    IEnumerator CarCoroutine1(GameObject trigger)//wait for ... seconds before car becomes active
    {

        if (stoptime_Car1 == false)
        {
            yield return new WaitForSeconds(1.0f);

        }
        else
        {
            yield return new WaitForSeconds(time);
        }

        if (trigger.GetComponent<Trigger1>().needtoStop1 == true && agent.isStopped == true)//one last check
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }


    }

   /* IEnumerator CarCoroutine2()//wait for ... seconds before car becomes active
    {

        if (stoptime_Car2 == false)
        {
            yield return new WaitForSeconds(1.0f);

        }
        else
        {

            yield return new WaitForSeconds(time);

        }

        if (Trigger2.needtoStop2 == true && agent.isStopped == true)//one last check
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

    }*/
    ///////////////////////////////////////////////////////////////////////////////////////////////Stoplight
    private void StopLightTurn(float distance)///stoplight example
    {
        if (distance < stopDistance_Stop)
        {

            //turn right
            if (agent.tag=="Car2")//using a tag to differentiate between the two cars would be best
            {
               //Debug.Log(agent.name+" will turn");

                if (hit.name == "Stoplight A")//need raycast range to be 90
                {
                    agent.isStopped = true;

                    if (TriggerA.needtoStop == true || TriggerB.needtoStop==true)//works
                    {

                        stoptime_Car3 = false;
                        //Debug.Log("here");

                    }

                    CarRightTurnDecision();

                }
               /* else if(hit.name=="Stoplight C")////also have to be careful of cars
                {
                    agent.isStopped = true;

                    if (TriggerA.needtoStop == true)
                    {

                        stoptime_Car4 = false;
                        Debug.Log("here");

                    }
                    CarRightTurnDecision();
                }*/
            }
            else
            {
                agent.isStopped = true;//agent will stop
            }


        }
    }

    private void CheckPedestrainCrossRoad(float distance)//green light check
    {
        if (distance < stopDistance_Stop)
        {
            if (agent.CompareTag("Car1"))//cars go straight
            {
                if (hit.name == "Stoplight A")//check if there are still pedestrians walking in the crossroad
                {

                    if (TriggerA.needtoStop == true || TriggerD.needtoStop == true)//some is still walking in the crossroad
                    {

                        agent.isStopped = true;//agent will not move

                    }
                    else
                    {
                        agent.isStopped = false;//agent will move
                    }
                }
                else if (hit.name == "Stoplight D")
                {
                    if (TriggerD.needtoStop == true || TriggerA.needtoStop == true)//some is still walking in the crossroad
                    {
                        agent.isStopped = true;//agent will not move
                    }
                    else
                    {
                        agent.isStopped = false;//agent will move
                    }
                }
            }
            else if (agent.CompareTag("Car2"))//use comparetag-it is faster
            {
                if (hit.name == "Stoplight A")//check if there are still pedestrians walking in the crossroad
                {

                    if (TriggerA.needtoStop == true || TriggerB.needtoStop == true)//some is still walking in the crossroad
                    {
                        //agent.velocity = Vector3.zero;
                        agent.isStopped = true;//agent will not move
                        Debug.Log("no turn");

                    }
                    else
                    {
                        agent.isStopped = false;//agent will move
                    }
                }
                else if (hit.name == "Stoplight C")
                {
                    if (TriggerC.needtoStop == true || TriggerA.needtoStop == true)//some is still walking in the crossroad
                    {
                        agent.isStopped = true;//agent will not move
                    }
                    else
                    {
                        agent.isStopped = false;//agent will move
                    }
                }

            }
        }

    }

    private void CarRightTurnDecision()
    {
        if (TriggerA.needtoStop == false || TriggerB.needtoStop==false)
        {
            StartCoroutine(CarCoroutine3());

        }
       else if (TriggerC.needtoStop == false)
        {
            StartCoroutine(CarCoroutine4());
        }
    }


    IEnumerator CarCoroutine3()//wait for ... seconds before car becomes active
    {

        if (stoptime_Car3 == false)
        {
            yield return new WaitForSeconds(1.0f);

        }
        else
        {

            yield return new WaitForSeconds(time);

        }

        if ((TriggerA.needtoStop == true && agent.isStopped == true) || (TriggerB.needtoStop == true && agent.isStopped == true))//one last check
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

    }

    IEnumerator CarCoroutine4()//wait for ... seconds before car becomes active
    {

        if (stoptime_Car4 == false)
        {
            yield return new WaitForSeconds(1.0f);

        }
        else
        {

            yield return new WaitForSeconds(time);

        }

        if (TriggerC.needtoStop == true && agent.isStopped == true)//one last check
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

    }






}
