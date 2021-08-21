using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 5.0f;
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
        Rotate_4();
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
        float y = Input.GetAxis("Horizontal");
        y = y * speed * Time.deltaTime;
        gameObject.transform.Rotate(new Vector3(0, y, 0));
        
        //if (Input.GetKey(KeyCode.W))
        //{
        //    float rot = speed * Time.deltaTime * 10.0f;
        //    FR.transform.Rotate(Vector3.up * rot);
        //    FL.transform.Rotate(Vector3.up * rot);
        //    RR.transform.Rotate(Vector3.up * rot);
        //    RL.transform.Rotate(Vector3.up * rot);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    float rot = speed * Time.deltaTime * -10.0f;
        //    FR.transform.Rotate(Vector3.up * rot);
        //    FL.transform.Rotate(Vector3.up * rot);
        //    RR.transform.Rotate(Vector3.up * rot);
        //    RL.transform.Rotate(Vector3.up * rot);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    float rot = speed * Time.deltaTime * -10.0f;
        //    FR.transform.Rotate(Vector3.up * rot, Space.World);
        //    FL.transform.Rotate(Vector3.up * rot, Space.World);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    float rot = speed * Time.deltaTime * 10.0f;
        //    FR.transform.Rotate(Vector3.up * rot, Space.World);
        //    FL.transform.Rotate(Vector3.up * rot, Space.World);
        //}
    }

    void Rotate_4()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Vector3 dir = new Vector3(10, 0, 0) - transform.position;
            Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);

            if(dirXZ != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dirXZ);
                Quaternion frameRot = Quaternion.RotateTowards(transform.rotation
                    , targetRot, speed * Time.deltaTime);
                transform.rotation = frameRot;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Vector3 dir = new Vector3(-10, 0, 0) - transform.position;
            Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);

            if (dirXZ != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dirXZ);
                Quaternion frameRot = Quaternion.RotateTowards(transform.rotation
                    , targetRot, speed * Time.deltaTime);
                transform.rotation = frameRot;
            }
        }
    }
}
