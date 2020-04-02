using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light green;
    public Light red;


    // Start is called before the first frame update
    void Start()
    {
        green= green.GetComponent<Light>();
        red= red.GetComponent<Light>();
        red.enabled = false;//red light is off first
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("Entered");

            green.enabled = false;//turn off green light
            red.enabled=true;//turn on red light
        
        }

        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("Entered");

            green.enabled = true;//turn off green light
            red.enabled = false;//turn on red light

        }
    }
}
