using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleRoad : MonoBehaviour
{//script in START


    public GameObject[] newRoad;//list of startpoints
    //public GameObject[] newEnd;

   
    public static GameObject newStartObject;
    private int random;


    void Update()//do update for multiples
    {


        random = returnRandomPosition(newRoad);//call function to receive a random position
       
        newStartObject = newRoad[random];//get the object
    }


    private int returnRandomPosition(GameObject[] newRoad)
    {
        int range = Random.Range(0, newRoad.Length);//randomly generated value
        //Debug.Log(range);
        return range;
    }

}
