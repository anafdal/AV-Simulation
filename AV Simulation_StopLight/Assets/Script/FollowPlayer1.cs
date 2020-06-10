using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    public Transform target;
    public int y = -20;
    public int z = -15;
    public int x = 0;

    /// <summary>
    /// Detect (1) y=-20 z=-15 x=0
    /// Detect (2) y=-20 z=15 x=0
    /// Detect (3) y=-20 x=-20 z=0
    /// Detect (4) y=-20 x=20 z=0
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
       // transform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position=new Vector3(target.position.x+x,target.position.y+y,target.position.z+z);
    }
}
