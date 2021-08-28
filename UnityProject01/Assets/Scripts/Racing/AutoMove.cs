using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public GameObject CAR;

    [Range(0, 50)]
    public float distance = 15.0f;
    public float moveSpeed = 0.0f;
    public float speedRotate = 100.0f;
    public bool chk = false;

    private RaycastHit[] rayLeft;
    private RaycastHit[] rayRight;

    // Start is called before the first frame update
    void Start()
    {

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
                    Gizmos.DrawLine(transform.position, rayRight[i].point);
                }
            }
        }

        if (rayLeft != null)
        {
            for (int i = 0; i < rayLeft.Length; i++)
            {
                if (this.rayLeft[i].collider.gameObject.tag == "Wall")
                {
                    Gizmos.DrawLine(transform.position, rayLeft[i].point);
                }
            }
        }
    }

    void AUTOMOVE()
    {
        rayLeft = Physics.RaycastAll(transform.position, transform.forward - transform.right, distance);
        rayRight = Physics.RaycastAll(transform.position, transform.forward + transform.right, distance);
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
        CAR.transform.Translate(Vector3.forward * moveDelta);

        if (LEFT && !RIGHT)
        {
            CAR.transform.Rotate(Vector3.up * rotate);
        }
        if (!LEFT && RIGHT)
        {
            CAR.transform.Rotate(Vector3.up * (-rotate));
        }
        if (moveSpeed >= 20.0f) moveSpeed = 20.0f;
        moveSpeed += 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        moveSpeed = 10.0f;
        //hitObject.transform.Translate(Vector3.forward * 0);
        //CAR.transform.Translate(Vector3.forward * 0);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(!chk)
    //    {
    //        GameManager.Instance.EndTimeChk(CAR);
    //        chk = true;
    //    }
    //}


}
