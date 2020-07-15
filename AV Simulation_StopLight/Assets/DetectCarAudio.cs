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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //Find the closest car
      // CarList = GetObjectsInLayer(Cars, 9);

      //Check the closest car within radius of 0.5 unit





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
    // private static List<GameObject> GetObjectsInLayer(GameObject root, int layer)
    // {
    //     var ret = new List<GameObject>();
    //     foreach (Transform t in root.transform.GetComponentsInChildren(typeof(GameObject), true))
    //     {
    //         if (t.gameObject.layer == layer)
    //         {
    //             ret.Add (t.gameObject);
    //         }
    //     }
    //     return ret;
    // }
}
