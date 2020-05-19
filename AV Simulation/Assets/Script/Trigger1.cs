using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public static bool needtoStop1 = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Enter");
            needtoStop1 = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Enter");
            needtoStop1 = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // Debug.Log("Exit");
            needtoStop1 = false;

        }
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

        }
    }

}


