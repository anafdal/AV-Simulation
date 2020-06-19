using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch0 : MonoBehaviour
{
    public Light first;
    public Light second;
    public float time = 20.0f;//rate to change lights

    public GameObject firstSpot;
    public GameObject secondSpot;
    public GameObject yellowSpot;

    /// <summary>
    /// Light A and D will turn red first then green second
    /// Light C will turn green first then red second
    /// </summary>
    void Start()
    {
        second = second.GetComponent<Light>();
        first = first.GetComponent<Light>();

        second.enabled = true;
        first.enabled = false;

        //yellowSpot.GetComponent<Renderer>().material.color = Color.gray;
        //firstSpot.GetComponent<Renderer>().material.color = Color.gray;
        //secondSpot.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (second.enabled == true && first.enabled == false)
        {
            StartCoroutine(LightChange1());

        }
        else if (second.enabled == false && first.enabled == true)
        {
            StartCoroutine(LightChange2());

        }

    }

    IEnumerator LightChange1()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        second.enabled = false;
        first.enabled = true;

        //firstSpot.GetComponent<Renderer>().material.color = Color.red;
        //secondSpot.GetComponent<Renderer>().material.color = Color.gray;
        //Debug.Log("Entefirst1");
    }


    IEnumerator LightChange2()//wait for ... seconds before car becomes active
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(time);
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        second.enabled = true;
        first.enabled = false;

        //firstSpot.GetComponent<Renderer>().material.color = Color.gray;
        //secondSpot.GetComponent<Renderer>().material.color = Color.green;
        //Debug.Log("Entefirst2");
    }
}
