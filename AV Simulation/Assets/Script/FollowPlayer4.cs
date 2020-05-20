using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer4 : MonoBehaviour
{
    public Transform target;
    public int y = -20;
    public int x = 20;

    // Start is called before the first frame update
    void Start()
    {
        // transform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.localRotation=new Vector3(this.transmform)
        this.transform.position = new Vector3(target.position.x + x, target.position.y + y, target.position.z);
    }
}
