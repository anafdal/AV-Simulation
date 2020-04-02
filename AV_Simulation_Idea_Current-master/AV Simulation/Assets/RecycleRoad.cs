using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleRoad : MonoBehaviour
{//script in cars(..)


    public GameObject[] startPoints;//list of startpoints
    public GameObject[] cars;//list of cars

    public static GameObject newStartObject;
    private int position;
  
    void Awake()//do update for multiples
    {

        position = returnPosition();//call function to receive a random position     
        //Debug.Log(position);
        newStartObject = startPoints[position];//get the object
    }

    private int returnPosition()
    {
        //int range = Random.Range(0, newRoad.Length);//randomly generated value
        //return range;
        int value=0;

        for(int i = 0; i < cars.Length; i++)
        {
            if (this.transform.name == cars[i].transform.name)
            {
              
                value=(i+1)%2;
            }
           
        }
        return value;

    }

}
