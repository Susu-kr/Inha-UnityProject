using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    public GameObject startObj;
    public GameObject endObj;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(lineRenderer)
        {
            lineRenderer.SetPosition(0, startObj.transform.position);
            lineRenderer.SetPosition(1, endObj.transform.position);

        }
    }
}
