using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerC : MonoBehaviour
{
    public static bool needtoStop = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
            needtoStop = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Enter");
            needtoStop = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Exit");
            needtoStop = false;

        }
    }
}

