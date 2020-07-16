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
    public AudioClip FullyStopCross;
    public AudioClip YieldingCrossNow;
    public AudioClip RestartingMove;

    public float Volume = 0.7f;

    public GameObject Cars;
    GameObject ClosestCar;

    List<GameObject> CarList;

    float ClosestDis;
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

      //Check the closest car within radius of 0.5 unit
      foreach(GameObject car in CarList){
          float Dis = Vector3.Distance(transform.position, car.transform.position);
          if (Dis < ClosestDis){
            ClosestDis = Dis;
            ClosestCar = car;
          }
      }

      if(ClosestDis < 120.0f && ClosestDis > 119.0f){
        Debug.Log(ClosestCar.gameObject.name);
        Debug.Log(ClosestDis);
        audioSource.PlayOneShot(CarApproach, Volume);
      }


      //Testing
      if(Input.GetKey("k")){
        audioSource.PlayOneShot(CarApproach, Volume);
        Debug.Log("Press");
      }
    }
    /*
    void OnCollisionEnter(Collision c){
      if(c.gameObject.layer == 9) // Layer 9 is Car
      audioSource.PlayOneShot(CarApproach, Volume);

    }
    */

}
