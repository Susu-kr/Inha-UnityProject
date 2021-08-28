using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera mainCamera;

    Vector3     defaultPosition;
    Quaternion  defaultRotation;
    float       defaultZoom;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        defaultPosition = mainCamera.transform.position;
        defaultRotation = mainCamera.transform.rotation;
        defaultZoom = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        InitCamera();
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    void InitCamera()
    {
        if(Input.GetMouseButton(2))
        {
            mainCamera.transform.position = defaultPosition;
            mainCamera.transform.rotation = defaultRotation;
            mainCamera.fieldOfView = defaultZoom;
        }
    }

    void MoveCamera()
    {
        if(Input.GetMouseButton(0))
        {
            transform.Translate(
                Input.GetAxisRaw("Mouse X") / 10.0f,
                Input.GetAxisRaw("Mouse Y") / 10.0f,
                0.0f);
        }
    }

    void RotateCamera()
    {
        if(Input.GetMouseButton(1))
        {
            // »óÇÏÁÂ¿ì
            transform.Rotate(Input.GetAxisRaw("Mouse Y") * -10.0f, Input.GetAxisRaw("Mouse X") * 10.0f, 0.0f);
        }
    }

    void ZoomCamera()
    {
        mainCamera.fieldOfView += (20 * -Input.GetAxis("Mouse ScrollWheel"));
    }
}
