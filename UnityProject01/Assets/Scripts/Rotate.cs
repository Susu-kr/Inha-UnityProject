using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 10.0f;
    public GameObject FR,FL,RR,RL;
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.eulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
        //Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        //this.transform.rotation = target;
        //this.transform.Rotate(Vector3.up * 45.0f);
        //this.transform.rotation *= Quaternion.AngleAxis(45.0f, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate_3();
    }

    void Rotate_1()
    {
        float rot = speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rot, Vector3.up);
    }

    void Rotate_2()
    {
        float rot = speed * Time.deltaTime * 10.0f;
        transform.Rotate(Vector3.right * rot);
    }

    void Rotate_3()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float rot = speed * Time.deltaTime * 10.0f;
            FR.transform.Rotate(Vector3.up * rot);
            FL.transform.Rotate(Vector3.up * rot);
            RR.transform.Rotate(Vector3.up * rot);
            RL.transform.Rotate(Vector3.up * rot);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float rot = speed * Time.deltaTime * -10.0f;
            FR.transform.Rotate(Vector3.up * rot);
            FL.transform.Rotate(Vector3.up * rot);
            RR.transform.Rotate(Vector3.up * rot);
            RL.transform.Rotate(Vector3.up * rot);
        }
        if (Input.GetKey(KeyCode.A))
        {
            float rot = speed * Time.deltaTime * -10.0f;
            FR.transform.Rotate(Vector3.up * rot, Space.World);
            FL.transform.Rotate(Vector3.up * rot, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            float rot = speed * Time.deltaTime * 10.0f;
            FR.transform.Rotate(Vector3.up * rot, Space.World);
            FL.transform.Rotate(Vector3.up * rot, Space.World);
        }
    }
}
