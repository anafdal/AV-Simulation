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
    private GameObject Image7;
    private GameObject Image8;
    private GameObject StripeB;//blue stripe

    [HideInInspector]
    public int imValue;//check value


    private GameObject Detect;//the images are held here
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("FirstPerson-AIO");
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

        Image7 = Detect.transform.GetChild(9).gameObject;
        Image8 = Detect.transform.GetChild(10).gameObject;
        StripeB = Detect.transform.GetChild(11).gameObject;

        Image1.SetActive(false);
        Image2.SetActive(false);
        StripeY.SetActive(false);

        Image3.SetActive(false);
        Image4.SetActive(false);
        StripeG.SetActive(false);

        Image5.SetActive(false);
        Image6.SetActive(false);
        StripeR.SetActive(false);

        Image7.SetActive(true);
        Image8.SetActive(true);
        StripeB.SetActive(true);
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
            Image7.SetActive(false);
            Image8.SetActive(false);
            StripeB.SetActive(false);
            // audio: I'm Approaching
            Player.GetComponent<DetectCarAudio>().PlayAudio("CarApproach");
        }
        else if (imValue == 2)//stop
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            StripeY.SetActive(false);
            Image5.SetActive(false);
            Image6.SetActive(false);
            StripeR.SetActive(false);
            Image7.SetActive(false);
            Image8.SetActive(false);
            StripeB.SetActive(false);

            Image3.SetActive(true);
            Image4.SetActive(true);
            StripeG.SetActive(true);

            // audio: I'm fully stopped
            Player.GetComponent<DetectCarAudio>().PlayAudio("FullyStop");
        }
        else if (imValue == 3)//restart
        {
            Image1.SetActive(false);
            Image2.SetActive(false);
            StripeY.SetActive(false);
            Image3.SetActive(false);
            Image4.SetActive(false);
            StripeG.SetActive(false);
            Image7.SetActive(false);
            Image8.SetActive(false);
            StripeB.SetActive(false);

            Image5.SetActive(true);
            Image6.SetActive(true);
            StripeR.SetActive(true);

            // audio: I'm restarting
            Player.GetComponent<DetectCarAudio>().PlayAudio("AboutToRestart");
        }
        else//running
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

            Image7.SetActive(true);
            Image8.SetActive(true);
            StripeB.SetActive(true);
        }

    }
}
