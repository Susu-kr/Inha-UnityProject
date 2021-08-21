using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    float speedMove = 10.0f;
    float speedRotate = 100.0f;

    Collider collider;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move_Rotate();
    }

    public void Move_Rotate()
    {
        float rotate = Input.GetAxis("Horizontal");
        float move = Input.GetAxis("Vertical");

        rotate = rotate * speedRotate * Time.deltaTime;
        move = move * speedMove * Time.deltaTime;

        if(move < 0)
        {
            rotate *= -1;
        }
        gameObject.transform.Rotate(Vector3.up * rotate);
        gameObject.transform.Translate(Vector3.forward * move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌이 발생한 경우
        GameObject hitObject = collision.gameObject;
        print("Collider : " + hitObject.name + " 와 충돌시작");
    }

    private void OnCollisionStay(Collision collision)
    {
        // 충돌 하고 있는 중간
        GameObject hitObject = collision.gameObject;
        print("Collider : " + hitObject.name + " 와 충돌중");
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌이 끝난 경우
        GameObject hitObject = collision.gameObject;
        print("Collider : " + hitObject.name + " 와 충돌끝");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌이 발생한 경우
        GameObject hitObject = other.gameObject;
        print("Trigger : " + hitObject.name + " 와 충돌시작");
    }

    private void OnTriggerStay(Collider other)
    {
        // 충돌 하고 있는 중간
        GameObject hitObject = other.gameObject;
        print("Trigger  : " + hitObject.name + " 와 충돌중");
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌이 끝난 경우
        GameObject hitObject = other.gameObject;
        print("Trigger  : " + hitObject.name + " 와 충돌끝");
    }
}
