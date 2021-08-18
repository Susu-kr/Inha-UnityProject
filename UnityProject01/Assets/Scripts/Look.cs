using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Look_At_1();
        Look_At_2();
    }

    void Look_At_1()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
    }

    void Look_At_2()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.forward = dirToTarget.normalized; // 단위벡터가 된다. (크기가 1)
    }
}
