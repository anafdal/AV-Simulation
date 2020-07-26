using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarStop : MonoBehaviour
{//found in car(..)

    private GameObject Player;

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
    private bool value2 = false;

    //raycast
    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    public float maxDistance = 90.0f;//raycast can detect anything with 90 units
    RaycastHit raycastHit;//hit
    GameObject hit;


    void Start()
    {
        Player = GameObject.Find("FirstPerson-AIO");
        agent = agent.GetComponent<NavMeshAgent>();
        Cursor.visible = false;
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

                 if (hit.transform.tag == "Stop") {//for stop signs

                    stopdestination = hit.transform.GetChild(0).position;//position to use as a stop place
                    float distance = Vector3.Distance(transform.position, stopdestination);//calculate distance between objects

                    IconUi.ChangeIcon(distance,agent);


                 }


            }
            else
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
                        value2 = true;

                        if (agent.CompareTag("Car1"))
                        {
                            IconUi.ChangeIcon2(distance, red.enabled, value2, agent);//only if light is red
                           //Debug.Log(agent.name+" :"+distance);
                        }

                        else if (agent.CompareTag("Car2"))//these cars will turn
                        {
                            IconUi.ChangeIcon(distance,agent);
                        }

                        StopLightTurn(distance);
                       //Debug.Log(agent.name + " hit");

                    }

                    else if (green.enabled == true)//if green is on
                    {
                         //value2=IconUi.StopIcon2(value2, agent);//only if agent has already been stopped by red light previously

                        CheckPedestrainCrossRoad(distance);
                    }

                }
                else if (hit.CompareTag ("Check"))//prevent the agent froms stopping in the midddle of the crossroad if light turns red
                {
                    IconUi.StopIcon(agent);//display only running icons


                    float distance = Vector3.Distance(transform.position, hit.transform.position);//calculate distance

                    if (distance < 20)
                    {
                        agent.isStopped = false;//agent will move
                        agent.SetDestination(agent.steeringTarget);
                    }



                  // Debug.Log(agent.name+": Hit");
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
          //add Audio
            agent.SetDestination(agent.steeringTarget);//once car in front moves, resume path
            agent.isStopped = false;
        }


    }

    private void PedestrianCheck(GameObject trigger) {///checks if there is a person walking on the pedestrian walk at the stop sign

        if (trigger.GetComponent<Trigger1>().needtoStop1 == true)
        {
            // Player.GetComponent<DetectCarAudio>().PlayAudio("CarApproach");
            stoptime_Car1 = false;
        }



    }

    private void CarDecision(GameObject trigger)//decides if the car needs to wait for pedestrian or leave after pedestrian has crossed
    {
        //restarting
        if (trigger.GetComponent<Trigger1>().needtoStop1 == false)
        {
          //add Audio "car restarting"
          // Player.GetComponent<DetectCarAudio>().PlayAudio("AboutToRestart");

          StartCoroutine(CarCoroutine1(trigger));

        }


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
                        value2 = IconUi.StopIcon2(value2, agent);//only if agent has already been stopped by red light previously
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
                        value2 = IconUi.StopIcon2(value2, agent);//only if agent has already been stopped by red light previously
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
                        value2 = IconUi.StopIcon2(value2, agent);//only if agent has already been stopped by red light previously
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
                        value2 = IconUi.StopIcon2(value2, agent);//only if agent has already been stopped by red light previously
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
