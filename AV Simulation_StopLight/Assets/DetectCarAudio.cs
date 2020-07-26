using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCarAudio : MonoBehaviour
{
    // public GameObject AudioControl;

    public AudioSource audioSource;
    // public AudioSource[] AllCarAudios;

    public AudioClip CarApproach;
    public AudioClip AboutToRestart;
    public AudioClip FullyStop;
    // public AudioClip YieldingCrossNow;
    // public AudioClip RestartingMove;

    public float Volume = 0.7f;

    public GameObject Cars;
    GameObject ClosestCar;

    List<GameObject> CarList;

    float ClosestDis;
    float timeCount = 0f;
    // Start is called before the first frame update
    void Start()
    {
      CarList = new List<GameObject>();

      foreach(Transform t in Cars.transform){
        if(t.gameObject.layer == 9) //layer 9 is Car
        CarList.Add(t.gameObject);
      }

      ClosestCar = CarList[0];
      ClosestDis = Vector3.Distance(transform.position, ClosestCar.transform.position);

    }

    // Update is called once per frame
    void Update()
    {

      // //Check the closest car within certain distance
      // foreach(GameObject car in CarList){
      //     float Dis = Vector3.Distance(transform.position, car.transform.position);
      //     if (Dis < ClosestDis){
      //       ClosestDis = Dis;
      //       ClosestCar = car;
      //     }
      // }
      // // Debug.Log(ClosestCar.gameObject.name);
      //
      // if(ClosestDis < 120.0f && ClosestDis > 119.0f){
      //   Debug.Log(ClosestCar.gameObject.name);
      //   Debug.Log(ClosestDis);
      //   audioSource.PlayOneShot(CarApproach, Volume);
      // }

      //make sure each audio trigger has gap at least 3 seconds - avoid voice overlapping
      timeCount += Time.deltaTime;


    }

    public void PlayAudio(string command){
      switch(command){
        case "CarApproach":
          if(timeCount>=3.0f)
          {
            audioSource.PlayOneShot(CarApproach, Volume);
            timeCount = 0f;
          }
          break;
        case "FullyStop":
          if(timeCount>=3.0f)
          {
            audioSource.PlayOneShot(FullyStop, Volume);
            timeCount = 0f;
          }
          break;
        case "AboutToRestart":
          if(timeCount>=3.0f)
          {
            audioSource.PlayOneShot(AboutToRestart, Volume);
            timeCount = 0f;
          }
          break;
        default:
          break;
      }
      // audioSource.PlayOneShot(CarApproach, Volume);
    }

}
