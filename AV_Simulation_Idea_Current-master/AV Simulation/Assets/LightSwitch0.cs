using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch0 : MonoBehaviour
{
    public Light green;
    public Light red;
    public float time = 20.0f;//rate to change lights


    void Start()
    {
        green = green.GetComponent<Light>();
        red = red.GetComponent<Light>();
        green.enabled = true;
        red.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (green.enabled == true && red.enabled == false)
        {
            StartCoroutine(LightChange1());

        }
        else if (green.enabled == false && red.enabled == true)
        {
            StartCoroutine(LightChange2());

        }

    }

    IEnumerator LightChange1()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        green.enabled = false;
        red.enabled = true;
        //Debug.Log("Entered1");
    }


    IEnumerator LightChange2()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        green.enabled = true;
        red.enabled = false;
        //Debug.Log("Entered2");
    }
}
