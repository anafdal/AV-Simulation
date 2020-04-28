using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDetect : MonoBehaviour
{
    //detected
    public GameObject Image1;
    public GameObject Image2;

    //stopped
    public GameObject Image3;
    public GameObject Image4;

    //restarting
    public GameObject Image5;
    public GameObject Image6;

    private void Start()
    {
        Image1.SetActive(false);
        Image2.SetActive(false);
        Image3.SetActive(false);
        Image4.SetActive(false);
        Image5.SetActive(false);
        Image6.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (CarStop.imValue == 1)
        {
            Image1.SetActive(true);
            Image2.SetActive(true);

            Image3.SetActive(false);
            Image4.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);

        }
        else if(CarStop.imValue == 2)
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);

            Image3.SetActive(true);
            Image4.SetActive(true);
        }
        else if(CarStop.imValue == 3)
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            Image3.SetActive(false);
            Image4.SetActive(false);

            Image5.SetActive(true);
            Image6.SetActive(true);
        }
        else
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            Image3.SetActive(false);
            Image4.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);
        }
        
    }
}
