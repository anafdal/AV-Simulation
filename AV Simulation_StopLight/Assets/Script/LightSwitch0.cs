using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch0 : MonoBehaviour
{
    public Light green;
    public Light red;
    public float time = 20.0f;//rate to change lights

    public GameObject redSpot;
    public GameObject greenSpot;
    public GameObject yellowSpot;


    void Start()
    {
        green = green.GetComponent<Light>();
        red = red.GetComponent<Light>();

        green.enabled = true;
        red.enabled = false;

        yellowSpot.GetComponent<Renderer>().material.color = Color.gray;
        redSpot.GetComponent<Renderer>().material.color = Color.gray;
        greenSpot.GetComponent<Renderer>().material.color = Color.green;
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

        redSpot.GetComponent<Renderer>().material.color = Color.red;
        greenSpot.GetComponent<Renderer>().material.color = Color.gray;
        //Debug.Log("Entered1");
    }


    IEnumerator LightChange2()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        green.enabled = true;
        red.enabled = false;

        redSpot.GetComponent<Renderer>().material.color = Color.gray;
        greenSpot.GetComponent<Renderer>().material.color = Color.green;
        //Debug.Log("Entered2");
    }
}
