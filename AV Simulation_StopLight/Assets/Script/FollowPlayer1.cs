using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    public Transform target;
    public int y = -20;
    public int z = -15;

    // Start is called before the first frame update
    void Start()
    {
       // transform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position=new Vector3(target.position.x,target.position.y+y,target.position.z+z);
    }
}
