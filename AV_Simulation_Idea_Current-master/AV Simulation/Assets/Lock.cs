using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject bar;
   
    // Start is called before the first frame update
    void Update()
    {
        bar.transform.position = new Vector3(bar.transform.position.x,-0.9f, bar.transform.position.z);

    }

}
