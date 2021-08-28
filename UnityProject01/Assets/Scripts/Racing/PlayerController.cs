using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject CAR;
    GameManager GameManager;

    float moveSpeed = 0.0f;
    float speedRotate = 100.0f;
    bool chk = false;

    //Collider collider;
    //Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = gameObject.GetComponent<Rigidbody>();
        //collider = gameObject.GetComponent<Collider>();
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
        move = move * moveSpeed * Time.deltaTime;

        if (move < 0)
        {
            rotate *= -1;
        }
        gameObject.transform.Rotate(Vector3.up * rotate);
        gameObject.transform.Translate(Vector3.forward * move);

        if (moveSpeed >= 20.0f) moveSpeed = 20.0f;
        moveSpeed += 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        moveSpeed = 10.0f;
        //GameObject hitObject = collision.gameObject;
        //gameObject.transform.Translate(Vector3.forward * 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!chk)
        {
            GameManager.Instance.EndTimeChk(CAR);
            chk = true;
        }
    }
}
