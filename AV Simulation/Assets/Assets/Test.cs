using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject sp;

    // Update is called once per frame
    void Update()
    {
        sp.transform.position = agent.steeringTarget;
    }
}
