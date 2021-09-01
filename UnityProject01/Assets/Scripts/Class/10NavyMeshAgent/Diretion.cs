using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretion : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }
}
