using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;

    void Update()
    {
        //1. 마우스 입력 값을 이용한다.
        float mx = Input.GetAxis("Mouse X"); 
        float my = Input.GetAxis("Mouse Y"); 

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);

        //2. 회전을 한다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);

    }
}
