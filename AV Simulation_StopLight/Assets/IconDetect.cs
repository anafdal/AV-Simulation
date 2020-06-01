using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDetect : MonoBehaviour
{
    //detected
    private GameObject Image1;
    private GameObject Image2;

    //stopped
    private GameObject Image3;
    private GameObject Image4;

    //restarting
    private GameObject Image5;
    private GameObject Image6;

    [HideInInspector]
    public int imValue;//check value


    private GameObject Detect;//the images are held here
    
  
    private void Start()
    {
        imValue = 0;

        Detect = this.transform.Find("Detect ").gameObject;

        Image1 = Detect.transform.GetChild(0).gameObject;
        Image2 = Detect.transform.GetChild(1).gameObject;
        Image3 = Detect.transform.GetChild(2).gameObject;
        Image4 = Detect.transform.GetChild(3).gameObject;
        Image5 = Detect.transform.GetChild(4).gameObject;
        Image6 = Detect.transform.GetChild(5).gameObject;

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
       

        if (imValue == 1)//detect
        {
            Image1.SetActive(true);
            Image2.SetActive(true);

            Image3.SetActive(false);
            Image4.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);

        }
        else if (imValue == 2)//stop
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);

            Image3.SetActive(true);
            Image4.SetActive(true);
        }
        else if (imValue == 3)//restart
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            Image3.SetActive(false);
            Image4.SetActive(false);

            Image5.SetActive(true);
            Image6.SetActive(true);
        }
        else//deactivated
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

