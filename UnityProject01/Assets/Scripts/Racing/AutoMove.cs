using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public GameObject CAR;


    [Range(0, 50)]
    public float distance = 15.0f;
    public float moveSpeed = 1.0f;
    public float speedRotate = 100.0f;

    private RaycastHit[] rayLeft;
    private RaycastHit[] rayRight;
    private Ray rayR;
    private Ray rayL;

    // Start is called before the first frame update
    void Start()
    {
        rayR = new Ray(transform.position, transform.forward + transform.right);
        rayL = new Ray(transform.position, transform.forward - transform.right);
    }

    // Update is called once per frame
    void Update()
    {
        AUTOMOVE();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawGizmos();
    }

    private void DrawGizmos()
    {
        if(rayRight != null)
        {
            for(int i = 0; i < rayRight.Length; i++)
            {
                if(this.rayRight[i].collider.gameObject.tag == "Wall")
                {
                    Gizmos.DrawLine(transform.position, transform.position + rayRight[i].point);
                }
            }
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward +transform.right) * distance);
        }

        if (rayLeft != null)
        {
            for (int i = 0; i < rayLeft.Length; i++)
            {
                if (this.rayLeft[i].collider.gameObject.tag == "Wall")
                {
                    Gizmos.DrawLine(transform.position, transform.position + rayLeft[i].point);
                }
            }
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward - transform.right) * distance);
        }
    }

    void AUTOMOVE()
    {
        rayLeft = Physics.RaycastAll(rayL, distance);
        rayRight = Physics.RaycastAll(rayR, distance);
        bool LEFT = false;
        bool RIGHT = false;

        for(int i = 0; i < rayLeft.Length; i++)
        {
            if(rayLeft[i].collider.gameObject.tag == "Wall")
            {
                LEFT = true;
            }
        }
        for (int i = 0; i < rayRight.Length; i++)
        {
            if (rayRight[i].collider.gameObject.tag == "Wall")
            {
                RIGHT = true;
            }
        }
        float rotate = speedRotate * Time.deltaTime;
        float moveDelta = this.moveSpeed * Time.deltaTime;

        if (LEFT && RIGHT)
        {
            CAR.transform.Translate(Vector3.forward * moveDelta);
        }
        if (LEFT && !RIGHT)
        {
            CAR.transform.Rotate(Vector3.up * rotate);
            CAR.transform.Translate(Vector3.forward * moveDelta);
        }
        if (!LEFT && RIGHT)
        {
            CAR.transform.Rotate(Vector3.up * (-rotate));
            CAR.transform.Translate(Vector3.forward * moveDelta);
        }
    }
}
