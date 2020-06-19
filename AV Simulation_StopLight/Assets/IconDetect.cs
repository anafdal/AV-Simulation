using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDetect : MonoBehaviour
{
    //detected
    private GameObject Image1;
    private GameObject Image2;
    private GameObject StripeY;//yellow stripe

    //stopped
    private GameObject Image3;
    private GameObject Image4;
    private GameObject StripeG;//green stripe

    //restarting
    private GameObject Image5;
    private GameObject Image6;
    private GameObject StripeR;//red stripe

    //runnning
    private GameObject StripeB;//blue stripe

    [HideInInspector]
    public int imValue;//check value


    private GameObject Detect;//the images are held here
    
  
    private void Start()
    {
        imValue = 0;

        Detect = this.transform.Find("Detect ").gameObject;

        Image1 = Detect.transform.GetChild(0).gameObject;
        Image2 = Detect.transform.GetChild(1).gameObject;
        StripeY= Detect.transform.GetChild(2).gameObject;

        Image3 = Detect.transform.GetChild(3).gameObject;
        Image4 = Detect.transform.GetChild(4).gameObject;
        StripeG= Detect.transform.GetChild(5).gameObject;

        Image5 = Detect.transform.GetChild(6).gameObject;
        Image6 = Detect.transform.GetChild(7).gameObject;
        StripeR= Detect.transform.GetChild(8).gameObject;

        StripeB= Detect.transform.GetChild(9).gameObject;

        Image1.SetActive(false);
        Image2.SetActive(false);
        StripeY.SetActive(false);

        Image3.SetActive(false);
        Image4.SetActive(false);
        StripeG.SetActive(false);

        Image5.SetActive(false);
        Image6.SetActive(false);
        StripeR.SetActive(false);

        StripeB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       

        if (imValue == 1)//detect
        {
            Image1.SetActive(true);
            Image2.SetActive(true);
            StripeY.SetActive(true);

            Image3.SetActive(false);
            Image4.SetActive(false);
            StripeG.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);
            StripeR.SetActive(false);

        }
        else if (imValue == 2)//stop
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            StripeY.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);
            StripeR.SetActive(false);

            Image3.SetActive(true);
            Image4.SetActive(true);
            StripeG.SetActive(true);
        }
        else if (imValue == 3)//restart
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            StripeY.SetActive(false);
            Image3.SetActive(false);
            Image4.SetActive(false);
            StripeG.SetActive(false);

            Image5.SetActive(true);
            Image6.SetActive(true);
            StripeR.SetActive(true);
        }
        else//deactivated
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            StripeY.SetActive(false);

            Image3.SetActive(false);
            Image4.SetActive(false);
            StripeG.SetActive(false);

            Image5.SetActive(false);
            Image6.SetActive(false);
            StripeR.SetActive(false);
        }

    }
}

